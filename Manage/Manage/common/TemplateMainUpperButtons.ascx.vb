
Namespace common
    Partial Class TemplateMainUpperButtons
        Inherits System.Web.UI.UserControl

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
            SelectedLanguages()
            SelectedUpperMenuStyle()
        End Sub


        Protected Sub SelectedUpperMenuStyle()

            Dim requestUrl As String = HttpContext.Current.Request.Url.ToString()
            'Default
            HomeHyperlink.CssClass = "selected"
            UserHyperLink.CssClass = ""
            ProjectHyperLink.CssClass = ""
            AccountHyperLink.CssClass = ""
            HelpHyperlink.CssClass = ""

            If String.IsNullOrEmpty(requestUrl) Then
                HomeHyperlink.CssClass = "selected"
                UserHyperLink.CssClass = ""
                ProjectHyperLink.CssClass = ""
                AccountHyperLink.CssClass = ""
                HelpHyperlink.CssClass = ""
            ElseIf requestUrl.Contains("Default.aspx") Then
                HomeHyperlink.CssClass = "selected"
                UserHyperLink.CssClass = ""
                ProjectHyperLink.CssClass = ""
                AccountHyperLink.CssClass = ""
                HelpHyperlink.CssClass = ""
            ElseIf requestUrl.Contains(("/User/")) Then
                HomeHyperlink.CssClass = ""
                UserHyperLink.CssClass = "selected"
                ProjectHyperLink.CssClass = ""
                AccountHyperLink.CssClass = ""
                HelpHyperlink.CssClass = ""
            ElseIf requestUrl.Contains(("/Project/")) Then
                HomeHyperlink.CssClass = ""
                UserHyperLink.CssClass = ""
                ProjectHyperLink.CssClass = "selected"
                AccountHyperLink.CssClass = ""
                HelpHyperlink.CssClass = ""
            ElseIf requestUrl.Contains(("/Account/")) Then
                HomeHyperlink.CssClass = ""
                UserHyperLink.CssClass = ""
                ProjectHyperLink.CssClass = ""
                AccountHyperLink.CssClass = "selected"
                HelpHyperlink.CssClass = ""
            ElseIf requestUrl.Contains(("help")) Then
                HomeHyperlink.CssClass = ""
                UserHyperLink.CssClass = ""
                ProjectHyperLink.CssClass = ""
                AccountHyperLink.CssClass = ""
                HelpHyperlink.CssClass = "selected"
            End If

            ''Exception in this case
            If requestUrl.Contains(("/Management/Profile/")) Then
                HomeHyperlink.CssClass = "selected"
                UserHyperLink.CssClass = ""
                ProjectHyperLink.CssClass = ""
                AccountHyperLink.CssClass = ""
                HelpHyperlink.CssClass = ""
            End If
        End Sub

        Protected Sub SelectedLanguages()
            Dim lang As String = Request.QueryString("l")
            selected.Src = "../Images/flags/earth.png"

            If lang IsNot Nothing Or lang <> "" Then

                Select Case lang

                    Case "en-US"
                        selected.Src = "../Images/flags/MI.gif"
                        Session("LCID") = 1
                    Case "fr"
                        selected.Src = "../Images/flags/FR.png"
                        Session("LCID") = "2"
                    Case "es"
                        selected.Src = "../Images/flags/ES.png"

                    Case "zh-CN"
                        selected.Src = "../Images/flags/CN.png"

                    Case "ru"
                        selected.Src = "../Images/flags/RU.png"

                    Case "ar"
                        selected.Src = "../Images/flags/AE.png"

                    Case "ja"
                        selected.Src = "../Images/flags/JP.png"

                    Case "de"
                        selected.Src = "../Images/flags/DE.png"
                End Select

            End If
        End Sub
    End Class
End Namespace