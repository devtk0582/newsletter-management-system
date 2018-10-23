using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;

namespace NewsletterMS.Admin
{
    public partial class SelectNewsletter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindNewsletters();
            }
        }

        private void BindNewsletters()
        {
            ddlNewsletters.DataSource = (new BOPublications()).GetPublications();
            ddlNewsletters.DataTextField = "NewsletterName";
            ddlNewsletters.DataValueField = "NewsletterID";
            ddlNewsletters.DataBind();
            ddlNewsletters.Items.Insert(0, new ListItem("Select Newsletter", "0"));
        }

        protected void ddlNewsletters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlNewsletters.SelectedIndex > 0)
            {
                Session["NewsletterID"] = ddlNewsletters.SelectedValue;
                if (Request.QueryString["page"] != null)
                {
                    switch (Request.QueryString["page"].ToString())
                    {
                        case "ad":
                            Response.Redirect("~/Admin/AdEntries.aspx");
                            break;
                        case "am":
                            Response.Redirect("~/Admin/AdminMaintenance.aspx");
                            break;
                        case "av":
                            Response.Redirect("~/Admin/Advertisers.aspx");
                            break;
                        case "nl":
                            Response.Redirect("~/Admin/Publications.aspx");
                            break;
                        case "um":
                            Response.Redirect("~/Admin/UserMaintenance.aspx");
                            break;
                        case "as":
                            Response.Redirect("~/Admin/AdAssignment.aspx");
                            break;
                    }
                }
                else
                    Response.Redirect("~/Admin/EditNewsletter.aspx");
            }
        }
    }
}