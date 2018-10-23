using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;
using System.IO;
using System.Text.RegularExpressions;

namespace NewsletterMS.Admin
{
    public partial class Publications : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "Name";
                ViewState["SortDirection"] = "ASC";
            }
        }

        private void BindPublications()
        {
            try
            {
                gvPublications.DataBind();
            }
            catch (Exception ex)
            {
                lblErrPublication.Text = ex.Message; // Constants.GLOBAL_ERROR_MESSAGE + BOLogs.LogErrorToDB(ex, "Countries - BindCountries");
            }
        }

        protected void lbSearchPublication_Click(object sender, EventArgs e)
        {
            BindPublications();
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchPublication.Text = string.Empty;
            BindPublications();
        }

        protected void lbAddPublication_Click(object sender, EventArgs e)
        {
            Popup(0);
        }

        protected void gvPublications_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditPublication":
                    Popup(long.Parse(e.CommandArgument.ToString()));
                    break;
                case "DeletePublication":
                    (new BOPublications()).DeletePublication(long.Parse(e.CommandArgument.ToString()));
                    BindPublications();
                    break;
            }
        }

        protected void gvPublications_Sorting(object sender, GridViewSortEventArgs e)
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
                lblErrPublication.Text = ex.Message;
            }
        }

        private void BindPublicationData(long publicationId)
        {
            try
            {
                Newsletter publication = (new BOPublications()).GetPublicationByID(publicationId);

                if (publication == null)
                {
                    return;
                }
                txtPublicationName.Text = publication.NewsletterName;
                if (!string.IsNullOrEmpty(publication.NewsletterType))
                    ddlTypes.SelectedValue = publication.NewsletterType;
                txtContactName.Text = publication.PrimaryContactName;
                txtContactEmail.Text = publication.PrimaryContactEmail;
                txtContactPhone.Text = publication.PrimaryContactPhone;
                if (!string.IsNullOrEmpty(publication.NewsletterFrequency))
                    rblFrequency.SelectedValue = publication.NewsletterFrequency;
                if (!string.IsNullOrEmpty(publication.BackgroundColor))
                    txtBackgroundColor.Text = publication.BackgroundColor;
                if (!string.IsNullOrEmpty(publication.SectionColor))
                    txtSectionColor.Text = publication.SectionColor;
                if (publication.UniqueID.HasValue)
                    lblURL.Text = Properties.Settings.Default.DefaultRootURL + @"/Archive/" + Util.GetNewsletterFileName(publication.NewsletterName);
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        public void Popup(long publicationId)
        {
            try
            {
                if (publicationId != 0)
                {
                    hfPublicationID.Value = publicationId.ToString();
                    trURL.Visible = true;
                    BindPublicationData(publicationId);
                    lblTitle.Text = "Edit Publication";
                    lbSave.Text = "Update";
                }
                else
                {
                    hfPublicationID.Value = string.Empty;
                    lblTitle.Text = "Add Publication";
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
                if (txtPublicationName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Name should not be empty";
                    mpePopup.Show();
                    return;
                }



                if (txtContactName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Contact name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtContactEmail.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Email should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (!Util.IsEmail(txtContactEmail.Text.Trim()))
                {
                    lblErrorMsg.Text = "Email address is not valid";
                    mpePopup.Show();
                    return;
                }

                try
                {
                    System.Drawing.Color bgColor = System.Drawing.ColorTranslator.FromHtml("#" + txtBackgroundColor.Text.Trim());
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = "Background Color is not valid";
                    mpePopup.Show();
                    return;
                }

                try
                {
                    System.Drawing.Color scColor = System.Drawing.ColorTranslator.FromHtml("#" + txtSectionColor.Text.Trim());
                }
                catch (Exception ex)
                {
                    lblErrorMsg.Text = "Section Color is not valid";
                    mpePopup.Show();
                    return;
                }

                
                BOPublications boPublications = new BOPublications();

                if (hfPublicationID.Value != string.Empty)
                {
                    boPublications.UpdatePublication(long.Parse(hfPublicationID.Value), txtPublicationName.Text.Trim(), ddlTypes.SelectedValue, rblFrequency.SelectedValue,
                        txtContactName.Text.Trim(), txtContactEmail.Text.Trim(), txtContactPhone.Text.Trim(), txtBackgroundColor.Text.Trim(), txtSectionColor.Text.Trim());
                }
                else
                {
                    if (boPublications.CheckIfPublicationExists(txtPublicationName.Text.Trim()))
                    {
                        lblErrorMsg.Text = "Similar name has existed. Please choose another name.";
                        mpePopup.Show();
                        return;
                    }

                    string uniqueId = boPublications.AddPublication(txtPublicationName.Text.Trim(), ddlTypes.SelectedValue, rblFrequency.SelectedValue,
                        txtContactName.Text.Trim(), txtContactEmail.Text.Trim(), txtContactPhone.Text.Trim(), txtBackgroundColor.Text.Trim(), txtSectionColor.Text.Trim());

                    if (uniqueId != "")
                    {
                        SavePublicationToArchive(txtPublicationName.Text.Trim());
                    }
                }

                ClearPanel();
                mpePopup.Hide();
                BindPublications();
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void SavePublicationToArchive(string newsletterName)
        {
            string newsletterFileName = Regex.Replace(newsletterName.Replace(' ', '-').ToLower(), "[^a-z0-9]", "") + ".htm";
            string destPath = Server.MapPath("~/Archive") + "\\" + newsletterFileName;
            string sourcePath = Server.MapPath("~/Template/NewsletterTemplate.htm");

            File.Copy(sourcePath, destPath);
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
            txtPublicationName.Text = "";
            ddlTypes.SelectedIndex = 0;
            rblFrequency.SelectedValue = "W";
            txtContactName.Text = "";
            txtContactEmail.Text = "";
            txtContactPhone.Text = "";
            txtSectionColor.Text = "";
            txtBackgroundColor.Text = "";
            lblErrorMsg.Text = "";
            lblURL.Text = "";
            trURL.Visible = false;
        }

        protected void gvPublications_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}