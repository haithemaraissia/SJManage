


Namespace Management.Professional

    Partial Class ManagementProfessionalApproveProfessional
        Inherits Page


        Private Sub UpdateProfessionalPortfolio(ByVal professionalID As String)
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE ProfessionalPortfolio SET Modified = 0 WHERE ProID = " & professionalID.ToString
            ProfessionalSqlDataSource.UpdateCommand = updatePortfolio
            ProfessionalSqlDataSource.Update()
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs)
            If Page.IsPostBack Then
                Return
            End If
            ManageUtility.Authenticate()
        End Sub

        Private Sub ProfessionalGridViewSelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ProfessionalGridView.SelectedIndexChanged
            Dim professionalId As String = ProfessionalGridView.SelectedDataKey.Value.ToString()
            UpdateProfessionalPortfolio(professionalId)
        End Sub
    End Class
End Namespace