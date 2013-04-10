using System;
using System.Linq;
using SidejobModel;

namespace Management.Message
{
    public partial class ManagementTimeUpManagementEmailProblem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void EmailSentExceptionGridViewSelectedIndexChanged(object sender, EventArgs e)
        {
            //Issue is fixed, then delete it
            if (EmailSentExceptionGridView.SelectedDataKey == null) return;
            var selected = (int)EmailSentExceptionGridView.SelectedDataKey.Value;

            using (var context = new SidejobEntities())
            {
                var current = (from c in context.EmailSentExceptions
                               where c.ID == selected
                               select c).FirstOrDefault();
                if (current != null)
                {
                    context.DeleteObject(current);
                    context.SaveChanges();
                    Response.Redirect(Context.Request.Url.ToString());
                }

            }
        }
    }
}