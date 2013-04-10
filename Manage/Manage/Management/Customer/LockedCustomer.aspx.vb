
Imports System.Net.Mail
Imports SidejobModel

Namespace Management.Customer

    Partial Class ManagementCustomerLockedCustomer
        Inherits System.Web.UI.Page


        Private Sub UpdateCustomerPortfolio(ByVal ID As String)
            Using sidejobcontext = New SidejobEntities()
                Dim lockedCustomer = (From c In sidejobcontext.LockedCustomers
                        Where c.ID = ID Select c).FirstOrDefault()
                sidejobcontext.DeleteObject(lockedCustomer)
                sidejobcontext.SaveChanges()
                Response.Redirect(Request.Url.ToString())
            End Using
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