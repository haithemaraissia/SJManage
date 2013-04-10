
Imports SidejobModel

Namespace Management.Project

    Partial Class ManagementProjectLockedProject
        Inherits Page
        Private Sub ProjectGridViewRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles ProjectGridView.RowCommand
            Dim projectId As String = ProjectGridView.DataKeys(0).Value.ToString
            Select Case e.CommandName
                Case "AcceptChange"
                    AcceptProject(projectId)
                Case "DenyChange"
                    DenyProject(projectId)
            End Select
        End Sub

        Private Sub AcceptProject(ByVal projectID As String)
            Using sidejobcontext = New SidejobEntities()
                sidejobcontext.AcceptProject(CType(projectID, Integer?))
            End Using
            Dim deletePendingProject As String
            deletePendingProject = "DELETE FROM LockedProject WHERE ProjectID = " & projectID.ToString
            ProjectSqlDataSource.DeleteCommand = deletePendingProject
            ProjectSqlDataSource.Delete()
        End Sub

        Private Sub DenyProject(ByVal projectID As String)

            ''ADDED TO THE DENIED Project
            Using sidejobcontext = New SidejobEntities()
                Dim project = (From c In sidejobcontext.LockedProjects
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