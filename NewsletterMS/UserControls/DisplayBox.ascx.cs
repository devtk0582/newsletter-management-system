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
    public partial class DisplayBox : System.Web.UI.UserControl
    {
        public string BoxID
        {
            get { return hfBoxID.Value; }
            set { hfBoxID.Value = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void SetBoxContent(NewsletterBox box)
        {
            try
            {
                switch (box.BoxType)
                {
                    case "N":
                        hfBoxID.Value = box.BoxID;
                        ltlContent.Text = AddMoreLink(box.BoxData, box.NewsletterID, box.BoxID);
                        imgView.ImageUrl = box.BoxImage;
                        divNews.Visible = true;
                        divAd.Visible = false;
                        divVideo.Visible = false;
                        break;
                    case "V":
                        videoEmbed.Attributes["src"] = @"http://www.youtube.com/v/" + Util.ParseVideoLink(box.BoxLink);
                        divNews.Visible = false;
                        divAd.Visible = false;
                        divVideo.Visible = true;
                        break;
                    case "A":
                        divNews.Visible = false;
                        divAd.Visible = true;
                        adBox.SetBoxContent(box);
                        lblAdName.Text = adBox.AdName;
                        divVideo.Visible = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                
            }
           
        }

        private string AddMoreLink(string content, long newsletterId, string boxId)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(content);
            int total = ParseNode(doc.DocumentNode, 0);
            if (total >= 350)
                return doc.DocumentNode.InnerHtml + @"<div style=""display: inline-block""><a class=""box_content__more_link"" target=""_blank"" href=""NewsletterDetails.aspx?n=" + newsletterId + "&b=" + boxId + @""">More</a></div>";
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