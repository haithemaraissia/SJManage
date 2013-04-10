using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using SidejobModel;

namespace Management.Refund.Professional
{
    public partial class PartOfManageRefundProfessional : System.Web.UI.Page
    {
        private int BidderLCID { get; set; }
        private string BidderUsername { get; set; }
        private string BidderRole { get; set; }
        private int BidderID { get; set; }
        private string BidderEmailAddress { get; set; }
        private int PosterLCID { get; set; }
        private string PosterUsername { get; set; }
        private string PosterRole { get; set; }
        private int PosterID { get; set; }
        private string PosterEmailAddress { get; set; }
        private int ProjectID { get; set; }
        private int ProjectLCID { get; set; }
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

        protected void ProfessionalRefundGridViewSelectedIndexChanged(object sender, EventArgs e)
        {
            //You should have refunded through paypal

            //Now; 
            //Removed from current customerrefund and inserted into archivedprofessionalrefund
            ArchiveRefund((int)ProfessionalRefundGridView.SelectedValue);
        }

        private void ArchiveRefund(int pdtid)
        {
            using (var context = new SidejobEntities())
            {
                var currentrefund = (from c in context.RefundProfessionalSuccessfulPDTs
                                     where c.PDTID == pdtid
                                     select c).FirstOrDefault();
                if (currentrefund != null)
                {
                    var rd = (from c in context.ResponseDelays
                              where c.ProjectID == currentrefund.ProjectID
                              select c).FirstOrDefault();
                    if (rd != null)
                    {
                        GetBidderPosterProjectProperties(rd);
                        var archivedrefund = new ArchivedRefundProfessionalSuccessfulPDT
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
                                                     ProID = currentrefund.ProID,
                                                     ProjectID = currentrefund.ProjectID
                                                 };
                        context.AddToArchivedRefundProfessionalSuccessfulPDTs(archivedrefund);
                        context.SaveChanges();
                        PDTID = pdtid;
                        if (rd.PosterID == currentrefund.ProID)
                        {
                            EmailPoster(Message.Refund);
                        }
                        else
                        {
                            EmailBidder(Message.Refund);
                        }
                        Response.Redirect(Context.Request.Url.ToString());
                    }
                }
            }
        }

        private void EmailBidder(Message messageType)
        {
            //Notification:
            //?l=en-US&prid=5&usn=jack&bidder=smith&rem=1
            //Reminder:
            //?l=en-US&usn=jack&rem=1
            //NewOption:(Inside and Outside Website)
            //?l=en-US&prid=5&rol=CUS&cid=28
            //New Opportunity:(Inside and Outside Website)
            //l=en-US&prid=5&pid=25
            //Refund:
            //l=en-US&pdtid=1&prid=5&pid=25
            //Blocked
            //l=fr&prid=5&cid=28
            switch (messageType)
            {

                //This will be under Manage Site
                case Message.Refund:
                    {
                        if (BidderRole == "CUS")
                        {
                            //Send Email To admin to Refund.
                            //Later will be done through API
                            SendEmail(BidderEmailAddress, BidderLCID,
                                      "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Customer/CustomerRefund.aspx", Message.Refund, "CUS", BidderID, PDTID);
                        }
                        if (BidderRole == "PRO")
                        {
                            SendEmail(BidderEmailAddress, BidderLCID,
                                      "http://www.my-side-job.com/Manage/MySideJob/Management/Refund/EmailTemplates/Professional/ProfessionalRefund.aspx", Message.Refund, "PRO", BidderID, PDTID);
                        }
                    }
                    break;
            }
        }

        private void EmailPoster(Message messageType)
        {
            //Notification:
            //?l=en-US&prid=5&usn=jack&bidder=smith&rem=1
            //Reminder:
            //?l=en-US&usn=jack&rem=1
            //NewOption:(Inside and Outside Website)
            //?l=en-US&prid=5&rol=CUS&cid=28
            //New Opportunity:(Inside and Outside Website)
            //l=en-US&prid=5&pid=25
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

        private void SendEmail(string userEmail, int lcid, string templateUrl,
                                 Message messageType, string role = "", int id = 0, int pdtid = 0)
        {
            //Refund:
            //l=en-US&pdtid=1&prid=5&cid=25

            var url = "";
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
            if (rd.BidderID != null) BidderID = (int)rd.BidderID;
            PosterID = rd.PosterID;
            ProjectID = rd.ProjectID;
            ////////////////////////POSTER BIDDER PROJECT LCID//////////////////////////
            using (var context = new SidejobEntities())
            {
                if (rd.BidderRole == "CUS")
                {
                    BidderRole = "CUS";
                    var c1 = (from c in context.Customers
                              where c.CustomerID == rd.BidderID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        BidderLCID = c1.LCID;
                        BidderUsername = c1.UserName;
                        var cg = (from c in context.CustomerGenerals
                                  where c.CustomerID == rd.BidderID
                                  select c).FirstOrDefault();
                        if (cg != null)
                        {
                            BidderEmailAddress = cg.EmailAddress;
                        }
                    }
                }

                if (rd.BidderRole == "PRO")
                {
                    BidderRole = "PRO";
                    var c1 = (from c in context.Professionals
                              where c.ProID == rd.BidderID
                              select c).FirstOrDefault();
                    if (c1 != null)
                    {
                        BidderLCID = c1.LCID;
                        BidderUsername = c1.UserName;
                        var pg = (from c in context.ProfessionalGenerals
                                  where c.ProID == rd.BidderID
                                  select c).FirstOrDefault();
                        if (pg != null)
                        {
                            BidderEmailAddress = pg.EmailAddress;
                        }
                    }
                }

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

                var p1 = (from c in context.ProjectRequirements
                          where c.ProjectID == ProjectID
                          select c).FirstOrDefault();
                if (p1 != null) ProjectLCID = p1.LCID;
            }

        }
        ////////////////////////Get Bidder Poster Project Properties//////////////////////////


    }
}