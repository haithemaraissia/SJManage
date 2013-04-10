<%@ Page Title="" Language="C#" MasterPageFile="~/Plain.master" AutoEventWireup="true" CodeFile="ManagementEmailProblem.aspx.cs" Inherits="Management.Message.ManagementTimeUpManagementEmailProblem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
  
    <div>
                                    <br />
                                    Email and Message that didn't go through through Management Process<br /><hr />
                                    <br />
                                    <asp:GridView ID="EmailSentExceptionGridView" runat="server" 
                                        AutoGenerateColumns="False" DataKeyNames="ID"
                                        DataSourceID="EmailSentExceptionSqlDataSource" onselectedindexchanged="EmailSentExceptionGridViewSelectedIndexChanged" 
                                        >
                                        <Columns>
                                            <asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" 
                                                SortExpression="ID" InsertVisible="False" />
                                            <asp:BoundField DataField="DateTime" HeaderText="DateTime" 
                                                SortExpression="DateTime" />
                                            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" 
                                                SortExpression="EmailAddress" />
                                            <asp:BoundField DataField="Reason" HeaderText="Reason" 
                                                SortExpression="Reason" />
                                            <asp:BoundField DataField="UserId" HeaderText="UserId" 
                                                SortExpression="UserId" />
                                            <asp:BoundField DataField="UserRole" HeaderText="UserRole" 
                                                SortExpression="UserRole" />
                                                  <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="select" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <asp:SqlDataSource ID="EmailSentExceptionSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                        SelectCommand="SELECT * FROM [EmailSentException]"></asp:SqlDataSource>
                                    <br />
                                </div>
                                
                                
  <%--                               <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="DeleteButton" runat="server" Text="Delete" CommandName="select" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
    
</asp:Content>
