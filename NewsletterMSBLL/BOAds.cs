using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsletterMSBLL
{
    public class BOAds
    {
        private NLMSDataClassesDataContext context;

        public BOAds()
        {
            context = new NLMSDataClassesDataContext();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<Ad> GetAdsPagedAndSorted(string searchValue, string sortExpression, int pageIndex, int pageSize)
        {
            var query = (from o in context.Ads
                         where o.Active == true
                         select o);

            bool sortDescending = false;
            if (!String.IsNullOrEmpty(sortExpression))
            {
                string[] values = sortExpression.Split(' ');
                sortExpression = values[0];
                if (values.Length > 1)
                {
                    sortDescending = values[1] == "DESC";
                }
            }

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(o => o.AdDescription.StartsWith(searchValue));

            if (!sortDescending)
            {
                switch (sortExpression)
                {
                    case "Desc":
                    default:
                        query = query.OrderBy(o => o.AdDescription);
                        break;
                    case "Campaign":
                        query = query.OrderBy(o => o.AdCampaign);
                        break;
                    case "Type":
                        query = query.OrderBy(o => o.AdType);
                        break;
                    case "Region":
                        query = query.OrderBy(o => o.AdRegionCode);
                        break;
                    case "Price":
                        query = query.OrderBy(o => o.AdPrice);
                        break;
                    case "Status":
                        query = query.OrderBy(o => o.Active);
                        break;
                }
            }
            else
            {
                switch (sortExpression)
                {
                    case "Desc":
                    default:
                        query = query.OrderByDescending(o => o.AdDescription);
                        break;
                    case "Campaign":
                        query = query.OrderByDescending(o => o.AdCampaign);
                        break;
                    case "Region":
                        query = query.OrderByDescending(o => o.AdRegionCode);
                        break;
                    case "Type":
                        query = query.OrderByDescending(o => o.AdType);
                        break;
                    case "Price":
                        query = query.OrderByDescending(o => o.AdPrice);
                        break;
                    case "Status":
                        query = query.OrderByDescending(o => o.Active);
                        break;
                }
            }

            List<Ad> ads = query.Skip(pageIndex).Take(pageSize).ToList();

            return ads;
        }

        public int TotalNumberOfAds(string searchValue)
        {
            var query = (from o in context.Ads
                         where o.Active == true
                         select o);

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(o => o.AdDescription.StartsWith(searchValue));

            return query.Count();
        }

        public Ad GetAdByID(long adId)
        {
            return (from o in context.Ads
                    where o.AdID == adId
                    select o).SingleOrDefault();
        }

        public List<Ad> GetAdsByAdvertiser(int advertiserId)
        {
            return (from o in context.Ads
                    where o.AdvertiserID == advertiserId
                    && o.Active == true
                    select o).ToList();
        }

        public void AddAd(long advertiserId, string description, string campaign, int region, 
            int type, decimal price, string instruction, string link, string image, string video, string content)
        {
            Ad adEntry = new Ad();
            adEntry.AdvertiserID = advertiserId;
            adEntry.AdDescription = description;
            adEntry.AdCampaign = campaign;
            adEntry.AdRegionCode = region;
            adEntry.AdInstruction = instruction;
            adEntry.AdPrice = price;
            adEntry.AdType = type;
            adEntry.AdLink = link;
            adEntry.AdImage = image;
            adEntry.AdVideo = video;
            adEntry.AdContent = content;
            adEntry.Active = true;
            context.Ads.InsertOnSubmit(adEntry);
            context.SubmitChanges();
        }

        public void UpdateAd(long adId, long advertiserId, string description, string campaign, int region,
            int type, decimal price, string instruction, string link, string image, string video, string content)
        {
            var adEntry = (from o in context.Ads
                              where o.AdID == adId
                               select o).SingleOrDefault();

            if (adEntry != null)
            {
                adEntry.AdvertiserID = advertiserId;
                adEntry.AdDescription = description;
                adEntry.AdCampaign = campaign;
                adEntry.AdRegionCode = region;
                adEntry.AdPrice = price;
                adEntry.AdInstruction = instruction;
                adEntry.AdType = type;
                adEntry.AdLink = link;
                adEntry.AdImage = image;
                adEntry.AdVideo = video;
                adEntry.AdContent = content;
                context.SubmitChanges();
            }
        }

        public void DeleteAd(long adId)
        {
            var adEntry = (from o in context.Ads
                              where o.AdID == adId
                        select o).SingleOrDefault();

            if (adEntry != null)
            {
                adEntry.Active = false;
                context.SubmitChanges();
            }
        }

        public List<AdType> GetAdTypes()
        {
            return (from o in context.AdTypes
                    select o).ToList();
        }

        public List<AdRegion> GetAdRegions()
        {
            return (from o in context.AdRegions
                    select o).ToList();
        }
    }
}
