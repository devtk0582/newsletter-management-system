using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;

namespace NewsletterMS.Admin
{
    public partial class Advertisers : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "Name";
                ViewState["SortDirection"] = "ASC";
            }
        }

        private void BindAdvertisers()
        {
            try
            {
                gvAdvertisers.DataBind();
            }
            catch (Exception ex)
            {
                lblErrAdvertiser.Text = ex.Message; // Constants.GLOBAL_ERROR_MESSAGE + BOLogs.LogErrorToDB(ex, "Countries - BindCountries");
            }
        }

        protected void lbSearchAdvertiser_Click(object sender, EventArgs e)
        {
            BindAdvertisers();
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchAdvertiser.Text = string.Empty;
            BindAdvertisers();
        }

        protected void lbAddAdvertiser_Click(object sender, EventArgs e)
        {
            Popup(0);
        }

        protected void gvAdvertisers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditAdvertiser":
                    Popup(long.Parse(e.CommandArgument.ToString()));
                    break;
                case "DeleteAdvertiser":
                    (new BOAdvertisers()).DeleteAdvertiser(long.Parse(e.CommandArgument.ToString()));
                    BindAdvertisers();
                    break;
            }
        }

        protected void gvAdvertisers_Sorting(object sender, GridViewSortEventArgs e)
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
                lblErrAdvertiser.Text = ex.Message;
            }
        }

        private void BindAdvertiserData(long advertiserId)
        {
            try
            {
                Advertiser advertiser = (new BOAdvertisers()).GetAdvertiserByID(advertiserId);

                if (advertiser == null)
                {
                    return;
                }
                txtAdvertiserName.Text = advertiser.AdvertiserName;
                txtAdvertiserRegionType.Text = advertiser.AdvertiserRegionType;
                txtContact1Name.Text = advertiser.AdvertiserContact1Name;
                txtContact1Email.Text = advertiser.AdvertiserContact1Email;
                txtContact1Phone.Text = advertiser.AdvertiserContact1Phone;
                txtContact1Phone2.Text = advertiser.AdvertiserContact1Phone2;
                txtContact2Name.Text = advertiser.AdvertiserContact2Name;
                txtContact2Email.Text = advertiser.AdvertiserContact2Email;
                txtContact2Phone.Text = advertiser.AdvertiserContact2Phone;
                txtContact2Phone2.Text = advertiser.AdvertiserContact2Phone2;
                
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
                mpePopup.Show();
            }
        }

        public void Popup(long advertiserId)
        {
            try
            {
                if (advertiserId != 0)
                {
                    hfAdvertiserID.Value = advertiserId.ToString();
                    BindAdvertiserData(advertiserId);
                    lblTitle.Text = "Edit Advertiser";
                    lbSave.Text = "Update";
                }
                else
                {
                    hfAdvertiserID.Value = string.Empty;
                    lblTitle.Text = "Add Advertiser";
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
                if (txtAdvertiserName.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtContact1Name.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Primary contact name should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (txtContact1Email.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Primary contact email should not be empty";
                    mpePopup.Show();
                    return;
                }

                if (!Util.IsEmail(txtContact1Email.Text.Trim()))
                {
                    lblErrorMsg.Text = "Primary contact email address is not valid";
                    mpePopup.Show();
                    return;
                }


                BOAdvertisers boAdvertisers = new BOAdvertisers();
                if (hfAdvertiserID.Value != string.Empty)
                {
                    boAdvertisers.UpdateAdvertiser(long.Parse(hfAdvertiserID.Value), txtAdvertiserName.Text.Trim(), txtAdvertiserRegionType.Text.Trim(),
                        txtContact1Name.Text.Trim(), txtContact1Email.Text.Trim(), txtContact1Phone.Text.Trim(), txtContact1Phone2.Text.Trim(),
                        txtContact2Name.Text.Trim(), txtContact2Email.Text.Trim(), txtContact2Phone.Text.Trim(), txtContact2Phone2.Text.Trim());
                }
                else
                {
                    boAdvertisers.AddAdvertiser(txtAdvertiserName.Text.Trim(), txtAdvertiserRegionType.Text.Trim(),
                        txtContact1Name.Text.Trim(), txtContact1Email.Text.Trim(), txtContact1Phone.Text.Trim(), txtContact1Phone2.Text.Trim(),
                        txtContact2Name.Text.Trim(), txtContact2Email.Text.Trim(), txtContact2Phone.Text.Trim(), txtContact2Phone2.Text.Trim());
                }

                ClearPanel();
                mpePopup.Hide();
                BindAdvertisers();
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
            txtAdvertiserName.Text = "";
            txtAdvertiserRegionType.Text = "";
            txtContact1Name.Text = "";
            txtContact1Email.Text = "";
            txtContact1Phone.Text = "";
            txtContact1Phone2.Text = "";
            txtContact2Name.Text = "";
            txtContact2Email.Text = "";
            txtContact2Phone.Text = "";
            txtContact2Phone2.Text = "";
            lblErrorMsg.Text = "";
        }

        protected void gvAdvertisers_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }
}