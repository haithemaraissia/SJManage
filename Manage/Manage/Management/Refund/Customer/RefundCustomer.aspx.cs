using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using SidejobModel;

namespace Management.Refund.Customer
{
    public partial class PartOfManageRefundCustomer : System.Web.UI.Page
    {
        private string PosterUsername { get; set; }
        private int PosterLCID { get; set; }
        private string PosterRole { get; set; }
        private int PosterID { get; set; }
        private string PosterEmailAddress { get; set; }
        private int ProjectID { get; set; }
        private int PDTID { get; set; }

        private enum Message
        {
            Refund
        };

        protected override void InitializeCulture()
        {
            var lang = Request.QueryString["l"];
            if (lang != null | !string.IsNullOrEmpty(lang))
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
                Session["LCID"] = ManageUtility.GetLCID(lang);
            }
            else
            {
                if (Session["LCID"] != null)
                {
                    Thread.CurrentThread.CurrentUICulture =
                        new CultureInfo(ManageUtility.GetLanguage(Convert.ToInt32(Session["LCID"])));
                    Thread.CurrentThread.CurrentCulture =
                        CultureInfo.CreateSpecificCulture(ManageUtility.GetLanguage(Convert.ToInt32(Session["LCID"])));
                }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["LCID"] == null)
            {
                Session["LCID"] = 1;
            }
        }

        protected void CustomerRefundGridViewSelectedIndexChanged(object sender, EventArgs e)
        {
            //You should have refunded through paypal

            //Now; 
            //Removed from current customerrefund and inserted into archivedCustomerrefund
            ArchiveRefund((int)CustomerRefundGridView.SelectedValue);
        }

        private void ArchiveRefund(int pdtid)
        {
            using (var context = new SidejobEntities())
            {
                var currentrefund = (from c in context.RefundCustomerSuccessfulPDTs
                                     where c.PDTID == pdtid
                                     select c).FirstOrDefault();
                if (currentrefund == null) return;
                var rd = (from c in context.ResponseDelays
                          where c.ProjectID == currentrefund.ProjectID
                          select c).FirstOrDefault();
                if (rd == null) return;
                GetBidderPosterProjectProperties(rd);
                var archivedrefund = new ArchivedRefundCustomerSuccessfulPDT
                                         {
                                             PDTID = currentrefund.PDTID,
                                             GrossTotal = currentrefund.GrossTotal,
                                             Invoice = currentrefund.Invoice,
                                             PaymentStatus = currentrefund.PaymentStatus,
                                             FirstName = currentrefund.FirstName,
                                             LastName = currentrefund.LastName,
                                             PaymentFee = currentrefund.PaymentFee,
                                             BusinessEmail = currentrefund.BusinessEmail,
                                             TxToken = currentrefund.TxToken,
                                             ReceiverEmail = currentrefund.ReceiverEmail,
                                             ItemName = currentrefund.ItemName,
                                             CurrencyCode = currentrefund.CurrencyCode,
                                             TransactionId = currentrefund.TransactionId,
                                             Custom = currentrefund.Custom,
                                             subscriberId = currentrefund.subscriberId,
                                             CustomerID = currentrefund.CustomerID,
                                             ProjectID = currentrefund.ProjectID
                                         };
                context.AddToArchivedRefundCustomerSuccessfulPDTs(archivedrefund);
                context.SaveChanges();
                PDTID = pdtid;
                EmailPoster(Message.Refund);
                Response.Redirect(Context.Request.Url.ToString());
            }
        }

        private void EmailPoster( Message messageType)
        {

            //Refund:
            //l=en-US&pdtid=1&prid=5&pid=25
            //Blocked
            //l=fr&prid=5&cid=28
            switch (messageType)
            {

                    //This will be under Manage Site
                case Message.Refund:
                    {
                        if (PosterRole == "CUS")
                        {
                            //Send Email To admin to Refund.
                            //Later will be done through API
                            SendEmail(PosterEmailAddress, PosterLCID,
                                      "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Customer/CustomerRefund.aspx", Message.Refund, "CUS", PosterID, PDTID);
                        }
                        if (PosterRole == "PRO")
                        {
                            SendEmail(PosterEmailAddress, PosterLCID,
                                      "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Professional/ProfessionalRefund.aspx", Message.Refund, "PRO", PosterID, PDTID);
                        }
                    }
                    break;
            }
        }

        private void SendEmail(string userEmail, int lcid, string templateUrl,  Message messageType, string role = "", int id = 0, int pdtid = 0)
        {
            //Refund:
            //l=en-US&pdtid=1&prid=5&cid=25


            string url = "";
            switch (messageType)
            {
                case Message.Refund:
                    if (role == "CUS")
                    {
                        url = templateUrl + "?&l=" + ManageUtility.GetLanguage(lcid) + "&pdtid=" + pdtid + "&prid=" + ProjectID + "&cid=" + id;
                    }
                    if (role == "PRO")
                    {
                        url = templateUrl + "?&l=" + ManageUtility.GetLanguage(lcid) + "&pdtid=" + pdtid + "&prid=" + ProjectID + "&pid=" + id;
                    }
                    break;


            }

            const string strFrom = "postmaster@my-side-job.com";
            var mailMsg = new MailMessage(new MailAddress(strFrom), new MailAddress(userEmail))
                              {
                                  BodyEncoding = Encoding.Default,
                                  Subject = Resources.Resource.Notification,
                                  Body = ManageUtility.GetHtmlFrom(url),
                                  Priority = MailPriority.High,
                                  IsBodyHtml = true
                              };
            var smtpMail = new SmtpClient();
            var basicAuthenticationInfo = new NetworkCredential("postmaster@my-side-job.com", "haithem759163");
            smtpMail.Host = "mail.my-side-job.com";
            smtpMail.UseDefaultCredentials = false;
            smtpMail.Credentials = basicAuthenticationInfo;
            try
            {
                smtpMail.Send(mailMsg);
            }
            catch (Exception)
            {
                Response.Redirect(Request.Url.ToString());
                throw;
            }
        }


        /////////////////////Get Bidder Poster Project Properties/////////////////////////
        private void GetBidderPosterProjectProperties(ResponseDelay rd)
        {
            if (rd.PosterID == 0) return;
            PosterID = rd.PosterID;
            ProjectID = rd.ProjectID;
            ////////////////////////POSTER BIDDER PROJECT LCID//////////////////////////
            using (var context = new SidejobEntities())
            {
                if (rd.PosterRole == "CUS")
                {
                    PosterRole = "CUS";
                    var c1 = (from c in context.Customers
                              where c.CustomerID == PosterID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        PosterLCID = c1.LCID;
                        PosterUsername = c1.UserName;
                        var cg = (from c in context.CustomerGenerals
                                  where c.CustomerID == rd.PosterID
                                  select c).FirstOrDefault();
                        if (cg != null)
                        {
                            PosterEmailAddress = cg.EmailAddress;
                        }
                    }
                }

                if (rd.PosterRole == "PRO")
                {
                    PosterRole = "PRO";
                    var c1 = (from c in context.Professionals
                              where c.ProID == PosterID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        PosterLCID = c1.LCID;
                        PosterUsername = c1.UserName;
                        var pg = (from c in context.ProfessionalGenerals
                                  where c.ProID == rd.BidderID
                                  select c).FirstOrDefault();
                        if (pg != null)
                        {
                            PosterEmailAddress = pg.EmailAddress;
                        }
                    }
                }
            }

        }
        ////////////////////////Get Bidder Poster Project Properties//////////////////////////


    }
}