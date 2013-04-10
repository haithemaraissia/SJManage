
Imports System.Net.Mail
Imports System.Net
Imports SidejobModel

Namespace Management.Professional
    Partial Class ManagementUserProfessional
        Inherits Page

        Private Sub ProfessionalGridViewRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles ProfessionalGridView.RowCommand
            Dim professionalId As String = ProfessionalGridView.DataKeys(0).Value.ToString
            Select Case e.CommandName
                Case "AcceptChange"
                    UpdateProfessionalPortfolio(professionalId)
                Case "DenyChange"
                    DenyProfessional(professionalId)
                Case "LockUser"
                    LockProfessional(professionalId)
            End Select
        End Sub

        Private Sub UpdateProfessionalPortfolio(ByVal professionalID As String)
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE ProfessionalPortfolio SET Modified = 0 WHERE ProID = " & professionalID.ToString
            ProfessionalSqlDataSource.UpdateCommand = updatePortfolio
            ProfessionalSqlDataSource.Update()
        End Sub

        Private Sub DenyProfessional(ByVal professionalID As String)
            ''Deny Professional            
            ''Modified = 2 
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE ProfessionalPortfolio SET Modified = 2 WHERE ProID = " & professionalID.ToString
            ProfessionalSqlDataSource.UpdateCommand = updatePortfolio
            ProfessionalSqlDataSource.Update()
            Using sidejobcontext = New SidejobEntities()
                Dim professional = (From c In sidejobcontext.ProfessionalGenerals
                Where c.ProID = ProfessionalID Select c).FirstOrDefault()
                Dim deniedProfessional As New DeniedProfessional
                deniedProfessional.ProId = professional.ProID
                deniedProfessional.FirstName = professional.FirstName
                deniedProfessional.LastName = professional.LastName
                deniedProfessional.Country = professional.CountryName
                deniedProfessional.Region = professional.RegionName
                deniedProfessional.Age = professional.Age
                deniedProfessional.Gender = professional.Gender
                deniedProfessional.EmailAddress = professional.EmailAddress
                sidejobcontext.AddToDeniedProfessionals(deniedProfessional)
                sidejobcontext.SaveChanges()
                Dim message = Resources.Resource.Dear + " " + professional.FirstName + " " + professional.LastName + "<br/>" + "<br/>" + _
                                Resources.Resource.YourPorfolioDenied + "<br/>" + _
                                Resources.Resource.IfyouwantDisputeTransaction + "<br/>" + "<br/>" + _
                                Resources.Resource.Sincerely + "<br/>" + _
                                Resources.Resource.Team
                SendEmail(CType(professionalID, Integer), Resources.Resource.ProfileUpdateDenied, message)
            End Using
        End Sub

        Private Sub LockProfessional(ByVal professionalID As String)
            ''Lock Professional
            ''Modified = 3
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE ProfessionalPortfolio SET Modified = 3 WHERE ProID = " & professionalID.ToString
            ProfessionalSqlDataSource.UpdateCommand = updatePortfolio
            ProfessionalSqlDataSource.Update()
            Using sidejobcontext = New SidejobEntities()
                Dim professional = (From c In sidejobcontext.ProfessionalGenerals
                Where c.ProID = ProfessionalID Select c).FirstOrDefault()
                Dim lockedProfessional As New LockedProfessional
                lockedProfessional.ProID = professional.ProID
                lockedProfessional.FirstName = professional.FirstName
                lockedProfessional.LastName = professional.LastName
                lockedProfessional.Country = professional.CountryName
                lockedProfessional.Region = professional.RegionName
                lockedProfessional.Age = professional.Age
                lockedProfessional.Gender = professional.Gender
                lockedProfessional.EmailAddress = professional.EmailAddress
                lockedProfessional.Reason = "Locked Professional"
                lockedProfessional.Date = Date.UtcNow.Date
                lockedProfessional.IP = 0
                lockedProfessional.ProjectID = 0
                sidejobcontext.AddToLockedProfessionals(lockedProfessional)
                sidejobcontext.SaveChanges()
                Dim message = Resources.Resource.Dear + " " + professional.FirstName + " " + professional.LastName + "<br/>" + "<br/>" + _
                              Resources.Resource.YourAccountLocked + "<br/>" + _
                               Resources.Resource.IfyouwantDisputeTransaction + "<br/>" + "<br/>" + _
                               Resources.Resource.Sincerely + "<br/>" + _
                               Resources.Resource.Team
                SendEmail(CType(professionalID, Integer), Resources.Resource.AccountLocked, message)
            End Using
        End Sub

        Private Sub SendEmail(professionalID As Integer, subject As String, body As String)
            Using sidejobcontext = New SidejobEntities()
                Dim receiverEmail = (From c In sidejobcontext.ProfessionalGenerals
              Where c.ProID = professionalID Select c.EmailAddress).FirstOrDefault()
                Const strFrom As String = "postmaster@my-side-job.com"
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
                    InsertEmailSentException(professionalID, "Error while Sending the Email", receiverEmail)
                Catch ex As Exception
                    InsertEmailSentException(professionalID, "Error while Sending the Email", receiverEmail)
                End Try
            End Using
        End Sub

        Private Sub SmtpClient_OnCompleted(ByVal sender As Object, ByVal e As ComponentModel.AsyncCompletedEventArgs)
            If (e.Cancelled) Then
                InsertEmailSentException(0, "CanceledReceiver", )
            End If

            If Not (e.Error Is Nothing) Then
                InsertEmailSentException(0, "ErrorSending", )
            Else
            End If
        End Sub

        Private Sub InsertEmailSentException(professionalID As Integer, reason As String, Optional receiverEmail As String = "NoEmailAddress")
            Using sidejobcontext = New SidejobEntities()
                Dim emailException As New EmailSentException
                emailException.Reason = reason
                emailException.UserId = ProfessionalID
                emailException.EmailAddress = receiverEmail
                emailException.DateTime = Date.UtcNow.Date
                emailException.UserRole = "PRO"
                sidejobcontext.AddToEmailSentExceptions(emailException)
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