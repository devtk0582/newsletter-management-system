using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;

namespace NewsletterMS.Admin
{
    public partial class Preview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["NewsletterID"] != null)
            {
                var newsletter = (new BOPublications()).GetPublicationByID(long.Parse(Session["NewsletterID"].ToString()));
                if (newsletter != null)
                {
                    hfCurrentNLID.Value = newsletter.UniqueID.HasValue ? newsletter.UniqueID.Value.ToString() : "";
                    if (!string.IsNullOrEmpty(newsletter.BackgroundColor))
                    {
                        form1.Attributes["style"] = "background-color: #" + newsletter.BackgroundColor + " !important";
                    }
                    if (!string.IsNullOrEmpty(newsletter.SectionColor))
                    {
                        hfSectionColor.Value = "#" + newsletter.SectionColor;
                    }
                }
            }
            else
            {
                Response.Redirect("~/Admin/Default.aspx");
            }
        }
    }
}