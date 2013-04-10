
Imports SidejobModel

Namespace Management.Customer

    Partial Class ManagementCustomerApproveCustomer
        Inherits System.Web.UI.Page


        Private Sub UpdateCustomerPortfolio(ByVal customerID As String)
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE CustomerPortfolio SET Modified = 0 WHERE CustomerID = " & customerID.ToString
            CustomerSqlDataSource.UpdateCommand = updatePortfolio
            CustomerSqlDataSource.Update()
        End Sub

        Protected Sub Page_Load(sender As Object, e As EventArgs)
            If Page.IsPostBack Then
                Return
            End If
            ManageUtility.Authenticate()
        End Sub

        Private Sub CustomerGridViewSelectedIndexChanged(sender As Object, e As System.EventArgs) Handles CustomerGridView.SelectedIndexChanged
            Dim customerId As String = CustomerGridView.SelectedDataKey.Value.ToString()
            UpdateCustomerPortfolio(customerId)
        End Sub
    End Class
End Namespace