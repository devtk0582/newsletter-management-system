using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using NewsletterMSBLL;

namespace NewsletterMS
{
    public partial class _Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["AdminUserID"] != null && Session["UserName"] != null)
                {
                    lblTitle.Text = Session["UserName"].ToString();
                    hfNewsletterID.Value = (new BOPublications()).GetPublicationByLocalAdmin(int.Parse(Session["AdminUserID"].ToString())).ToString();
                    if (Session["Role"].ToString() == "S")
                    {
                        SuperAdminMenu.Visible = true;
                        LocalAdminMenu.Visible = false;
                        BindNewsletters();
                    }
                    else
                    {
                        SuperAdminMenu.Visible = false;
                        LocalAdminMenu.Visible = true;
                    }
                }
                else
                {
                    FormsAuthentication.RedirectToLoginPage();
                }
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
            Session["NewsletterID"] = ddlNewsletters.SelectedValue;
            Response.Redirect("~/Admin/EditNewsletter.aspx");
        }

        protected void ManageSiteLink_Click(object sender, EventArgs e)
        {
            if (hfNewsletterID.Value != "" && long.Parse(hfNewsletterID.Value) > 0)
            {
                Session["NewsletterID"] = long.Parse(hfNewsletterID.Value);
                Response.Redirect("~/Admin/EditNewsletter.aspx");
            }
        }

        protected void UserMaintenanceLink_Click(object sender, EventArgs e)
        {
            if (hfNewsletterID.Value != "" && long.Parse(hfNewsletterID.Value) > 0)
            {
                Session["NewsletterID"] = long.Parse(hfNewsletterID.Value);
                Response.Redirect("~/Admin/UserMaintenance.aspx");
            }
        }
    }
}
