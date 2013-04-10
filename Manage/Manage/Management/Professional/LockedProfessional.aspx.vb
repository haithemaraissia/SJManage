
Imports SidejobModel

Namespace Management.Professional

    Partial Class ManagementProfessionalLockedProfessional
        Inherits Page

        Private Sub UpdateProfessionalPortfolio(ByVal professionalID As String)
            Using sidejobcontext = New SidejobEntities()
                Dim lockedProfessional = (From c In sidejobcontext.LockedProfessionals
                        Where c.ID = professionalID Select c).FirstOrDefault()
                sidejobcontext.DeleteObject(lockedProfessional)
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

        Private Sub ProfessionalGridView_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ProfessionalGridView.SelectedIndexChanged
            Dim professionalId As String = ProfessionalGridView.SelectedDataKey.Value.ToString()
            UpdateProfessionalPortfolio(professionalId)
        End Sub
    End Class
End Namespace