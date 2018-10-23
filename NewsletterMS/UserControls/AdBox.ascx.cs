using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;
using HtmlAgilityPack;

namespace NewsletterMS.UserControls
{
    public partial class AdBox : System.Web.UI.UserControl
    {
        public string AdName
        {
            get { return hfAdName.Value; }
            set { hfAdName.Value = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetBoxContent(NewsletterBox box)
        {
            try
            {
                Ad ad = (new BOAds()).GetAdByID(box.AdID.Value);
                imgView.ImageUrl = ad.AdImage;
                if (!string.IsNullOrEmpty(ad.AdLink))
                    imgView.PostBackUrl = ad.AdLink;
                else if (!string.IsNullOrEmpty(ad.AdVideo))
                    imgView.PostBackUrl = ad.AdVideo;
                else
                    imgView.Enabled = false;

                ltlContent.Text = AddMoreLink(ad.AdContent, ad.AdID);

                hfAdName.Value = ad.AdDescription;
            }
            catch (Exception ex)
            {

            }
        }

        private string AddMoreLink(string content, long adId)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            int total = ParseNode(doc.DocumentNode, 0);
            if (total >= 350)
                return doc.DocumentNode.InnerHtml + @"<div style=""display: inline-block""><a target=""_blank"" class=""box_content__more_link"" href=""NewsletterDetails.aspx?a=" + adId + @""">More</a></div>";
            else
                return doc.DocumentNode.InnerHtml;
        }

        private int ParseNode(HtmlNode node, int currentLength)
        {
            if (node.ChildNodes != null && node.ChildNodes.Count > 0)
            {
                int i = 0;
                while (i < node.ChildNodes.Count)
                {
                    HtmlNode child = node.ChildNodes[i];
                    if (currentLength > 350)
                    {
                        node.RemoveChild(child, false);
                    }
                    else
                    {
                        currentLength = ParseNode(child, currentLength);
                        i++;
                    }

                }

                return currentLength;
            }
            else
            {
                if (currentLength > 350)
                {
                    node.ParentNode.RemoveChild(node, false);
                    return currentLength;
                }

                int length = currentLength + node.InnerHtml.Length;
                if (length > 350)
                {
                    node.InnerHtml = node.InnerHtml.Substring(0, 350 - currentLength);
                }
                return length;
            }
        } 
    }
}