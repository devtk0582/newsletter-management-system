using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsletterMSBLL
{
    public class BOBoxes
    {
        private NLMSDataClassesDataContext context;

        public BOBoxes()
        {
            context = new NLMSDataClassesDataContext();
        }

        public string GetBannerByNewsletter(long newsletterId)
        {
            var bannerBox = (from o in context.NewsletterBoxes
                             where o.NewsletterID == newsletterId
                                && o.BoxType == "B"
                             select o).SingleOrDefault();

            if (bannerBox != null)
                return bannerBox.BoxImage;
            else
                return "";
        }

        public void InsertNewsletterBoxes(List<NewsletterBox> boxes)
        {
            context.NewsletterBoxes.InsertAllOnSubmit(boxes);
            context.SubmitChanges();
        }

        public void UpdateNewsletterBoxes(List<NewsletterBox> boxes, long newsletterId)
        {
            foreach (NewsletterBox box in boxes)
            {
                NewsletterBox originBox = (from o in context.NewsletterBoxes
                                           where o.NewsletterID == newsletterId
                                            && o.BoxID == box.BoxID
                                           select o).SingleOrDefault();
                if (originBox != null)
                {
                    originBox.BoxData = box.BoxData;
                    originBox.BoxType = box.BoxType;
                    originBox.BoxLink = box.BoxLink;
                    originBox.BoxImage = box.BoxImage;
                    context.SubmitChanges();
                }
            }
        }

        public List<NewsletterBox> GetNewsletterBoxes(long newsletterId)
        {
            return (from o in context.NewsletterBoxes
                    where o.NewsletterID == newsletterId
                    select o).ToList();
        }

        public NewsletterBox GetNewsletterBoxByID(long newsletterId, string boxId)
        {
            return (from o in context.NewsletterBoxes
                    where o.BoxID == boxId && o.NewsletterID == newsletterId
                    select o).SingleOrDefault();
        }

        public List<NewsletterSection> GetNewsletterSections()
        {
            return context.NewsletterSections.ToList();
        }
    }
}
