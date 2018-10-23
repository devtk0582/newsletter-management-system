using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NewsletterMSBLL;
using System.Drawing;
using System.Web.UI.HtmlControls;

namespace NewsletterMS.Admin
{
    public partial class AdAssignment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErrAdAssignment.Text = "";
            if (!IsPostBack)
            {
                if (Session["NewsletterID"] != null)
                {
                    BindAdvertisers();
                    SetupNewsletterBoxes();
                }
                else if (Session["Role"].ToString() == "S")
                {
                    Response.Redirect("~/Admin/SelectNewsletter.aspx?page=as");
                }
                else
                {
                    Response.Redirect("~/Admin/Default.aspx");
                }
            }
        }

        private void BindAdvertisers()
        {
            ddlAdvertisers.DataSource = (new BOAdvertisers()).GetAdvertisers();
            ddlAdvertisers.DataTextField = "AdvertiserName";
            ddlAdvertisers.DataValueField = "AdvertiserID";
            ddlAdvertisers.DataBind();
            ddlAdvertisers.Items.Insert(0, new ListItem("Select Advertiser", "0"));
        }

        private void SetupNewsletterBoxes()
        {
            List<NewsletterBox> boxes = (new BOBoxes()).GetNewsletterBoxes(long.Parse(Session["NewsletterID"].ToString()));
            foreach (NewsletterBox box in boxes)
            {
                switch (box.BoxID)
                {
                    case "A001":
                        lbBoxA001.BackColor = Color.Black;
                        break;
                    case "A002":
                        lbBoxA002.BackColor = Color.Black;
                        break;
                    case "A003":
                        lbBoxA003.BackColor = Color.Black;
                        break;
                    case "A004":
                        lbBoxA004.BackColor = Color.Black;
                        break;
                    case "N001":
                        lbBoxN001.BackColor = Color.Black;
                        break;
                    case "N002":
                        lbBoxN002.BackColor = Color.Black;
                        break;
                    case "N003":
                        lbBoxN003.BackColor = Color.Black;
                        break;
                    case "N004":
                        lbBoxN004.BackColor = Color.Black;
                        break;
                    case "N005":
                        lbBoxN005.BackColor = Color.Black;
                        break;
                    case "N006":
                        lbBoxN006.BackColor = Color.Black;
                        break;
                    case "N007":
                        lbBoxN007.BackColor = Color.Black;
                        break;
                    case "N008":
                        lbBoxN008.BackColor = Color.Black;
                        break;
                }
            }
        }

        protected void ddlAdvertisers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAdvertisers.SelectedIndex > 0)
            {
                lvAds.DataSource = (new BOAds()).GetAdsByAdvertiser(int.Parse(ddlAdvertisers.SelectedValue));
                lvAds.DataBind();
            }
        }

        protected void lvAds_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            if (e.CommandName == "SelectAd")
            {
                if (e.CommandArgument != null)
                {
                    Ad selectedAd = (new BOAds()).GetAdByID(long.Parse(e.CommandArgument.ToString()));
                    txtAdDesc.Text = selectedAd.AdDescription;
                    txtAdInstructions.Text = selectedAd.AdInstruction;
                    foreach (ListViewItem item in lvAds.Items)
                    {
                        ((HtmlTableRow)item.FindControl("trAdDesc")).BgColor = "";
                    }
                    ((HtmlTableRow)e.Item.FindControl("trAdDesc")).BgColor = "silver";
                    hfSelectedAd.Value = e.CommandArgument.ToString();
                }
            }
        }

        protected void lbBoxA001_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxA001.BackColor == Color.White)
                {
                    hfBoxA001.Value = hfSelectedAd.Value;
                    lbBoxA001.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxA002_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxA002.BackColor == Color.White)
                {
                    hfBoxA002.Value = hfSelectedAd.Value;
                    lbBoxA002.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN001_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN001.BackColor == Color.White)
                {
                    hfBoxN001.Value = hfSelectedAd.Value;
                    lbBoxN001.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN002_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN002.BackColor == Color.White)
                {
                    hfBoxN002.Value = hfSelectedAd.Value;
                    lbBoxN002.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN003_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN003.BackColor == Color.White)
                {
                    hfBoxN003.Value = hfSelectedAd.Value;
                    lbBoxN003.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN004_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN004.BackColor == Color.White)
                {
                    hfBoxN004.Value = hfSelectedAd.Value;
                    lbBoxN004.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN005_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN005.BackColor == Color.White)
                {
                    hfBoxN005.Value = hfSelectedAd.Value;
                    lbBoxN005.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN006_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN006.BackColor == Color.White)
                {
                    hfBoxN006.Value = hfSelectedAd.Value;
                    lbBoxN006.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN007_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN007.BackColor == Color.White)
                {
                    hfBoxN007.Value = hfSelectedAd.Value;
                    lbBoxN007.BackColor = Color.Black;
                }  
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxN008_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxN008.BackColor == Color.White)
                {
                    hfBoxN008.Value = hfSelectedAd.Value;
                    lbBoxN008.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxA003_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxA003.BackColor == Color.White)
                {
                    hfBoxA003.Value = hfSelectedAd.Value;
                    lbBoxA003.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbBoxA004_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(hfSelectedAd.Value))
            {
                if (lbBoxA004.BackColor == Color.White)
                {
                    hfBoxA004.Value = hfSelectedAd.Value;
                    lbBoxA004.BackColor = Color.Black;
                }
            }
            else
                lblErrAdAssignment.Text = "Please select an advertisement.";
        }

        protected void lbSave_Click(object sender, EventArgs e)
        {
            List<NewsletterBox> newsletterBoxes = new List<NewsletterBox>();

            if (!string.IsNullOrEmpty(hfBoxA001.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxA001.Value),
                    BoxID = "A001",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxA002.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxA002.Value),
                    BoxID = "A002",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxA003.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxA003.Value),
                    BoxID = "A003",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxA004.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxA004.Value),
                    BoxID = "A004",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN001.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN001.Value),
                    BoxID = "N001",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN002.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN002.Value),
                    BoxID = "N002",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN003.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN003.Value),
                    BoxID = "N003",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN004.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN004.Value),
                    BoxID = "N004",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN005.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN005.Value),
                    BoxID = "N005",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN006.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN006.Value),
                    BoxID = "N006",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN007.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN007.Value),
                    BoxID = "N007",
                    BoxType = "D"
                });
            }

            if (!string.IsNullOrEmpty(hfBoxN008.Value))
            {
                newsletterBoxes.Add(new NewsletterBox()
                {
                    NewsletterID = long.Parse(Session["NewsletterID"].ToString()),
                    AdID = long.Parse(hfBoxN008.Value),
                    BoxID = "N008",
                    BoxType = "D"
                });
            }

            if (newsletterBoxes.Count > 0)
            {
                (new BOBoxes()).InsertNewsletterBoxes(newsletterBoxes);
                lblErrAdAssignment.Text = "All changes have been saved.";
                hfBoxA001.Value = "";
                hfBoxA002.Value = "";
                hfBoxA003.Value = "";
                hfBoxA004.Value = "";
                hfBoxN001.Value = "";
                hfBoxN002.Value = "";
                hfBoxN003.Value = "";
                hfBoxN004.Value = "";
                hfBoxN005.Value = "";
                hfBoxN006.Value = "";
                hfBoxN007.Value = "";
                hfBoxN008.Value = "";
            }
        }
    }
}