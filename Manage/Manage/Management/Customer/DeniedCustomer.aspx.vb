
Imports System.Linq
Imports SidejobModel

Namespace Management.Customer
    Partial Class ManagementCustomerDeniedCustomer
        Inherits Page

        Protected Sub Page_Load(sender As Object, e As EventArgs)
            If Page.IsPostBack Then
                Return
            End If
            ManageUtility.Authenticate()
        End Sub

        Private Sub CustomerGridViewRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles CustomerGridView.RowCommand
            Dim customerId As String = CustomerGridView.DataKeys(0).Value.ToString
            Select Case e.CommandName
                Case "AcceptChange"
                    UpdateCustomerPortfolio(customerId)
            End Select
        End Sub

        Private Sub UpdateCustomerPortfolio(ByVal customerID As String)
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE CustomerPortfolio SET Modified = 0 WHERE CustomerID = " & customerID.ToString
            CustomerSqlDataSource.UpdateCommand = updatePortfolio
            CustomerSqlDataSource.Update()
            Using sidejobcontext = New SidejobEntities()
                Dim deniedCustomer = (From c In sidejobcontext.DeniedCustomers
                          Where c.CustomerId = customerID Select c).FirstOrDefault()
                sidejobcontext.DeleteObject(deniedCustomer)
            End Using
        End Sub

    End Class
End Namespace