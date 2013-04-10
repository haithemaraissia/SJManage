<%@ Page Title="" Language="VB" MasterPageFile="~/Plain.master" AutoEventWireup="false" CodeFile="LockedCustomer.aspx.vb" Inherits="Management.Customer.ManagementCustomerLockedCustomer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    Locked Customer Update<br />
    <br />
    <asp:GridView ID="CustomerGridView" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID" DataSourceID="CustomerSqlDataSource">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" 
                SortExpression="EmailAddress" />
            <asp:BoundField DataField="Reason" HeaderText="Reason" 
                SortExpression="Reason" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
            <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" 
                SortExpression="CustomerID" />
            <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" 
                SortExpression="ProjectID" />
                   <asp:TemplateField HeaderText="Accept">
                <ItemTemplate>
                    <asp:Button ID="AcceptButton" runat="server" CausesValidation="false" CommandName="select"
                        Text="Unlock" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="CustomerSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
        SelectCommand="SELECT LockedCustomer.* FROM LockedCustomer"
        OldValuesParameterFormatString="original_{0}">
    </asp:SqlDataSource>
</asp:Content>
