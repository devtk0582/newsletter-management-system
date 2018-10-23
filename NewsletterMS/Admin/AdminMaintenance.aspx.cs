using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;

namespace NewsletterMS.Admin
{
    public partial class AdminMaintenance : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "UserID";
                ViewState["SortDirection"] = "ASC";
            }
        }

        private void BindAdmins()
        {
            try
            {
                gvAdmins.DataBind();
            }
            catch (Exception ex)
            {
                lblErrAdmin.Text = ex.Message; // Constants.GLOBAL_ERROR_MESSAGE + BOLogs.LogErrorToDB(ex, "Countries - BindCountries");
            }
        }

        protected void lbSearchAdmin_Click(object sender, EventArgs e)
        {
            BindAdmins();
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchAdmin.Text = string.Empty;
            BindAdmins();
        }

        protected void lbAddAdmin_Click(object sender, EventArgs e)
        {
            Popup(0);
        }

        protected void gvAdmins_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditAdmin":
                    Popup(Convert.ToInt64(e.CommandArgument));
                    break;
                case "DeleteAdmin":
                    (new BOAdmins()).DeleteAdmin(Convert.ToInt64(e.CommandArgument));
                    gvAdmins.DataBind();
                    break;
            }
        }

        protected void gvAdmins_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                if (ViewState["SortDirection"].ToString() == "ASC")
                {
                    e.SortDirection = SortDirection.Descending;
                    ViewState["SortDirection"] = "DESC";
                }
                else
                {
                    e.SortDirection = SortDirection.Ascending;
                    ViewState["SortDirection"] = "ASC";
                }
                ViewState["SortExpression"] = e.SortExpression.ToString();
            }
            catch (Exception ex)
            {
                lblErrAdmin.Text = ex.Message;
            }
        }

        private void BindAdminData(long adminUserId)
        {
            try
            {
                BOAdmins boAdmin = new BOAdmins();
                AdminUser adminUser = boAdmin.GetAdminByID(adminUserId);

                if (adminUser == null)
                {
                    return;
                }
                txtName.Text = adminUser.Name;
                txtUserID.Text = adminUser.UserID;
                txtEmail.Text = adminUser.ContactEmail;
                txtPhone.Text = adminUser.Phone;
                ddlRoles.SelectedValue = adminUser.Role;

                var newsletters = boAdmin.GetAdminNewsletters(adminUserId);

                if (newsletters.Count > 0)
                {
                    foreach (long newsletterId in newsletters)
                    {
                        foreach (ListItem li in cblNewsletters.Items)
                        {
                            if (li.Value == newsletterId.ToString())
                                li.Selected = true;
                        }
                    }
                }
                
                trPassword.Visible = false;
                trConfirmPassword.Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        public void Popup(long adminUserId)
        {
            try
            {
                hfAdminID.Value = adminUserId.ToString();
                cblNewsletters.DataSource = (new BOPublications()).GetPublications();
                cblNewsletters.DataTextField = "NewsletterName";
                cblNewsletters.DataValueField = "NewsletterID";
                cblNewsletters.DataBind();

                if (adminUserId > 0)
                {
                    BindAdminData(adminUserId);
                    lblTitle.Text = "Edit Admin User";
                    lbSave.Text = "Update";
                }
                else
                {
                    trPassword.Visible = true;
                    trConfirmPassword.Visible = true;
                    lblTitle.Text = "Add Admin User";
                    lbSave.Text = "Add";
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
                if (txtName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtEmail.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Email should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (!Util.IsEmail(txtEmail.Text.Trim()))
                {
                    lblErrorMsg.Text = "Email address is not valid";
                    mpePopup.Show();
                    return;
                }

                long currentAdminID = long.Parse(hfAdminID.Value);
                BOAdmins boAdmins = new BOAdmins();
                List<long> selectedNewsletters = new List<long>();
                foreach (ListItem li in cblNewsletters.Items)
                {
                    if(li.Selected)
                        selectedNewsletters.Add(long.Parse(li.Value));
                }
                if (currentAdminID > 0)
                {
                    boAdmins.UpdateAdmin(currentAdminID, txtUserID.Text.Trim(), txtName.Text.Trim(),
                        ddlRoles.SelectedValue, txtPhone.Text.Trim(), txtEmail.Text.Trim(), selectedNewsletters);
                }
                else
                {
                    boAdmins.AddAdmin(txtUserID.Text.Trim(), txtPassword.Text.Trim(), txtName.Text.Trim(),
                        ddlRoles.SelectedValue, txtPhone.Text.Trim(), txtEmail.Text.Trim(), selectedNewsletters);
                }

                ClearPanel();
                mpePopup.Hide();
                BindAdmins();
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
                ClearPanel();
                mpePopup.Hide();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void ClearPanel()
        {
            txtName.Text = "";
            txtUserID.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
            ddlRoles.SelectedIndex = 0;
            cblNewsletters.DataSource = null;
            cblNewsletters.DataBind();
            lblErrorMsg.Text = "";
        }

        protected void gvAdmins_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
            }
        }
    }
}