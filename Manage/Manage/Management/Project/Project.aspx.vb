
Imports System.Net.Mail
Imports System.Net
Imports SidejobModel
Namespace Management.Project
    Partial Class ManagementProjectProject
        Inherits Page

        Private Sub ProjectGridViewRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles ProjectGridView.RowCommand
            Dim projectId As String = ProjectGridView.DataKeys(0).Value.ToString
            Select Case e.CommandName
                Case "AcceptChange"
                    AcceptProject(projectId)
                Case "DenyChange"
                    DenyProject(projectId)
                Case "LockProject"
                    LockProject(projectId)
            End Select
        End Sub

        Private Sub AcceptProject(ByVal projectID As String)
            Using sidejobcontext = New SidejobEntities()
                sidejobcontext.AcceptProject(CType(projectID, Integer?))
            End Using
        End Sub

        Private Sub DenyProject(ByVal projectID As String)

            ''ADDED TO THE DENIED Project
            Using sidejobcontext = New SidejobEntities()
                Dim project = (From c In sidejobcontext.PendingProjects
                        Where c.ProjectID = projectID Select c).FirstOrDefault()
                Dim deniedProject As New DeniedProject
                deniedProject.ProjectID = project.ProjectID
                deniedProject.LCID = project.LCID
                deniedProject.JobTitle = project.JobTitle
                deniedProject.ProjectTitle = project.ProjectTitle
                deniedProject.StartDate = project.StartDate
                deniedProject.EndDate = project.EndDate
                deniedProject.Description = project.Description
                deniedProject.SpecialNotes = project.SpecialNotes
                deniedProject.Address = project.Address
                deniedProject.DatePosted = project.DatePosted
                sidejobcontext.AddToDeniedProjects(deniedProject)
                sidejobcontext.SaveChanges()
            End Using

            ''DELETE FROM THE PENDING TABLE
            Dim deletePendingProject As String
            deletePendingProject = "DELETE FROM PendingProject WHERE ProjectID = " & projectID.ToString
            ProjectSqlDataSource.DeleteCommand = deletePendingProject
            ProjectSqlDataSource.Delete()
            Dim message = Resources.Resource.YourProjectDenied + "<br/>" + _
                Resources.Resource.IfyouwantDisputeTransaction + "<br/>" + _
                Resources.Resource.Sincerely + "<br/>" + _
                Resources.Resource.Team
            SendEmail(CType(projectID, Integer), Resources.Resource.ProjectUpdateDenied, message)

        End Sub

        Private Sub LockProject(ByVal projectID As String)
            ''ADDED TO THE lockedProject
            Using sidejobcontext = New SidejobEntities()
                Dim project = (From c In sidejobcontext.PendingProjects
                        Where c.ProjectID = projectID Select c).FirstOrDefault()
                Dim lockedProject As New LockedProject
                lockedProject.ProjectID = project.ProjectID
                lockedProject.LCID = project.LCID
                lockedProject.JobTitle = project.JobTitle
                lockedProject.ProjectTitle = project.ProjectTitle
                lockedProject.StartDate = project.StartDate
                lockedProject.EndDate = project.EndDate
                lockedProject.Description = project.Description
                lockedProject.SpecialNotes = project.SpecialNotes
                lockedProject.Address = project.Address
                lockedProject.DatePosted = project.DatePosted
                sidejobcontext.AddToLockedProjects(lockedProject)
                sidejobcontext.SaveChanges()
            End Using

            ''DELETE FROM THE PENDING TABLE
            Dim deletePendingProject As String
            deletePendingProject = "DELETE FROM PendingProject WHERE ProjectID = " & projectID.ToString
            ProjectSqlDataSource.DeleteCommand = deletePendingProject
            ProjectSqlDataSource.Delete()
            Dim message = Resources.Resource.YourProjectLocked + "<br/>" + _
                        Resources.Resource.IfyouwantDisputeTransaction + "<br/>" + _
                        Resources.Resource.Sincerely + "<br/>" + _
                        Resources.Resource.Team
            SendEmail(CType(projectID, Integer), Resources.Resource.ProjectLocked, message)
        End Sub


        Private Sub SendEmail(projectID As Integer, subject As String, body As String)
            Dim receiverEmail As String = ""
            Const strFrom As String = "postmaster@my-side-job.com"
            Using sidejobcontext = New SidejobEntities()
                Dim project = (From c In sidejobcontext.Projects
                        Where c.ProjectID = projectID Select c).FirstOrDefault()
                If (project.PosterRole = "CUS") Then
                    Dim customerid As Integer = project.PosterID
                    receiverEmail = (From c In sidejobcontext.CustomerGenerals
                        Where c.CustomerID = customerid Select c.EmailAddress).FirstOrDefault()
                End If
                If (project.PosterRole = "PRO") Then
                    Dim professionalid As Integer = project.PosterID
                    receiverEmail = (From c In sidejobcontext.ProfessionalGenerals
                        Where c.ProID = professionalid Select c.EmailAddress).FirstOrDefault()
                End If

                Dim mailMsg = New MailMessage(New MailAddress(strFrom), New MailAddress(receiverEmail)) With { _
 .BodyEncoding = Encoding.[Default], _
 .Subject = subject, _
 .Body = body, _
 .Priority = MailPriority.High, _
 .IsBodyHtml = True _
}
                Dim smtpMail = New SmtpClient()
                Dim basicAuthenticationInfo = New NetworkCredential("postmaster@my-side-job.com", "haithem759163")
                smtpMail.Host = "mail.my-side-job.com"
                smtpMail.UseDefaultCredentials = False
                smtpMail.Credentials = basicAuthenticationInfo
                Try
                    smtpMail.Send(mailMsg)
                Catch smtpEx As SmtpException
                    InsertEmailSentException(projectID, "Error while Sending the Email", receiverEmail)
                Catch ex As Exception
                    InsertEmailSentException(projectID, "Error while Sending the Email", receiverEmail)
                End Try
            End Using
        End Sub

        Private Sub InsertEmailSentException(projectID As Integer, reason As String, Optional receiverEmail As String = "NoEmailAddress")
            Using sidejobcontext = New SidejobEntities()
                Dim project = (From c In sidejobcontext.Projects Where c.ProjectID = projectID Select c).FirstOrDefault()
                Dim emailException As New EmailSentException
                emailException.Reason = reason
                emailException.UserId = projectID
                emailException.EmailAddress = receiverEmail
                emailException.DateTime = Date.UtcNow.Date
                emailException.UserRole = project.PosterRole
                sidejobcontext.AddToEmailSentExceptions(emailException)
                sidejobcontext.SaveChanges()
            End Using
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs)
            If Page.IsPostBack Then
                Return
            End If
            ManageUtility.Authenticate()
        End Sub
    End Class
End Namespace