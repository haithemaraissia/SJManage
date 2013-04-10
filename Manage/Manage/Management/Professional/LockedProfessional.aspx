<%@ Page Title="" Language="VB" MasterPageFile="~/Plain.master" AutoEventWireup="false" CodeFile="LockedProfessional.aspx.vb" Inherits="Management.Professional.ManagementProfessionalLockedProfessional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    Lock Professional Update<br />
    <br />
    <asp:GridView ID="ProfessionalGridView" runat="server" AutoGenerateColumns="False" 
        DataKeyNames="ID" DataSourceID="ProfessionalSqlDataSource">
        <Columns>
            <asp:BoundField DataField="ID" HeaderText="ID" InsertVisible="False" 
                ReadOnly="True" SortExpression="ID" />
            <asp:BoundField DataField="EmailAddress" HeaderText="EmailAddress" 
                SortExpression="EmailAddress" />
            <asp:BoundField DataField="Reason" HeaderText="Reason" 
                SortExpression="Reason" />
            <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date" />
            <asp:BoundField DataField="IP" HeaderText="IP" SortExpression="IP" />
            <asp:BoundField DataField="ProID" HeaderText="ProID" 
                SortExpression="ProID" />
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
    <asp:SqlDataSource ID="ProfessionalSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
        SelectCommand="SELECT LockedProfessional.* FROM LockedProfessional"
        OldValuesParameterFormatString="original_{0}">
    </asp:SqlDataSource>
</asp:Content>