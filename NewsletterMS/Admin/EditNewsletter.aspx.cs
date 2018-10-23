using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using NewsletterMSBLL;
using System.Net.Mail;
using NewsletterMS.UserControls;

namespace NewsletterMS.Admin
{
    public partial class EditNewsletter : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblErr.Text = "";
            if (!IsPostBack)
            {
                //lblCurrentDate.Text = DateTime.Now.ToString("D");
                if (Session["NewsletterID"] != null)
                {
                    hfCurrentNLID.Value = Session["NewsletterID"].ToString();
                    BindNewsletter(long.Parse(Session["NewsletterID"].ToString()));
                }
                else
                {
                    Response.Redirect("~/Admin/Default.aspx");
                }
            }
        }

        private void BindNewsletter(long newsletterId)
        {
            var newsletter = (new BOPublications()).GetPublicationByID(newsletterId);
            if (newsletter != null)
            {
                titleHeader.InnerText = newsletter.NewsletterName;
                if (newsletter.UniqueID.HasValue)
                    urlHeader.InnerText = "(URL: " + Properties.Settings.Default.DefaultRootURL + @"/Archive/" + Util.GetNewsletterFileName(newsletter.NewsletterName) + ")";
            }
        }

        //private void BindNewsletter(long newsletterId)
        //{
        //    string newsletterType = (new BOPublications()).GetPublicationTypeByID(newsletterId);
        //    switch (newsletterType)
        //    {
        //        case "2B":
        //            boxN003.Visible = false;
        //            boxN004.Visible = false;
        //            boxN005.Visible = false;
        //            boxN006.Visible = false;
        //            boxN007.Visible = false;
        //            boxN008.Visible = false;
        //            break;
        //        case "4B":
        //            boxN005.Visible = false;
        //            boxN006.Visible = false;
        //            boxN007.Visible = false;
        //            boxN008.Visible = false;
        //            break;
        //        case "6B":
        //            boxN007.Visible = false;
        //            boxN008.Visible = false;
        //            break;
        //        default:
        //            break;
        //    }

        //    List<NewsletterBox> boxes = (new BOBoxes()).GetNewsletterBoxes(newsletterId);
        //    if (boxes != null && boxes.Count > 0)
        //    {
        //        foreach (NewsletterBox box in boxes)
        //        {
        //            switch (box.BoxID)
        //            {
        //                case "A001":
        //                    if (Session["Role"].ToString() == "L")
        //                    {
        //                        lbUploadA001.Visible = false;
        //                        fuA001.Visible = false;
        //                    }
        //                    else
        //                    {
        //                        if (box.BoxType == "D")
        //                        {
        //                            divA001.Visible = false;
        //                            divA001AD.Visible = true;
        //                            adBoxA001.SetBoxContent(box);
        //                        }
        //                        else
        //                        {
        //                            imgA001.ImageUrl = box.BoxImage;
        //                        }
        //                        hfA001Mode.Value = "U";
        //                    }
        //                    break;
        //                case "A002":
        //                    if (Session["Role"].ToString() == "L")
        //                    {
        //                        lbUploadA002.Visible = false;
        //                        fuA002.Visible = false;
        //                    }
        //                    else
        //                    {
        //                        if (box.BoxType == "D")
        //                        {
        //                            divA002.Visible = false;
        //                            divA002AD.Visible = true;
        //                            adBoxA002.SetBoxContent(box);
        //                        }
        //                        else
        //                        {
        //                            imgA002.ImageUrl = box.BoxImage;

        //                        }
        //                        hfA002Mode.Value = "U";
        //                    }
        //                    break;
        //                case "A003":
        //                    if (Session["Role"].ToString() == "L")
        //                    {
        //                        lbUploadA003.Visible = false;
        //                        fuA003.Visible = false;
        //                    }
        //                    else
        //                    {
        //                        if (box.BoxType == "D")
        //                        {
        //                            divA003.Visible = false;
        //                            divA003AD.Visible = true;
        //                            adBoxA003.SetBoxContent(box);
        //                        }
        //                        else
        //                        {
        //                            imgA003.ImageUrl = box.BoxImage;

        //                        }
        //                        hfA003Mode.Value = "U";
        //                    }
        //                    break;
        //                case "A004":
        //                    if (Session["Role"].ToString() == "L")
        //                    {
        //                        lbUploadA004.Visible = false;
        //                        fuA004.Visible = false;
        //                    }
        //                    else
        //                    {
        //                        if (box.BoxType == "D")
        //                        {
        //                            divA004.Visible = false;
        //                            divA004AD.Visible = true;
        //                            adBoxA004.SetBoxContent(box);
        //                        }
        //                        else
        //                        {
        //                            imgA004.ImageUrl = box.BoxImage;

        //                        }
        //                        hfA004Mode.Value = "U";
        //                    }
        //                    break;
        //                case "B001":
        //                    imgBanner.ImageUrl = box.BoxImage;
        //                    hfBannerMode.Value = "U";
        //                    break;
        //                case "N001":
        //                    boxN001.SetBoxContent(box);
        //                    boxN001.BoxMode = "U";
        //                    break;
        //                case "N002":
        //                    boxN002.SetBoxContent(box);
        //                    boxN002.BoxMode = "U";
        //                    break;
        //                case "N003":
        //                    boxN003.SetBoxContent(box);
        //                    boxN003.BoxMode = "U";
        //                    break;
        //                case "N004":
        //                    boxN004.SetBoxContent(box);
        //                    boxN004.BoxMode = "U";
        //                    break;
        //                case "N005":
        //                    boxN005.SetBoxContent(box);
        //                    boxN005.BoxMode = "U";
        //                    break;
        //                case "N006":
        //                    boxN006.SetBoxContent(box);
        //                    boxN006.BoxMode = "U";
        //                    break;
        //                case "N007":
        //                    boxN007.SetBoxContent(box);
        //                    boxN007.BoxMode = "U";
        //                    break;
        //                case "N008":
        //                    boxN008.SetBoxContent(box);
        //                    boxN008.BoxMode = "U";
        //                    break;
        //            }
        //        }
        //    }
        //}

        //protected void lbUploadBanner_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fuBanner.HasFile)
        //        {
        //            string fileExt = Path.GetExtension(fuBanner.FileName);
        //            if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg" && fileExt.ToLower() != ".png")
        //            {
        //                lblErr.Text = "Only PNG, GIF and JPG image are accepted.";
        //                return;
        //            }
        //            string fileName = Guid.NewGuid().ToString() + fileExt;
        //            fuBanner.SaveAs(Server.MapPath("~/Upload") + "\\" + fileName);
        //            imgBanner.ImageUrl = "~/Upload/" + fileName;
        //            hfBannerTrack.Value = "Y";
        //        }
        //        else
        //        {
        //            lblErr.Text = "No file selected";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErr.Text = ex.Message;
        //    }
        //}

        //protected void lbUploadA001_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fuA001.HasFile)
        //        {
        //            string fileExt = Path.GetExtension(fuA001.FileName);
        //            if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg" && fileExt.ToLower() != ".png")
        //            {
        //                lblErr.Text = "Only PNG, GIF and JPG image are accepted.";
        //                return;
        //            }
        //            string fileName = Guid.NewGuid().ToString() + fileExt;
        //            fuA001.SaveAs(Server.MapPath("~/Upload") + "\\" + fileName);
        //            imgA001.ImageUrl = "~/Upload/" + fileName;
        //            hfA001Track.Value = "Y";
        //        }
        //        else
        //        {
        //            lblErr.Text = "No file selected";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErr.Text = ex.Message;
        //    }
        //}

        //protected void lbUploadA002_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fuA002.HasFile)
        //        {
        //            string fileExt = Path.GetExtension(fuA002.FileName);
        //            if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg" && fileExt.ToLower() != ".png")
        //            {
        //                lblErr.Text = "Only PNG, GIF and JPG image are accepted.";
        //                return;
        //            }
        //            string fileName = Guid.NewGuid().ToString() + fileExt;
        //            fuA002.SaveAs(Server.MapPath("~/Upload") + "\\" + fileName);
        //            imgA002.ImageUrl = "~/Upload/" + fileName;
        //            hfA002Track.Value = "Y";
        //        }
        //        else
        //        {
        //            lblErr.Text = "No file selected";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErr.Text = ex.Message;
        //    }
        //}

        //protected void lbUploadA003_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fuA003.HasFile)
        //        {
        //            string fileExt = Path.GetExtension(fuA003.FileName);
        //            if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg" && fileExt.ToLower() != ".png")
        //            {
        //                lblErr.Text = "Only PNG, GIF and JPG image are accepted.";
        //                return;
        //            }
        //            string fileName = Guid.NewGuid().ToString() + fileExt;
        //            fuA003.SaveAs(Server.MapPath("~/Upload") + "\\" + fileName);
        //            imgA003.ImageUrl = "~/Upload/" + fileName;
        //            hfA003Track.Value = "Y";
        //        }
        //        else
        //        {
        //            lblErr.Text = "No file selected";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErr.Text = ex.Message;
        //    }
        //}

        //protected void lbUploadA004_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (fuA004.HasFile)
        //        {
        //            string fileExt = Path.GetExtension(fuA004.FileName);
        //            if (fileExt.ToLower() != ".gif" && fileExt.ToLower() != ".jpg" && fileExt.ToLower() != ".jpeg" && fileExt.ToLower() != ".png")
        //            {
        //                lblErr.Text = "Only PNG, GIF and JPG image are accepted.";
        //                return;
        //            }
        //            string fileName = Guid.NewGuid().ToString() + fileExt;
        //            fuA004.SaveAs(Server.MapPath("~/Upload") + "\\" + fileName);
        //            imgA004.ImageUrl = "~/Upload/" + fileName;
        //            hfA004Track.Value = "Y";
        //        }
        //        else
        //        {
        //            lblErr.Text = "No file selected";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        lblErr.Text = ex.Message;
        //    }
        //}

        //protected void lbSubmit_Click(object sender, EventArgs e)
        //{
        //    List<NewsletterBox> addBoxes = new List<NewsletterBox>();
        //    List<NewsletterBox> updateBoxes = new List<NewsletterBox>();
        //    if (hfBannerTrack.Value == "Y")
        //    {
        //        if (hfBannerMode.Value == "U")
        //            updateBoxes.Add(GetBannerBox());
        //        else
        //            addBoxes.Add(GetBannerBox());
        //    }

        //    if (hfA001Track.Value == "Y")
        //    {
        //        if (hfA001Mode.Value == "U")
        //            updateBoxes.Add(new NewsletterBox()
        //                {
        //                    BoxType = "A",
        //                    BoxID = "A001",
        //                    BoxImage = imgA001.ImageUrl,
        //                    BoxPage = 1,
        //                    NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //                });
        //        else
        //            addBoxes.Add(new NewsletterBox()
        //            {
        //                BoxType = "A",
        //                BoxID = "A001",
        //                BoxImage = imgA001.ImageUrl,
        //                BoxPage = 1,
        //                NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //            });
        //    }

        //    if (hfA002Track.Value == "Y")
        //    {
        //        if (hfA002Mode.Value == "U")
        //            updateBoxes.Add(new NewsletterBox()
        //            {
        //                BoxType = "A",
        //                BoxID = "A002",
        //                BoxImage = imgA002.ImageUrl,
        //                BoxPage = 1,
        //                NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //            });
        //        else
        //            addBoxes.Add(new NewsletterBox()
        //            {
        //                BoxType = "A",
        //                BoxID = "A002",
        //                BoxImage = imgA002.ImageUrl,
        //                BoxPage = 1,
        //                NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //            });

        //        if (hfA003Mode.Value == "U")
        //            updateBoxes.Add(new NewsletterBox()
        //            {
        //                BoxType = "A",
        //                BoxID = "A003",
        //                BoxImage = imgA003.ImageUrl,
        //                BoxPage = 1,
        //                NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //            });
        //        else
        //            addBoxes.Add(new NewsletterBox()
        //            {
        //                BoxType = "A",
        //                BoxID = "A003",
        //                BoxImage = imgA003.ImageUrl,
        //                BoxPage = 1,
        //                NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //            });
        //    }

        //    if (hfA003Track.Value == "Y")
        //    {
        //        if (hfA004Mode.Value == "U")
        //            updateBoxes.Add(new NewsletterBox()
        //            {
        //                BoxType = "A",
        //                BoxID = "A004",
        //                BoxImage = imgA004.ImageUrl,
        //                BoxPage = 1,
        //                NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //            });
        //        else
        //            addBoxes.Add(new NewsletterBox()
        //            {
        //                BoxType = "A",
        //                BoxID = "A004",
        //                BoxImage = imgA004.ImageUrl,
        //                BoxPage = 1,
        //                NewsletterID = long.Parse(Session["NewsletterID"].ToString())
        //            });
        //    }

        //    if (boxN001.BoxType != "A" && boxN001.Track)
        //    {
        //        if (boxN001.BoxMode == "U")
        //            updateBoxes.Add(boxN001.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN001.GetBoxContent());
        //    }

        //    if (boxN002.BoxType != "A" && boxN002.Track)
        //    {
        //        if (boxN002.BoxMode == "U")
        //            updateBoxes.Add(boxN002.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN002.GetBoxContent());
        //    }

        //    if (boxN003.BoxType != "A" && boxN003.Track)
        //    {
        //        if (boxN003.BoxMode == "U")
        //            updateBoxes.Add(boxN003.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN003.GetBoxContent());
        //    }

        //    if (boxN004.BoxType != "A" && boxN004.Track)
        //    {
        //        if (boxN004.BoxMode == "U")
        //            updateBoxes.Add(boxN004.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN004.GetBoxContent());
        //    }

        //    if (boxN005.BoxType != "A" && boxN005.Track)
        //    {
        //        if (boxN005.BoxMode == "U")
        //            updateBoxes.Add(boxN005.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN005.GetBoxContent());
        //    }

        //    if (boxN006.BoxType != "A" && boxN006.Track)
        //    {
        //        if (boxN006.BoxMode == "U")
        //            updateBoxes.Add(boxN006.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN006.GetBoxContent());
        //    }

        //    if (boxN007.BoxType != "A" && boxN007.Track)
        //    {
        //        if (boxN007.BoxMode == "U")
        //            updateBoxes.Add(boxN007.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN007.GetBoxContent());
        //    }

        //    if (boxN008.BoxType != "A" && boxN008.Track)
        //    {
        //        if (boxN008.BoxMode == "U")
        //            updateBoxes.Add(boxN008.GetBoxContent());
        //        else
        //            addBoxes.Add(boxN008.GetBoxContent());
        //    }

        //    BOBoxes boBoxes = new BOBoxes();

        //    boBoxes.InsertNewsletterBoxes(addBoxes);
        //    boBoxes.UpdateNewsletterBoxes(updateBoxes, long.Parse(Session["NewsletterID"].ToString()));
        //    BindNewsletter(long.Parse(Session["NewsletterID"].ToString()));
        //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "notification", "alert('A newsletter has been updated successfully');", true);
        //}

        //private NewsletterBox GetBannerBox()
        //{
        //    NewsletterBox box = new NewsletterBox();
        //    box.NewsletterID = long.Parse(Session["NewsletterID"].ToString());
        //    box.BoxID = "B001";
        //    box.BoxType = "B";
        //    box.BoxImage = imgBanner.ImageUrl;
        //    box.BoxLink = "";
        //    box.BoxData = "";
        //    box.BoxPage = 1;
        //    return box;
        //}

        //protected void lbSendEmail_Click(object sender, EventArgs e)
        //{
        //    if (Session["Email"] != null && Util.IsEmail(Session["Email"].ToString()))
        //    {
        //        long newsletterId = long.Parse(Session["NewsletterID"].ToString());
        //        string bannerUrl = (new BOBoxes()).GetBannerByNewsletter(newsletterId);

        //        string mailBody = Util.GetTemplateContent(HttpContext.Current.Server.MapPath("~/Template/EmailTemplate.htm"));
        //        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        //        mail.To.Add(Session["Email"].ToString());
        //        mail.Subject = "A newsletter is coming from jiushizhutk.com";
        //        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mailBody.Replace("[[imgLogo]]", @"<a href='http://newsletters.jiushizhutk.com/nl/" + newsletterId + @"' target='_blank' ><img src=""cid:Logo"" /></a>").Replace("[[NewsletterID]]", newsletterId.ToString()), null, "text/html");
        //        string imagePath = Server.MapPath(string.IsNullOrEmpty(bannerUrl) ? "~/Images/NewsLetterLogo.gif" : bannerUrl);
        //        string mimeType = "image/jpeg";
        //        switch (Path.GetExtension(imagePath).ToLower())
        //        {
        //            case ".gif":
        //                mimeType = "image/gif";
        //                break;
        //            case ".png":
        //                mimeType = "image/png";
        //                break;
        //        }
        //        LinkedResource imageResource = new LinkedResource(imagePath, mimeType);
        //        imageResource.ContentId = "Logo";
        //        htmlView.LinkedResources.Add(imageResource);
        //        mail.IsBodyHtml = true;
        //        mail.AlternateViews.Add(htmlView);

        //        SmtpClient client = new SmtpClient();
        //        try
        //        {
        //            client.Send(mail);
        //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "emailSent", "alert('A preview email has been sent successfully to your email address');", true);
        //        }
        //        catch (Exception ex)
        //        {
        //            lblErr.Text = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        lblErr.Text = "User email address is empty or invalid.";
        //    }
        //}
            

    }
}