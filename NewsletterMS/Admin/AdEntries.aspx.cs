using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;
using System.IO;

namespace NewsletterMS.Admin
{
    public partial class AdEntries : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrAds.Text = "";
            if (!IsPostBack)
            {
                ViewState["SortExpression"] = "Desc";
                ViewState["SortDirection"] = "ASC";
            }
        }

        private void BindAds()
        {
            try
            {
                gvAds.DataBind();
            }
            catch (Exception ex)
            {
                lblErrAds.Text = ex.Message; // Constants.GLOBAL_ERROR_MESSAGE + BOLogs.LogErrorToDB(ex, "Countries - BindCountries");
            }
        }

        protected void lbSearchAd_Click(object sender, EventArgs e)
        {
            BindAds();
        }

        protected void lbShowAll_Click(object sender, EventArgs e)
        {
            txtSearchAd.Text = string.Empty;
            BindAds();
        }

        protected void lbAddAd_Click(object sender, EventArgs e)
        {
            Popup(0);
            
        }

        protected void gvAds_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "EditAd":
                    Popup(long.Parse(e.CommandArgument.ToString()));
                    break;
                case "DeleteAd":
                    (new BOAds()).DeleteAd(long.Parse(e.CommandArgument.ToString()));
                    BindAds();
                    break;
            }
        }

        protected void gvAds_Sorting(object sender, GridViewSortEventArgs e)
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
                lblErrAds.Text = ex.Message;
            }
        }

        private void BindAdData(long adId)
        {
            try
            {
                Ad ad = (new BOAds()).GetAdByID(adId);

                if (ad == null)
                {
                    return;
                }
                txtAdDesc.Text = ad.AdDescription;
                txtCampaign.Text = ad.AdCampaign;
                ddlRegions.SelectedValue = ad.AdRegionCode.ToString();
                ddlTypes.SelectedValue = ad.AdType.ToString();
                ddlAdvertisers.SelectedValue = ad.AdvertiserID.Value.ToString();
                txtInstruction.Text = ad.AdInstruction;
                txtContent.Text = ad.AdContent;
                txtLink.Text = ad.AdLink;
                ddlStatus.SelectedValue = ad.Active.ToString();
                if (!string.IsNullOrEmpty(ad.AdVideo))
                    txtVideoLink.Text = ad.AdVideo;

                if (!string.IsNullOrEmpty(ad.AdImage))
                    imgView.ImageUrl = ad.AdImage;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        public void Popup(long adId)
        {
            try
            {
                if (adId != 0)
                {
                    hfAdID.Value = adId.ToString();
                    BindAdData(adId);
                    lblTitle.Text = "Edit Ad";
                    lbSave.Text = "Update";
                }
                else
                {
                    hfAdID.Value = string.Empty;
                    lblTitle.Text = "Add Ad";
                    lbSave.Text = "Add";
                }

                ddlAdvertisers.DataSource = (new BOAdvertisers()).GetAdvertisers();
                ddlAdvertisers.DataTextField = "AdvertiserName";
                ddlAdvertisers.DataValueField = "AdvertiserID";
                ddlAdvertisers.DataBind();
                ddlAdvertisers.Items.Insert(0, new ListItem("Select Advertiser", "0"));

                BOAds boAds = new BOAds();
                ddlTypes.DataSource = boAds.GetAdTypes();
                ddlTypes.DataTextField = "TypeName";
                ddlTypes.DataValueField = "TypeID";
                ddlTypes.DataBind();
                ddlTypes.Items.Insert(0, new ListItem("Select Type", "0"));

                ddlRegions.DataSource = boAds.GetAdRegions();
                ddlRegions.DataTextField = "RegionName";
                ddlRegions.DataValueField = "RegionID";
                ddlRegions.DataBind();
                ddlRegions.Items.Insert(0, new ListItem("Select Region", "0"));

                trAddAd.Visible = true;
                trViewAds.Visible = false;
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
                if (txtAdDesc.Text.Trim() == "")
                {
                    lblErrorMsg.Text = "Description should not be empty";
                    return;
                }

                long advertiserId = long.Parse(ddlAdvertisers.SelectedValue);

                if (advertiserId == 0)
                {
                    lblErrorMsg.Text = "Advertiser is required";
                    return;
                }

                BOAds boAds = new BOAds();
                if (hfAdID.Value != string.Empty)
                {
                    boAds.UpdateAd(long.Parse(hfAdID.Value), advertiserId, txtAdDesc.Text.Trim(), txtCampaign.Text.Trim(),
                        int.Parse(ddlRegions.SelectedValue), int.Parse(ddlTypes.SelectedValue), decimal.Parse(string.IsNullOrEmpty(txtPrice.Text.Trim()) ? "0" : txtPrice.Text.Trim()),
                        txtInstruction.Text.Trim(), txtLink.Text.Trim(), imgView.ImageUrl, txtVideoLink.Text.Trim(), txtContent.Text.Trim());
                }
                else
                {
                    boAds.AddAd(advertiserId, txtAdDesc.Text.Trim(), txtCampaign.Text.Trim(),
                        int.Parse(ddlRegions.SelectedValue), int.Parse(ddlTypes.SelectedValue), decimal.Parse(string.IsNullOrEmpty(txtPrice.Text.Trim()) ? "0" : txtPrice.Text.Trim()),
                        txtInstruction.Text.Trim(), txtLink.Text.Trim(), imgView.ImageUrl, txtVideoLink.Text.Trim(), txtContent.Text.Trim());
                }

                ClearPanel();
                BindAds();
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
            }
            catch (Exception ex)
            {
                lblErrorMsg.Text = ex.Message;
            }
        }

        private void ClearPanel()
        {
            txtAdDesc.Text = "";
            txtCampaign.Text = "";
            ddlAdvertisers.DataSource = null;
            ddlAdvertisers.DataBind();
            ddlRegions.DataSource = null;
            ddlRegions.DataBind();
            ddlTypes.DataSource = null;
            ddlTypes.DataBind();
            ddlStatus.DataSource = null;
            ddlStatus.DataBind();   
            txtInstruction.Text = "";
            txtLink.Text = "";
            txtPrice.Text = "";
            lblErrorMsg.Text = "";
            txtContent.Text = "";
            txtVideoLink.Text = "";

            trAddAd.Visible = false;
            trViewAds.Visible = true;
        }

        protected void gvAds_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void lbUpload_Click(object sender, EventArgs e)
        {
            if (UploadImage.HasFile)
            {
                string fileExt = Path.GetExtension(UploadImage.FileName);
                if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg" && fileExt.ToLower() != ".png")
                {
                    lblErrAds.Text = "Only PNG, GIF and JPG image are accepted.";
                    return;
                }
                string fileName = Guid.NewGuid().ToString() + fileExt;
                UploadImage.SaveAs(Server.MapPath("~/Upload") + "\\" + fileName);
                imgView.ImageUrl = "~/Upload/" + fileName;
            }
            else
            {
                lblErrAds.Text = "No file selected";
            }
        }

    }
}