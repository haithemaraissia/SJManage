<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/Plain.master" CodeFile="RefundProfessional.aspx.cs"
    Inherits="Management.Refund.Professional.PartOfManageRefundProfessional" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        Current<br />
        <br />
        <asp:GridView ID="ProfessionalRefundGridView" runat="server" AllowPaging="True" AllowSorting="True"
            AutoGenerateColumns="False" DataKeyNames="PDTID" DataSourceID="RefundProfessionalSuccessfulPDTSqlDataSource"
            OnSelectedIndexChanged="ProfessionalRefundGridViewSelectedIndexChanged">
            <Columns>
                <asp:BoundField DataField="PDTID" HeaderText="PDTID" ReadOnly="True" SortExpression="PDTID" />
                <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                <asp:BoundField DataField="Invoice" HeaderText="Invoice" SortExpression="Invoice" />
                <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="RefundButton" runat="server" Text="Refund" CommandName="select" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:SqlDataSource ID="RefundProfessionalSuccessfulPDTSqlDataSource" runat="server"
            ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" SelectCommand="SELECT * FROM [RefundProfessionalSuccessfulPDT]">
        </asp:SqlDataSource>
        <br />
        <hr />
        Go To Archived:
        <asp:HyperLink ID="ProfessionalArchivedRefundHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Professional/ArchievedRefundedProfessional.aspx">Professional Archived Refund</asp:HyperLink>
    </div>
</asp:Content>
