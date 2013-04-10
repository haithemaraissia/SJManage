<%@ Page Language="C#" AutoEventWireup="false" MasterPageFile="~/Plain.master" CodeFile="Refund.aspx.cs"
    Inherits="Management.Refund.AccountRefund" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <table>
            <tr>
                <td>
                    <div>
                        You need to Issue Refund for the following;
                        <br />
                        After Refund is manually submitted through paypal, Click Refund
                        <br />
                        <hr />
                        <br />
                        <div id="CustomerRefund">
                            Customer<br />
                            <br />
                            <asp:GridView ID="CustomerGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" DataKeyNames="PDTID" DataSourceID="RefundCustomerSuccessfulPDTSqlDataSource">
                                <Columns>
                                    <asp:BoundField DataField="PDTID" HeaderText="PDTID" ReadOnly="True" SortExpression="PDTID" />
                                    <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                                    <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                    <asp:BoundField DataField="PaymentFee" HeaderText="PaymentFee" SortExpression="PaymentFee" />
                                    <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                                    <asp:BoundField DataField="CustomerID" HeaderText="CustomerID" SortExpression="CustomerID" />
                                    <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="RefundCustomerSuccessfulPDTSqlDataSource" runat="server" ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>"
                                SelectCommand="SELECT * FROM [RefundCustomerSuccessfulPDT]"></asp:SqlDataSource>
                            <br />
                            <asp:LinkButton ID="RefundCustomerSuccessfulPDTLinkButton" runat="server" PostBackUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Customer/RefundCustomer.aspx">Refund Customer</asp:LinkButton>
                            <br />
                            <hr />
                            <br />
                        </div>
                        <div id="ProfessionalRefund">
                            Professional<br />
                            <br />
                            <asp:GridView ID="ProfessionalGridView" runat="server" AllowPaging="True" AllowSorting="True"
                                AutoGenerateColumns="False" DataKeyNames="PDTID" DataSourceID="RefundProfessionalSuccessfulPDTSqlDataSource">
                                <Columns>
                                    <asp:BoundField DataField="PDTID" HeaderText="PDTID" ReadOnly="True" SortExpression="PDTID"
                                        InsertVisible="False" />
                                    <asp:BoundField DataField="GrossTotal" HeaderText="GrossTotal" SortExpression="GrossTotal" />
                                    <asp:BoundField DataField="PaymentStatus" HeaderText="PaymentStatus" SortExpression="PaymentStatus" />
                                    <asp:BoundField DataField="FirstName" HeaderText="FirstName" SortExpression="FirstName" />
                                    <asp:BoundField DataField="LastName" HeaderText="LastName" SortExpression="LastName" />
                                    <asp:BoundField DataField="PaymentFee" HeaderText="PaymentFee" SortExpression="PaymentFee" />
                                    <asp:BoundField DataField="TransactionId" HeaderText="TransactionId" SortExpression="TransactionId" />
                                    <asp:BoundField DataField="ProID" HeaderText="ProID" SortExpression="ProID" />
                                    <asp:BoundField DataField="ProjectID" HeaderText="ProjectID" SortExpression="ProjectID" />
                                </Columns>
                            </asp:GridView>
                            <asp:SqlDataSource ID="RefundProfessionalSuccessfulPDTSqlDataSource" runat="server"
                                ConnectionString="<%$ ConnectionStrings:SideJobConnectionString %>" SelectCommand="SELECT * FROM [RefundProfessionalSuccessfulPDT]">
                            </asp:SqlDataSource>
                            <br />
                            <asp:LinkButton ID="RefundProfessionalSuccessfulPDTLinkButton" runat="server" PostBackUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Professional/RefundProfessional.aspx">Refund Professional</asp:LinkButton>
                            <br />
                            <hr />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
