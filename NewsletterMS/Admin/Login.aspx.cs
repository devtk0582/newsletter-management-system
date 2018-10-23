using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;
using System.Web.Security;

namespace NewsletterMS
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUserID.Focus();
            }
        }

        protected void lbSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    AdminUser user = (new BOAdmins()).AuthenticateUser(txtUserID.Text.Trim(), txtPassword.Text.Trim());
                    if (user != null)
                    {
                        Session["AdminUserID"] = user.AdminUserID;
                        Session["UserID"] = user.UserID;
                        Session["Email"] = user.ContactEmail;
                        Session["UserName"] = user.Name;
                        Session["Role"] = user.Role;
                        if (user.NewsletterID.HasValue)
                            Session["NewsletterID"] = user.NewsletterID.Value;
                        FormsAuthentication.RedirectFromLoginPage(txtUserID.Text.Trim(), false);
                        //Response.Redirect("~/Default.aspx");
                    }
                    else
                    {
                        lblErrMsg.Text = "Invalid user id or password.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblErrMsg.Text = ex.Message;
            }
        }
    }
}