using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NewsletterMSBLL;
using System.Web.SessionState;
using System.Web.Script.Services;

namespace NewsletterMS
{
    /// <summary>
    /// Summary description for NewsletterWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class NewsletterWebService : System.Web.Services.WebService, IReadOnlySessionState 
    {

        [WebMethod(EnableSession=true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public NewsletterEntitySectionView GetNewsletterEntities()
        {
            if (HttpContext.Current.Session["NewsletterID"] != null)
                return (new BONewsletterEntities()).GetEntitiesByNewsletterID(int.Parse(HttpContext.Current.Session["NewsletterID"].ToString()));
            else
                return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public NewsletterEntitySectionView GetNewsletterEntitiesByUniqueID(string nid)
        {
           return (new BONewsletterEntities()).GetEntitiesByNewsletterID(nid);
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool UploadNewsletterEntities(List<NewsletterEntityView> input, string mode)
        {
            if (HttpContext.Current.Session["NewsletterID"] != null)
                return (new BONewsletterEntities()).UploadNewsletterEntities(int.Parse(HttpContext.Current.Session["NewsletterID"].ToString()), input, mode);
            else
                return false;
        }
    }
}
