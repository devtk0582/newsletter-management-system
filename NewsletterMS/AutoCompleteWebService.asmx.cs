using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using NewsletterMSBLL;

namespace NewsletterMS
{
    /// <summary>
    /// Summary description for AutoCompleteWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class AutoCompleteWebService : System.Web.Services.WebService
    {

        [WebMethod]
        public string[] SearchAdmins(string prefixText, int count)
        {
            try
            {
                return (new BOAdmins()).SearchAdmins(prefixText);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
