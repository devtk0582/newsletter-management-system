using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;

namespace NewsletterMS.Admin
{
    public partial class Sections : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "Name";
                ViewState["SortDirection"] = "ASC";
            }
        }

        private void BindSections()
        {
            try
            {
                gvSections.DataBind();
            }
            catch (Exception ex)
            {
                lblErr.Text = ex.Message; // Constants.GLOBAL_ERROR_MESSAGE + BOLogs.LogErrorToDB(ex, "Countries - BindCountries");
            }
        }

        protected void lbAddSection_Click(object sender, EventArgs e)
        {
            Popup(0);
        }

        protected void gvSections_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditSection":
                    Popup(Convert.ToInt32(e.CommandArgument));
                    break;
                case "DeleteSection":
                    (new BOSections()).DeleteSection(Convert.ToInt32(e.CommandArgument));
                    gvSections.DataBind();
                    break;
            }
        }

        protected void gvSections_Sorting(object sender, GridViewSortEventArgs e)
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
                lblErr.Text = ex.Message;
            }
        }

        private void BindSectionData(int sectionId)
        {
            try
            {
                NewsletterSection section = (new BOSections()).GetSectionByID(sectionId);

                if (section == null)
                {
                    return;
                }
                txtSectionName.Text = section.Name;
                txtSectionCode.Text = section.Code;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        public void Popup(int sectionId)
        {
            try
            {
                hfSectionID.Value = sectionId.ToString();
                if (sectionId > 0)
                {
                    BindSectionData(sectionId);
                    lblTitle.Text = "Edit Section";
                    lbSave.Text = "Update";
                }
                else
                {
                    txtSectionCode.Text = (new BOSections()).GetNewSectionCode();
                    lblTitle.Text = "Add Section";
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
                if (txtSectionName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                int currentSectionID = int.Parse(hfSectionID.Value);
                BOSections boSections = new BOSections();
                if (currentSectionID > 0)
                {
                    boSections.UpdateSection(currentSectionID, txtSectionName.Text.Trim());
                }
                else
                {
                    boSections.AddSection(txtSectionName.Text.Trim(), txtSectionCode.Text.Trim());
                }

                ClearPanel();
                mpePopup.Hide();
                BindSections();
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
            txtSectionName.Text = "";
            txtSectionCode.Text = "";
            lblErrorMsg.Text = "";
        }
    }
}