using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NewsletterMSBLL
{
    public class BOPublications
    {
        private NLMSDataClassesDataContext context;

        public BOPublications()
        {
            context = new NLMSDataClassesDataContext();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<Newsletter> GetPublicationsPagedAndSorted(string searchValue, string sortExpression, int pageIndex, int pageSize)
        {
            var query = (from o in context.Newsletters
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
                query = query.Where(o => o.NewsletterName.StartsWith(searchValue));

            if (!sortDescending)
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderBy(o => o.NewsletterName);
                        break;
                    case "Type":
                        query = query.OrderBy(o => o.NewsletterType);
                        break;
                    case "Frequency":
                        query = query.OrderBy(o => o.NewsletterFrequency);
                        break;
                    case "ContactName":
                        query = query.OrderBy(o => o.PrimaryContactName);
                        break;
                    case "ContactPhone":
                        query = query.OrderBy(o => o.PrimaryContactPhone);
                        break;
                    case "ContactEmail":
                        query = query.OrderBy(o => o.PrimaryContactEmail);
                        break;
                }
            }
            else
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderByDescending(o => o.NewsletterName);
                        break;
                    case "Type":
                        query = query.OrderByDescending(o => o.NewsletterType);
                        break;
                    case "Frequency":
                        query = query.OrderByDescending(o => o.NewsletterFrequency);
                        break;
                    case "ContactName":
                        query = query.OrderByDescending(o => o.PrimaryContactName);
                        break;
                    case "ContactPhone":
                        query = query.OrderByDescending(o => o.PrimaryContactPhone);
                        break;
                    case "ContactEmail":
                        query = query.OrderByDescending(o => o.PrimaryContactEmail);
                        break;
                }
            }

            List<Newsletter> publications = query.Skip(pageIndex).Take(pageSize).ToList();

            return publications;
        }

        public int TotalNumberOfPublications(string searchValue)
        {
            var query = (from o in context.Newsletters
                         where o.Active == true
                         select o);

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(o => o.NewsletterName.StartsWith(searchValue));

            return query.Count();
        }

        public Newsletter GetPublicationByID(long publicationId)
        {
            return (from o in context.Newsletters
                    where o.NewsletterID == publicationId
                    select o).SingleOrDefault();
        }

        public string GetPublicationTypeByID(long publicationId)
        {
            var newsletter = (from o in context.Newsletters
                              where o.NewsletterID == publicationId
                              select o).SingleOrDefault();
            if (newsletter == null)
                return "";
            else
                return newsletter.NewsletterType;
        }

        public string GetNewsletterFrequency(long newsletterId)
        {
            var newsletter = (from o in context.Newsletters
                              where o.NewsletterID == newsletterId
                              select o).SingleOrDefault();

            if (newsletter != null)
                return newsletter.NewsletterFrequency;
            else
                return "";
        }

        public List<Newsletter> GetPublications()
        {
            return (from o in context.Newsletters
                    where o.Active == true
                    select o).ToList();
        }

        public string AddPublication(string name, string type, string frequency, string contactName, string contactEmail, string contactPhone, string bgColor, string scColor)
        {
            Guid uniqueId = Guid.NewGuid();
            string result = "";
            Newsletter publication = new Newsletter();
            publication.NewsletterName = name;
            publication.NewsletterType = type;
            publication.NewsletterFrequency = frequency;
            publication.PrimaryContactName = contactName;
            publication.PrimaryContactEmail = contactEmail;
            publication.PrimaryContactPhone = contactPhone;
            publication.UniqueID = uniqueId;
            publication.BackgroundColor = bgColor;
            publication.SectionColor = scColor;
            publication.Active = true;

            publication.NewsletterEntities.Add(new NewsletterEntity()
            {
                EntityID = 1,
                Type = "H",
                Content = "",
                Section = ""
            });

            context.Newsletters.InsertOnSubmit(publication);
            context.SubmitChanges();
            if (publication.UniqueID.HasValue)
            {
                result = publication.UniqueID.Value.ToString();
                Directory.CreateDirectory(System.Web.HttpContext.Current.Server.MapPath("~/Upload") + "\\" + publication.NewsletterID.ToString());
            }
            return result;
        }

        public void UpdatePublication(long publicationId, string name, string type, string frequency, string contactName, string contactEmail, string contactPhone, string bgColor, string scColor)
        {
            var publication = (from o in context.Newsletters
                               where o.NewsletterID == publicationId
                               select o).SingleOrDefault();

            if (publication != null)
            {
                publication.NewsletterName = name;
                publication.NewsletterType = type;
                publication.NewsletterFrequency = frequency;
                publication.PrimaryContactName = contactName;
                publication.PrimaryContactEmail = contactEmail;
                publication.PrimaryContactPhone = contactPhone;
                publication.BackgroundColor = bgColor;
                publication.SectionColor = scColor;
                context.SubmitChanges();
            }
        }

        public void DeletePublication(long publicationId)
        {
            var publication = (from o in context.Newsletters
                        where o.NewsletterID == publicationId
                        select o).SingleOrDefault();
            if (publication != null)
            {
                publication.Active = false;
                context.SubmitChanges();
            }
        }

        public long GetPublicationByLocalAdmin(int adminId)
        {
            var adminUser = (from a in context.AdminUsers
                             where a.NewsletterID != null && a.AdminUserID == adminId
                             select a).SingleOrDefault();
            if (adminUser != null)
            {
                var publication = adminUser.Newsletter;
                return publication.NewsletterID;
            }
            else
            {
                return 0;
            }
        }

        public bool CheckIfPublicationExists(string name)
        {
            return context.Newsletters.ToList().Where(o => Regex.Replace(o.NewsletterName.Replace(' ', '-').ToLower(), "[^a-z0-9]", "") == Regex.Replace(name.Replace(' ', '-').ToLower(), "[^a-z0-9]", "")).Count() > 0;
        }
    }

    public class BOSections
    {
        private NLMSDataClassesDataContext context;

        public BOSections()
        {
            context = new NLMSDataClassesDataContext();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<NewsletterSection> GetSectionsPagedAndSorted(string sortExpression, int pageIndex, int pageSize)
        {
            var query = (from o in context.NewsletterSections
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

            if (!sortDescending)
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderBy(o => o.Name);
                        break;
                    case "Code":
                        query = query.OrderBy(o => o.Code);
                        break;
                }
            }
            else
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderByDescending(o => o.Name);
                        break;
                    case "Code":
                        query = query.OrderByDescending(o => o.Code);
                        break;
                }
            }

            List<NewsletterSection> sections = query.Skip(pageIndex).Take(pageSize).ToList();

            return sections;
        }

        public int TotalNumberOfSections()
        {
            var query = (from o in context.NewsletterSections
                         select o);

            return query.Count();
        }

        public NewsletterSection GetSectionByID(int sectionId)
        {
            return (from o in context.NewsletterSections
                    where o.ID == sectionId
                    select o).SingleOrDefault();
        }

        public void AddSection(string name, string code)
        {
            NewsletterSection section = new NewsletterSection();
            section.Name = name;
            section.Code = code;

            context.NewsletterSections.InsertOnSubmit(section);
            context.SubmitChanges();
        }

        public void UpdateSection(int sectionId, string name)
        {
            var section = (from o in context.NewsletterSections
                               where o.ID == sectionId
                               select o).SingleOrDefault();

            if (section != null)
            {
                section.Name = name;
                context.SubmitChanges();
            }
        }

        public void DeleteSection(int sectionId)
        {
            var section = (from o in context.NewsletterSections
                               where o.ID == sectionId
                               select o).SingleOrDefault();
            if (section != null)
            {
                context.NewsletterSections.DeleteOnSubmit(section);
                context.SubmitChanges();
            }
        }

        public string GetNewSectionCode()
        {
            int maxId = context.NewsletterSections.Max(o => o.ID);

            if (maxId > 0)
                return "S" + (maxId + 1).ToString();
            else
                return "S1";
        }
    }
}
