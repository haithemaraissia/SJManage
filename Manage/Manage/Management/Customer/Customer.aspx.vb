
Imports System.Net.Mail
Imports System.Linq
Imports System.Net
Imports SidejobModel

Namespace Management.Customer
    Partial Class ManagementUserCustomer
        Inherits Page

        Private Sub CustomerGridViewRowCommand(ByVal sender As Object, ByVal e As GridViewCommandEventArgs) Handles CustomerGridView.RowCommand
            Dim customerId As String = CustomerGridView.DataKeys(0).Value.ToString
            Select Case e.CommandName
                Case "AcceptChange"
                    UpdateCustomerPortfolio(customerId)
                Case "DenyChange"
                    DenyCustomer(customerId)
                Case "LockUser"
                    LockCustomer(customerId)
            End Select
        End Sub

        Private Sub UpdateCustomerPortfolio(ByVal customerID As String)
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE CustomerPortfolio SET Modified = 0 WHERE CustomerID = " & customerID.ToString
            CustomerSqlDataSource.UpdateCommand = updatePortfolio
            CustomerSqlDataSource.Update()
        End Sub

        Private Sub DenyCustomer(ByVal customerID As String)
            ''Deny Customer            
            ''Modified = 2 
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE CustomerPortfolio SET Modified = 2 WHERE CustomerID = " & customerID.ToString
            CustomerSqlDataSource.UpdateCommand = updatePortfolio
            CustomerSqlDataSource.Update()
            Using sidejobcontext = New SidejobEntities()
                Dim customer = (From c In sidejobcontext.CustomerGenerals
                Where c.CustomerID = customerID Select c).FirstOrDefault()
                Dim deniedcustomer As New DeniedCustomer
                deniedcustomer.CustomerId = customer.CustomerID
                deniedcustomer.FirstName = customer.FirstName
                deniedcustomer.LastName = customer.LastName
                deniedcustomer.Country = customer.CountryName
                deniedcustomer.Region = customer.RegionName
                deniedcustomer.Age = customer.Age
                deniedcustomer.Gender = customer.Gender
                deniedcustomer.EmailAddress = customer.EmailAddress
                sidejobcontext.AddToDeniedCustomers(deniedcustomer)
                sidejobcontext.SaveChanges()
                Dim message = Resources.Resource.Dear + " " + customer.FirstName + " " + customer.LastName + "<br/>" + "<br/>" + _
                Resources.Resource.YourPorfolioDenied + "<br/>" + _
                Resources.Resource.IfyouwantDisputeTransaction + "<br/>" + "<br/>" + _
                Resources.Resource.Sincerely + "<br/>" + _
                Resources.Resource.Team
                SendEmail(CType(customerID, Integer), Resources.Resource.ProfileUpdateDenied, message)
            End Using
        End Sub

        Private Sub LockCustomer(ByVal customerID As String)
            ''Lock Customer
            ''Modified = 3
            Dim updatePortfolio As String
            updatePortfolio = " UPDATE CustomerPortfolio SET Modified = 3 WHERE CustomerID = " & customerID.ToString
            CustomerSqlDataSource.UpdateCommand = updatePortfolio
            CustomerSqlDataSource.Update()
            Using sidejobcontext = New SidejobEntities()
                Dim customer = (From c In sidejobcontext.CustomerGenerals
                Where c.CustomerID = customerID Select c).FirstOrDefault()
                Dim lockedCustomer As New LockedCustomer
                lockedCustomer.CustomerID = customer.CustomerID
                lockedCustomer.FirstName = customer.FirstName
                lockedCustomer.LastName = customer.LastName
                lockedCustomer.Country = customer.CountryName
                lockedCustomer.Region = customer.RegionName
                lockedCustomer.Age = customer.Age
                lockedCustomer.Gender = customer.Gender
                lockedCustomer.EmailAddress = customer.EmailAddress
                lockedCustomer.Reason = "Locked Customer"
                lockedCustomer.Date = Date.UtcNow.Date
                lockedCustomer.IP = 0
                lockedCustomer.ProjectID = 0
                sidejobcontext.AddToLockedCustomers(lockedCustomer)
                sidejobcontext.SaveChanges()
                Dim message = Resources.Resource.Dear + " " + customer.FirstName + " " + customer.LastName + "<br/>" + "<br/>" + _
                                Resources.Resource.YourAccountLocked + "<br/>" + _
                                Resources.Resource.IfyouwantDisputeTransaction + "<br/>" + "<br/>" + _
                                Resources.Resource.Sincerely + "<br/>" + _
                                Resources.Resource.Team
                SendEmail(CType(customerID, Integer), Resources.Resource.AccountLocked, message)
            End Using
        End Sub

        Private Sub SendEmail(customerID As Integer, subject As String, body As String)
            Using sidejobcontext = New SidejobEntities()
                Dim receiverEmail As String = (From c In sidejobcontext.CustomerGenerals
                Where c.CustomerID = customerID Select c.EmailAddress).FirstOrDefault()
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
                    InsertEmailSentException(customerID, "Error while Sending the Email", receiverEmail)
                Catch ex As Exception
                    InsertEmailSentException(customerID, "Error while Sending the Email", receiverEmail)
                End Try
            End Using
        End Sub

        Private Sub InsertEmailSentException(customerID As Integer, reason As String, Optional receiverEmail As String = "NoEmailAddress")
            Using sidejobcontext = New SidejobEntities()
                Dim emailException As New EmailSentException
                emailException.Reason = reason
                emailException.UserId = customerID
                emailException.EmailAddress = receiverEmail
                emailException.DateTime = Date.UtcNow.Date
                emailException.UserRole = "CUS"
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