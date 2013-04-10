<%@ Page Title="" Language="C#" MasterPageFile="~/Plain.master" AutoEventWireup="true" CodeFile="ArchievedCustomerSuccessfulPDT.aspx.cs" Inherits="Management_Transaction_Customer_ArchievedCustomerSuccessfulPDT" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        Archieved<br />
        <br />
        <asp:GridView runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataKeyNames="PDTID" DataSourceID="ArchievedCustomerSuccessfulPDTSqlDataSource">
            <Columns>
                <asp:BoundField DataField="PDTID" HeaderText="PDTID" InsertVisible="False" ReadOnly="True"
                    SortExpression="PDTID" />
                <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                <asp:BoundField DataField="Invoice" HeaderText="Invoice" SortExpression="Invoice" />
                <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                <asp:BoundField DataField="CurrencyCode" HeaderText="CurrencyCode" SortExpression="CurrencyCode" />
                <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="ArchievedCustomerSuccessfulPDTSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
            SelectCommand="SELECT * FROM [ArchivedCustomerSuccessfulPDT]"></asp:SqlDataSource>
    </div>
</asp:Content>
