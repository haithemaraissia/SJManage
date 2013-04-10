<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TemplateMainUpperButtons.ascx.vb"
    Inherits="common.TemplateMainUpperButtons" %>
<link href="http://my-side-job.com/Manage/MySideJob/Styles/Site.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="http://my-side-job.com/Manage/MySideJob/Styles/Menustyle.css" type="text/css" />
<!--[if lte IE 7]>
        <link rel="stylesheet" type="text/css" href=Styles/ie.css" media="screen" />
    <![endif]-->
<script type="text/javascript" src="http://my-side-job.com/Manage/MySideJob/Scripts/jquery-1.4.1.min.js"></script>
<script type="text/javascript" language="javascript" src="http://my-side-job.com/Manage/MySideJob/Scripts/jquery.dropdownPlain.js"></script>
<div id="page-wrap">
    <ul class="dropdown" style="z-index: 100">
        <li><asp:HyperLink ID="HomeHyperlink"  runat="server" NavigateUrl="http://my-side-job.com/Manage/MySideJob/Default.aspx" 
            Text="<%$ Resources:Resource, Home %>"></asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="UserHyperLink" runat="server" NavigateUrl="" Text="<%$ Resources:Resource, User %>"></asp:HyperLink>
            <ul>
                <li>
                <asp:HyperLink ID="CustomerHyperLink" runat="server" 
                NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Customer/Customer.aspx" 
                Text="<%$ Resources:Resource, Customer %>"></asp:HyperLink>
                    <ul>
                        <li>
                        <asp:HyperLink ID="DeniedCustomerHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Customer/DeniedCustomer.aspx" Text="<%$ Resources:Resource, DeniedCustomer %>"></asp:HyperLink>
                        </li>
                        <li>
                        <asp:HyperLink ID="LockedCustomerHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Customer/LockedCustomer.aspx" Text="<%$ Resources:Resource, LockedCustomer %>"></asp:HyperLink>
                        </li>
                        <li>
                           <asp:HyperLink ID="ApproveCustomerHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Customer/ApproveCustomer.aspx" Text="<%$ Resources:Resource, ApproveCustomer %>"></asp:HyperLink>
                       </li>
                    </ul>
                </li>
                <li>
                <asp:HyperLink ID="ProfessionalHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Professional/Professional.aspx" 
                Text="<%$ Resources:Resource, Professional %>"></asp:HyperLink>
                    <ul>
                        <li>
                        <asp:HyperLink ID="DeniedProfessionalHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Professional/DeniedProfessional.aspx" Text="<%$ Resources:Resource, DeniedProfessional %>"></asp:HyperLink>
                        </li>
                        <li>
                        <asp:HyperLink ID="LockedProfessionalHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Professional/LockedProfessional.aspx" Text="<%$ Resources:Resource, LockedProfessional %>"></asp:HyperLink>
                        </li>
                                                <li>
                           <asp:HyperLink ID="ApproveProfessionalHyperLink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Professional/ApproveProfessional.aspx" Text="<%$ Resources:Resource, ApproveProfessional %>"></asp:HyperLink>
                       </li>
                    </ul>
                </li>
                
                
                
                
                 <li>
                <asp:HyperLink ID="MessageHyperlink" runat="server"  NavigateUrl="" Text="<%$ Resources:Resource, Message %>"></asp:HyperLink>
                 <ul>
                        <li>
                        <asp:HyperLink ID="AutomatedEmailProblemHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Message/AutomatedEmailProblem.aspx" Text="<%$ Resources:Resource, AutomatedEmailProblem %>"></asp:HyperLink>
                        </li>
                        <li>
                        <asp:HyperLink ID="ManagementEmailProblemHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Message/ManagementEmailProblem.aspx" Text="<%$ Resources:Resource, ManagementEmailProblem %>"></asp:HyperLink>
                        </li>
                    </ul>
                </li>
                <li>
                <asp:HyperLink ID="RefundHyperLink" runat="server" 
                NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Refund.aspx" 
                Text="<%$ Resources:Resource, Refund %>"></asp:HyperLink>
                 <ul>
                        <li>
                        <asp:HyperLink ID="CustomerRefundHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Customer/RefundCustomer.aspx" Text="<%$ Resources:Resource, RefundCustomer %>"></asp:HyperLink>
                        </li>
                        <li>
                        <asp:HyperLink ID="ArchivedCustomerRefundHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Customer/ArchievedRefundedCustomer.aspx" Text="<%$ Resources:Resource, ArchievedRefundCustomer %>"></asp:HyperLink>
                        </li>
                        <li>
                        <asp:HyperLink ID="ProfessionalRefundHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Professional/RefundProfessional.aspx" Text="<%$ Resources:Resource, RefundProfessional %>"></asp:HyperLink>
                        </li>
                         <li>
                        <asp:HyperLink ID="ArchivedProfessionalRefundHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Refund/Professional/ArchievedRefundedProfessional.aspx" Text="<%$ Resources:Resource, ArchievedRefundProfessional %>"></asp:HyperLink>
                        </li>
                    </ul>
                </li>
                <li>
                <asp:HyperLink ID="TransactionHyperlink" runat="server" 
                NavigateUrl="" 
                Text="<%$ Resources:Resource, Transaction %>"></asp:HyperLink>
                <ul>
                        <li>
                        <asp:HyperLink ID="CustomerSuccesfulPDTHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Transaction/Customer/CustomerSuccessfulPDT.aspx" Text="<%$ Resources:Resource, CustomerTransaction %>"></asp:HyperLink>
                        </li>
                                                <li>
                        <asp:HyperLink ID="ArchivedCustomerSuccesfulPDTHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Transaction/Customer/ArchievedCustomerSuccessfulPDT.aspx" Text="<%$ Resources:Resource, ArchievedCustomerTransaction %>"></asp:HyperLink>
                        </li>
                        <li>
                        <asp:HyperLink ID="ProfessionalSuccesfulPDTHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Transaction/Professional/ProfessionalSuccessfulPDT.aspx" Text="<%$ Resources:Resource, ProfessionalTransaction %>"></asp:HyperLink>
                        </li>
                                                <li>
                        <asp:HyperLink ID="ArchivedProfessionalSuccesfulPDTHyperlink" runat="server" NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Transaction/Professional/ArchievedProfessionalSuccessfulPDT.aspx" Text="<%$ Resources:Resource, ArchievedProfessionalTransaction %>"></asp:HyperLink>
                        </li>
                    </ul>
                </li>
                </ul>
                </li>
        <li><asp:HyperLink ID="ProjectHyperLink" runat="server" NavigateUrl="" Text="<%$ Resources:Resource, Project %>"></asp:HyperLink>
            <ul>
                <li>
                <asp:HyperLink ID="ProjectHyperLink2" runat="server" 
                NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Project/Project.aspx" Text="<%$ Resources:Resource, Project %>">
                </asp:HyperLink>
                    <ul>
                        <li>
                        <asp:HyperLink ID="DeniedProjectHyperLink" runat="server" 
                        NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Project/DeniedProject.aspx" 
                        Text="<%$ Resources:Resource, DeniedProject %>"></asp:HyperLink>
                        </li>
                        <li>
                        <asp:HyperLink ID="LockedProjectHyperLink" runat="server" 
                        NavigateUrl="http://www.my-side-job.com/Manage/MySideJob/Management/Project/LockedProject.aspx" 
                        Text="<%$ Resources:Resource, LockedProject %>">
                        </asp:HyperLink>
                        </li>
                    </ul>
                </li>
            </ul>
        </li>
       <li><asp:HyperLink ID="AccountHyperLink" runat="server" NavigateUrl="" Text="<%$ Resources:Resource, Account %>"></asp:HyperLink>
            <ul>
                <li>
                    <asp:HyperLink ID="ViewProfileHyperLink" runat="server" NavigateUrl="http://my-side-job.com/Manage/MySideJob/Management/Profile/ViewProfile.aspx" 
                    Text="<%$ Resources:Resource, ViewProfile %>"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="EditProfileHyperLink" runat="server" NavigateUrl="http://my-side-job.com/Manage/MySideJob/Account/EditProfile.aspx" 
                    Text="<%$ Resources:Resource, EditProfile %>"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="DeleteProfileHyperLink" runat="server" NavigateUrl="http://my-side-job.com/Manage/MySideJob/Account/DeleteProfile.aspx" 
                    Text="<%$ Resources:Resource, DeleteProfile %>"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="ManagePhotoHyperLink" runat="server" NavigateUrl="http://my-side-job.com/Manage/MySideJob/Account/ManagePhoto.aspx" 
                    Text="<%$ Resources:Resource, ManagePhoto %>"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="ChangePasswordHyperLink" runat="server" NavigateUrl="http://my-side-job.com/Manage/MySideJob/Account/ChangePassword.aspx" 
                    Text="<%$ Resources:Resource, ChangePassword %>"></asp:HyperLink>
                </li>
                <li>
                    <asp:HyperLink ID="LogOutHyperLink" runat="server" NavigateUrl="http://my-side-job.com/Manage/MySideJob/Account/LogOut.aspx" 
                    Text="<%$ Resources:Resource, LogOut %>"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li style="width: 132px"><asp:HyperLink ID="HelpHyperlink" runat="server" NavigateUrl="" 
                Text="<%$ Resources:Resource, Help %>"></asp:HyperLink>
            <ul>
                <li><a href="http://www.my-side-job.com/Manage/MySideJob/AboutUs.aspx">About Us</a></li>
                <li><a href="http://www.my-side-job.com/Manage/MySideJob/WhiteBoard.aspx">WhiteBoard</a></li>
                <li><a href="http://www.my-side-job.com/Manage/MySideJob/ContactUs.aspx">Contact Us</a></li>
            </ul>
        </li>
        	<li style="float:right">
        	    <a href="#">
        	         <img id="selected" alt="earth" src="http://www.my-side-job.com/Images/flags/earth.png" runat="server" height="42" width="42" />
        	     </a>
        		<ul>
        		     <li><a href="?l=en-US"><img src="http://www.my-side-job.com/Images/flags/US.png" <asp:Label ID="Label1" ForeColor="White" runat="server" Text="<%$ Resources:Resource, English %>" Font-Size="Smaller"></asp:Label>  </></a></li>
                                                <li><a href="?l=fr"><img src="http://www.my-side-job.com/Images/flags/FR.png" <asp:Label ID="Label2" runat="server" Text="<%$ Resources:Resource, French %>" Font-Size="Smaller"></asp:Label>  </></a></li>
                                                <li><a href="?l=es"><img src="http://www.my-side-job.com/Images/flags/ES.png"  <asp:Label ID="Label3" runat="server" Text="<%$ Resources:Resource, Spanish %>" Font-Size="Smaller"></asp:Label>  </></a></li>
                                                <li><a href="?l=zh-CN"><img src="http://www.my-side-job.com/Images/flags/CN.png"  <asp:Label ID="Label4" runat="server" Text="<%$ Resources:Resource, Chinese %>" Font-Size="Smaller"></asp:Label>  </></a></li>
                                                <li><a href="?l=ru"><img src="http://www.my-side-job.com/Images/flags/RU.png"  <asp:Label ID="Label5" runat="server" Text="<%$ Resources:Resource, Russian %>" Font-Size="Smaller"></asp:Label></>  </a></li>
                                                <li><a href="?l=ar"><img src="http://www.my-side-job.com/Images/flags/AE.png" <asp:Label ID="Label6" runat="server" Text="<%$ Resources:Resource, Arabic %>" Font-Size="Smaller"></asp:Label></>  </a></li>
                                                <li><a href="?l=ja"><img src="http://www.my-side-job.com/Images/flags/JP.png" <asp:Label ID="Label7" runat="server" Text="<%$ Resources:Resource, Japanese %>" Font-Size="Smaller"></asp:Label></>  </a></li>
                                                <li><a href="?l=de"><img src="http://www.my-side-job.com/Images/flags/DE.png" <asp:Label ID="Label8" runat="server" Text="<%$ Resources:Resource, German %>" Font-Size="Smaller"></asp:Label></>  </a></li>
                            </ul>
        	</li>
        </ul>
	</div>