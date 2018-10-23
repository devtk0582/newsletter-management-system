using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;

namespace NewsletterMS
{
    public partial class AdminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }

        protected void lbSignOut_Click(object sender, EventArgs e)
        {
            Session["AdminUserID"] = null;
            Session["UserID"] = null;
            Session["Email"] = null;
            Session["UserName"] = null;
            Session["Role"] = null;
            Session["NewsletterID"] = null;
            Session.Clear();
            Response.Redirect("~/Admin/Login.aspx");
        }

        protected void lbChangeProfile_Click(object sender, EventArgs e)
        {
            if (Session["AdminUserID"] != null)
            {
                long adminUserId = long.Parse(Session["AdminUserID"].ToString());
                PopupChangeProfile(adminUserId);
            }
        }

        private void PopupChangeProfile(long adminUserId)
        {
            try
            {
                hfAdminID.Value = adminUserId.ToString();
                var adminUser = (new BOAdmins()).GetAdminByID(adminUserId);
                if (adminUser != null)
                {
                    txtAdminName.Text = adminUser.Name;
                    txtEmail.Text = adminUser.ContactEmail;
                    txtPhone.Text = adminUser.Phone;
                }
               
                mpePopup.Show();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtAdminName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (cbUpdatePassword.Checked && txtPassword.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Password should not be empty";
                    mpePopup.Show();
                    return;
                }

                BOAdmins boAdmins = new BOAdmins();

                boAdmins.UpdateProfile(long.Parse(hfAdminID.Value), txtAdminName.Text.Trim(), txtPassword.Text.Trim(), cbUpdatePassword.Checked, txtEmail.Text.Trim(), txtPhone.Text.Trim());

                txtAdminName.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtPassword.Text = "";
                cbUpdatePassword.Checked = false;
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        protected void lbCancel_Click(object sender, EventArgs e)
        {
            try
            {
                txtAdminName.Text = "";
                txtEmail.Text = "";
                txtPhone.Text = "";
                txtPassword.Text = "";
                cbUpdatePassword.Checked = false;
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

    }
}