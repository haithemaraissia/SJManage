<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutomatedEmailProblem.aspx.cs"
    Inherits="Management.TimeUp.PartOfManageAutomatedEmailProblem" %>
<%@ Register TagPrefix="UpperNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainUpperButtons.ascx" %>
<%@ Register TagPrefix="LowerNavigationButtons" TagName="NavigationButtons" Src="../../common/TemplateMainLowerButtons.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title></title>
    <link href="../../Styles/Site.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form runat="server">
    <div style="width: 100%; background: #800000;">
        <table style="margin: 0 auto;" align="center">
            <tr>
                <td>
                    <h2 id="H2SideJob" style="width: 960px;" align="center">
                        <asp:Label runat="server" Text="Side Job" Font-Bold="True" Font-Size="Large"
                            Style="font-size: xx-large; font-family: Andy; color: #FFFFFF;"></asp:Label></h2>
                </td>
            </tr>
        </table>
    </div>
    <div id="wrap">
        <div id="header">
            <div class="middle">
                <UpperNavigationButtons:NavigationButtons runat="server" />
            </div>
        </div>
        <div id="main">
            <div class="middle">
                <div class="box-top">
                </div>
                <div class="box">
                    <table align="center" >
                        <tr>
                            <td>
                                <div>
                                    <br />
                                    Email and Message that didn't go through through TimeUP Process<br /><hr />
                                    <br />
                                    <asp:GridView ID="AutomatedEmailProblemGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="MessageID"
                                        DataSourceID="AutomatedEmailSqlDataSource" OnSelectedIndexChanged="AutomatedEmailProblemGridViewSelectedIndexChanged">
                                        <Columns>
                                            <asp:BoundField DataField="MessageID" HeaderText="MessageID" ReadOnly="True" SortExpression="MessageID" />
                                            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" SortExpression="EmailAddress" />
                                            <asp:BoundField DataField="Title" HeaderText="Title" SortExpression="Title" />
                                            <asp:BoundField DataField="Message" HeaderText="Message" SortExpression="Message" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="select" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="AutomatedEmailSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                        SelectCommand="SELECT * FROM [AutomationEmailProblem]"></asp:SqlDataSource>
                                    <br />
                                </div>
                            </td>
                        </tr>
                    </table>
                    <span class="clear"></span>
                </div>
                <div class="box-bottom">
                </div>
                <span class="clear"></span>
            </div>
            <span class="clear"></span>
        </div>
        <div id="footer1">
            <LowerNavigationButtons:NavigationButtons runat="server" />
        </div>
    </div>
    </form>
</body>
</html>
