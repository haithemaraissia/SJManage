
Imports SidejobModel

Namespace Management.Project

    Partial Class ManagementProjectDeniedProject
        Inherits Page
        Private Sub ProjectGridViewRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles ProjectGridView.RowCommand
            Dim projectId As String = ProjectGridView.DataKeys(0).Value.ToString
            Select Case e.CommandName
                Case "AcceptChange"
                    AcceptProject(projectId)
                Case "LockProject"
                    LockProject(projectId)
            End Select
        End Sub

        Private Sub AcceptProject(ByVal projectID As String)
            Using sidejobcontext = New SidejobEntities()
                sidejobcontext.AcceptProject(CType(projectID, Integer?))
            End Using
            Dim deletePendingProject As String
            deletePendingProject = "DELETE FROM DeniedProject WHERE ProjectID = " & projectID.ToString
            ProjectSqlDataSource.DeleteCommand = deletePendingProject
            ProjectSqlDataSource.Delete()
        End Sub

        Private Sub LockProject(ByVal projectID As String)
            ''ADDED TO THE lockedProject
            Using sidejobcontext = New SidejobEntities()
                Dim project = (From c In sidejobcontext.DeniedProjects
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
                sidejobcontext.DeleteObject(project)
                sidejobcontext.SaveChanges()
                Response.Redirect(Request.Url.AbsoluteUri)
            End Using

            ''DELETE FROM THE PENDING TABLE
            Dim deletePendingProject As String
            deletePendingProject = "DELETE FROM PendingProject WHERE ProjectID = " & projectID.ToString
            ProjectSqlDataSource.DeleteCommand = deletePendingProject
            ProjectSqlDataSource.Delete()
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs)
            If Page.IsPostBack Then
                Return
            End If
            ManageUtility.Authenticate()
        End Sub
    End Class

End Namespace