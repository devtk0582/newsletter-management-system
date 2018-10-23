using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace NewsletterMSBLL
{
    public class Util
    {
        public static bool IsEmail(string inputEmail)
        {
            string strRedex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRedex);
            if (re.IsMatch(inputEmail))
                return true;
            else
                return false;
        }

        public static string ParseVideoLink(string link)
        {
            Match result = Regex.Match(link, @"^.*(youtu.be\/|v\/|u\/\w\/|embed\/|watch\?v=|\&v=)([^#\&\?]*).*");
            string videoId = "";
            if (result != null && result.Groups.Count > 0)
            {
                videoId = result.Groups[2].Value;
            }

            return videoId;
        }

        public static string GetTemplateContent(string templatePath)
        {
            string mailBody = String.Empty;

            using (StreamReader sr = new StreamReader(templatePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    mailBody += line;
                }
            }
            return mailBody;
        }

        public static string GetNewsletterFileName(string name)
        {
            return Regex.Replace(name.Replace(' ', '-').ToLower(), "[^a-z0-9]", "") + ".htm";
        }
    }
}
