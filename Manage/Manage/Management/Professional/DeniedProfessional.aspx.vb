
Imports SidejobModel

Namespace Management.Professional

    Partial Class ManagementProfessionalDeniedProfessional
        Inherits Page

        Private Sub ProfessionalGridViewRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles ProfessionalGridView.RowCommand
            Dim professionalId As String = ProfessionalGridView.DataKeys(0).Value.ToString
            Select Case e.CommandName
                Case "AcceptChange"
                    UpdateProfessionalPortfolio(professionalId)
            End Select
        End Sub

        Private Sub UpdateProfessionalPortfolio(ByVal professionalID As String)
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE ProfessionalPortfolio SET Modified = 0 WHERE ProID = " & professionalID.ToString
            ProfessionalSqlDataSource.UpdateCommand = updatePortfolio
            ProfessionalSqlDataSource.Update()
            Using sidejobcontext = New SidejobEntities()
                Dim deniedProfessional = (From c In sidejobcontext.DeniedProfessionals
                        Where c.ProId = ProfessionalID Select c).FirstOrDefault()
                sidejobcontext.DeleteObject(deniedProfessional)
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