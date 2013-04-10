<%@ Page Title="" Language="VB" MasterPageFile="~/Plain.master" AutoEventWireup="false"
    CodeFile="Customer.aspx.vb" Inherits="Management.Customer.ManagementUserCustomer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    Customer Portfolio Management<br />
    <br />
    <asp:GridView ID="CustomerGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="CustomerID"
        DataSourceID="CustomerSqlDataSource" Style="position: relative; top: 0px; left: 0px;
        width: 850px; text-align: center;">
        <Columns>
            <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" ReadOnly="True" SortExpression="CustomerID" />
            <asp:BoundField DataField="About" HeaderText="About" SortExpression="About" />
            <asp:BoundField DataField="SpecialNotes" HeaderText="SpecialNotes" SortExpression="SpecialNotes" />
            <asp:BoundField DataField="Modified" HeaderText="Modified" SortExpression="Modified" />
            <asp:TemplateField HeaderText="Accept">
                <ItemTemplate>
                    <asp:Button ID="AcceptButton" runat="server" CausesValidation="false" CommandName="AcceptChange"
                        Text="Accept" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deny">
                <ItemTemplate>
                    <asp:Button ID="DenyButton" runat="server" CausesValidation="false" CommandName="DenyChange"
                        Text="DenyChange" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lock User">
                <ItemTemplate>
                    <asp:Button ID="LockButton" runat="server" CausesValidation="false" CommandName="LockUser"
                        Text="LockUser" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="CustomerSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
        SelectCommand="SELECT [CustomerID], [About], [SpecialNotes], [Modified] FROM [CustomerPortfolio] WHERE ([Modified] = @Modified)"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="Modified" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>
