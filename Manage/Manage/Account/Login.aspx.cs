﻿using System;
using System.Web;

namespace Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
         // RegisterHyperLink.NavigateUrl = "Register.aspx?ReturnUrl=" + HttpManageUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }
    }
}
