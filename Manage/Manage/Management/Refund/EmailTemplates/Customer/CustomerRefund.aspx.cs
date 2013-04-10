using System;
using System.Globalization;
using System.Threading;
using System.Linq;
using SidejobModel;

namespace Management.Refund.EmailTemplates.Customer
{
    public partial class EmailTemplatesCustomerCustomerRefund : System.Web.UI.Page
    {
        private int ProjectID { get; set; }
        private int PDTID { get; set; }
        private int CustomerID { get; set; }

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
            if (!Page.IsPostBack)
            {
                MessageProperties();
            }
        }

        private void MessageProperties()
        {
            //GET THE PDTID FORM ARCHIVE REFUND
            const string singlespace = " ";
            const string startofBoldRed = "<span class='DarkRed'><strong>";
            const string lastofBoldRed = "</strong></span>";
            PDTID = Convert.ToInt32(Request.QueryString["pdtid"]);
            ProjectID = Convert.ToInt32(Request.QueryString["prid"]);
            CustomerID = Convert.ToInt32(Request.QueryString["cid"]);
            if (ProjectID == 0) return;
            ProjectNotification.Text = Resources.Resource.Project + singlespace + ProjectID + singlespace +
                                       Resources.Resource.Notification;
            ConfirmationEmail.Text = Resources.Resource.RefundEmailMessage1 + singlespace
                                     + startofBoldRed + ProjectID + lastofBoldRed + singlespace +
                                     Resources.Resource.RefundEmailMessage2;
            using (var context = new SidejobEntities())
            {
                var currentrefund = (from c in context.RefundCustomerSuccessfulPDTs
                                     where c.PDTID == PDTID
                                     select c).FirstOrDefault();
                if (currentrefund == null) return;
                Amount.Text = currentrefund.GrossTotal.ToString(CultureInfo.InvariantCulture) +
                              singlespace + currentrefund.CurrencyCode.ToString(CultureInfo.InvariantCulture) + singlespace;
                Transaction.Text = currentrefund.TransactionId;
                NameLabel.Text = currentrefund.FirstName + singlespace + currentrefund.LastName;
                context.DeleteObject(currentrefund);
                context.SaveChanges();
            }

        }
    }
}