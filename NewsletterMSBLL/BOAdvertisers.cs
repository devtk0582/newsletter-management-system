using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsletterMSBLL
{
    public class BOAdvertisers
    {
        private NLMSDataClassesDataContext context;

        public BOAdvertisers()
        {
            context = new NLMSDataClassesDataContext();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<Advertiser> GetAdvertisersPagedAndSorted(string searchValue, string sortExpression, int pageIndex, int pageSize)
        {
            var query = (from o in context.Advertisers
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
                query = query.Where(o => o.AdvertiserName.StartsWith(searchValue));

            if (!sortDescending)
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderBy(o => o.AdvertiserName);
                        break;
                    case "RegionType":
                        query = query.OrderBy(o => o.AdvertiserRegionType);
                        break;
                    case "ContactName":
                        query = query.OrderBy(o => o.AdvertiserContact1Name);
                        break;
                    case "ContactPhone":
                        query = query.OrderBy(o => o.AdvertiserContact1Phone);
                        break;
                    case "ContactEmail":
                        query = query.OrderBy(o => o.AdvertiserContact1Email);
                        break;
                }
            }
            else
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderByDescending(o => o.AdvertiserName);
                        break;
                    case "RegionType":
                        query = query.OrderByDescending(o => o.AdvertiserRegionType);
                        break;
                    case "ContactName":
                        query = query.OrderByDescending(o => o.AdvertiserContact1Name);
                        break;
                    case "ContactPhone":
                        query = query.OrderByDescending(o => o.AdvertiserContact1Phone);
                        break;
                    case "ContactEmail":
                        query = query.OrderByDescending(o => o.AdvertiserContact1Email);
                        break;
                }
            }

            List<Advertiser> advertisers = query.Skip(pageIndex).Take(pageSize).ToList();

            return advertisers;
        }

        public int TotalNumberOfAdvertisers(string searchValue)
        {
            var query = (from o in context.Advertisers
                         where o.Active == true
                         select o);

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(o => o.AdvertiserName.StartsWith(searchValue));

            return query.Count();
        }

        public Advertiser GetAdvertiserByID(long advertiserId)
        {
            return (from o in context.Advertisers
                    where o.AdvertiserID == advertiserId
                    select o).SingleOrDefault();
        }

        public List<Advertiser> GetAdvertisers()
        {
            return (from o in context.Advertisers
                    where o.Active == true
                    select o).ToList();
        }

        public void AddAdvertiser(string name, string regionType, string contact1Name, 
            string contact1Email, string contact1Phone, string contact1Phone2, string contact2Name,
            string contact2Email, string contact2Phone, string contact2Phone2)
        {
            Advertiser advertiser = new Advertiser();
            advertiser.AdvertiserName = name;
            advertiser.AdvertiserRegionType = regionType;
            advertiser.AdvertiserContact1Name = contact1Name;
            advertiser.AdvertiserContact1Email = contact1Email;
            advertiser.AdvertiserContact1Phone = contact1Phone;
            advertiser.AdvertiserContact1Phone2 = contact1Phone2;
            advertiser.AdvertiserContact2Name = contact2Name;
            advertiser.AdvertiserContact2Email = contact2Email;
            advertiser.AdvertiserContact2Phone = contact2Phone;
            advertiser.AdvertiserContact2Phone2 = contact2Phone2;
            advertiser.Active = true;
            context.Advertisers.InsertOnSubmit(advertiser);
            context.SubmitChanges();
        }

        public void UpdateAdvertiser(long advertiserId, string name, string regionType, string contact1Name,
            string contact1Email, string contact1Phone, string contact1Phone2, string contact2Name,
            string contact2Email, string contact2Phone, string contact2Phone2)
        {
            var advertiser = (from o in context.Advertisers
                              where o.AdvertiserID == advertiserId
                               select o).SingleOrDefault();

            if (advertiser != null)
            {
                advertiser.AdvertiserName = name;
                advertiser.AdvertiserRegionType = regionType;
                advertiser.AdvertiserContact1Name = contact1Name;
                advertiser.AdvertiserContact1Email = contact1Email;
                advertiser.AdvertiserContact1Phone = contact1Phone;
                advertiser.AdvertiserContact1Phone2 = contact1Phone2;
                advertiser.AdvertiserContact2Name = contact2Name;
                advertiser.AdvertiserContact2Email = contact2Email;
                advertiser.AdvertiserContact2Phone = contact2Phone;
                advertiser.AdvertiserContact2Phone2 = contact2Phone2;
                context.SubmitChanges();
            }
        }

        public void DeleteAdvertiser(long advertiserId)
        {
            var advertiser = (from o in context.Advertisers
                              where o.AdvertiserID == advertiserId
                        select o).SingleOrDefault();

            if (advertiser != null)
            {
                advertiser.Active = false;
                context.SubmitChanges();
            }
        }
    }
}
