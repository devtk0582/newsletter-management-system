using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsletterMS
{
    /// <summary>
    /// Summary description for UploadHandler
    /// </summary>
    public class UploadHandler : IHttpHandler, System.Web.SessionState.IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            int newsletterId = 0;
            HttpPostedFile uploads = context.Request.Files["upload"];
            string CKEditorFuncNum = context.Request["CKEditorFuncNum"];
            string fileName = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(uploads.FileName);
            string url = "";
            if (HttpContext.Current.Session["NewsletterID"] != null)
            {
                newsletterId = Convert.ToInt32(HttpContext.Current.Session["NewsletterID"].ToString());

                string newsletterDataPath = context.Server.MapPath("~/Upload") + "\\" + newsletterId;

                if (!System.IO.Directory.Exists(newsletterDataPath))
                    System.IO.Directory.CreateDirectory(newsletterDataPath);

                uploads.SaveAs(newsletterDataPath + "\\" + fileName);
                //provide direct URL here
                url = Properties.Settings.Default.DefaultRootURL + @"/Upload/" + newsletterId + "/" + fileName; //http://www.tokimedia.com/newsletters/

                context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum +
                  ", \"" + url + "\");</script>");
                context.Response.End();
            }
            else
            {
                uploads.SaveAs(context.Server.MapPath("~/Upload") + "\\" + fileName);
                //provide direct URL here
                url = Properties.Settings.Default.DefaultRootURL + @"/Upload/" + fileName; 
                context.Response.Write("<script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum +
                  ", \"" + url + "\");</script>");
                context.Response.End();
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}