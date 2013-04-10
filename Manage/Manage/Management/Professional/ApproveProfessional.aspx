<%@ Page Title="" Language="VB" MasterPageFile="~/Plain.master" AutoEventWireup="false" CodeFile="ApproveProfessional.aspx.vb" Inherits="Management.Professional.ManagementProfessionalApproveProfessional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    Approve/Disapprove Professional Portfolio Change<br />
    <br />
    <asp:GridView ID="ProfessionalGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="ProID"
        DataSourceID="ProfessionalSqlDataSource" Style="position: relative; top: 0px; left: 0px;
        width: 850px; text-align: center;">
        <Columns>
            <asp:BoundField DataField="ProID" HeaderText="ProID" ReadOnly="True" SortExpression="ProID" />
            <asp:BoundField DataField="About" HeaderText="About" SortExpression="About" />
            <asp:BoundField DataField="SpecialNotes" HeaderText="SpecialNotes" SortExpression="SpecialNotes" />
            <asp:BoundField DataField="Modified" HeaderText="Modified" SortExpression="Modified" />
            <asp:TemplateField HeaderText="Accept">
                <ItemTemplate>
                    <asp:Button ID="AcceptButton" runat="server" CausesValidation="false" CommandName="select"
                        Text="Accept" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="ProfessionalSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
        SelectCommand="SELECT [ProID], [About], [SpecialNotes], [Modified] FROM [ProfessionalPortfolio] WHERE ([Modified] = @Modified)"
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter DefaultValue="3" Name="Modified" Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>