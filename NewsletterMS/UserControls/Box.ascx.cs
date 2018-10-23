using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;
using System.IO;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace NewsletterMS.UserControls
{
    public partial class Box : System.Web.UI.UserControl
    {
        public string BoxNumber
        {
            get { return lblBoxNumber.Text; }
            set { lblBoxNumber.Text = value; }
        }

        public string BoxID
        {
            get { return hfBoxID.Value; }
            set { hfBoxID.Value = value; }
        }

        public string BoxMode
        {
            get { return hfMode.Value; }
            set { hfMode.Value = value; }
        }

        public string BoxType
        {
            get { return ddlTypes.SelectedValue; }
            set { ddlTypes.SelectedValue = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ddlTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            //switch (ddlTypes.SelectedValue)
            //{
            //    case "N":
            //        divEditNews.Visible = true;
            //        divEditAd.Visible = false;
            //        divEditVideo.Visible = false;
            //        break;
            //    case "V":
            //        divEditVideo.Visible = true;
            //        divEditAd.Visible = false;
            //        divEditNews.Visible = false;
            //        break;
            //    case "A":
            //        divEditAd.Visible = true;
            //        divEditNews.Visible = false;
            //        divEditVideo.Visible = false;
            //        break;
            //}
        }

        //public NewsletterBox GetBoxContent()
        //{
        //    NewsletterBox box = new NewsletterBox();
        //    box.NewsletterID = long.Parse(Session["NewsletterID"].ToString());
        //    box.BoxID = BoxNumber;
        //    box.BoxType = ddlTypes.SelectedValue;
        //    box.BoxImage = imgView.ImageUrl;
        //    box.BoxLink = txtVideoLink.Text.Trim();
        //    box.BoxData = txtContent.Text.Trim();
        //    box.BoxPage = 1;
        //    return box;
        //}

        //public void SetBoxContent(NewsletterBox box)
        //{
        //    switch (box.BoxType)
        //    {
        //        case "N":
        //            lblBoxNumber.Text = box.BoxID;
        //            txtContent.Text = box.BoxData;
        //            imgView.ImageUrl = box.BoxImage;
        //            divEditNews.Visible = true;
        //            divEditAd.Visible = false;
        //            divEditVideo.Visible = false;
        //            break;
        //        case "V":
        //            txtVideoLink.Text = box.BoxLink;
        //            videoEmbed.Attributes["src"] = @"http://www.youtube.com/v/" + Util.ParseVideoLink(box.BoxLink);
        //            divEditNews.Visible = false;
        //            divEditAd.Visible = false;
        //            divEditVideo.Visible = true;
        //            break;
        //        case "D":
        //            divEditNews.Visible = false;
        //            adBox.SetBoxContent(box);
        //            lblAdName.Text = adBox.AdName;
        //            divEditAd.Visible = true;
        //            divEditVideo.Visible = false;
        //            break;
        //    }
        //    ddlTypes.SelectedValue = box.BoxType;
        //}

    }
}