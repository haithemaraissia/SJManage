<%@ Page Title="" Language="VB" MasterPageFile="~/Plain.master" AutoEventWireup="false"
    CodeFile="Project.aspx.vb" Inherits="Management.Project.ManagementProjectProject" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <br />
    Project Pending Update<br />
    <br />
    <asp:GridView ID="ProjectGridView" runat="server" AutoGenerateColumns="False" DataKeyNames="ProjectID"
        DataSourceID="ProjectSqlDataSource" Style="position: relative; top: 0px; left: 0px;
        width: 850px; text-align: center;">
        <Columns>
            <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID"
                ReadOnly="True" />
            <asp:BoundField DataField="LCID" HeaderText="LCID" SortExpression="LCID" />
            <asp:BoundField DataField="ProjectTitle" HeaderText="ProjectTitle" SortExpression="ProjectTitle" />
            <asp:BoundField DataField="StartDate" HeaderText="StartDate" SortExpression="StartDate" />
            <asp:BoundField DataField="EndDate" HeaderText="EndDate" SortExpression="EndDate" />
            <asp:BoundField DataField="Description" HeaderText="Description" SortExpression="Description" />
            <asp:BoundField DataField="SpecialNotes" HeaderText="SpecialNotes" SortExpression="SpecialNotes" />
            <asp:TemplateField HeaderText="Accept">
                <ItemTemplate>
                    <asp:Button ID="AcceptButton" runat="server" CausesValidation="false" CommandName="AcceptChange"
                        Text="Accept" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Deny">
                <ItemTemplate>
                    <asp:Button ID="DenyButton" runat="server" CausesValidation="false" CommandName="DenyChange"
                        Text="Deny Change" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Lock Project">
                <ItemTemplate>
                    <asp:Button ID="LockButton" runat="server" CausesValidation="false" CommandName="LockProject"
                        Text="LockUser" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <asp:SqlDataSource ID="ProjectSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
        SelectCommand="SELECT [ProjectID], [LCID], [JobTitle], [ProjectTitle], [StartDate], [EndDate], [Description], [SpecialNotes], [Address], [DatePosted] FROM [PendingProject]"  >
        <DeleteParameters>
            <asp:Parameter Name="ProjectID" Type="Int32" />
        </DeleteParameters>
    </asp:SqlDataSource>
</asp:Content>
