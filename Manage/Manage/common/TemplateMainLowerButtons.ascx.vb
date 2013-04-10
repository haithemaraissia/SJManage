
Partial Class TemplateMainLowerButtons
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        copyrightlabel.Text = Resources.Resource.Copyright.ToString + " © " + Date.Now.Year.ToString
    End Sub
End Class
