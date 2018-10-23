using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace NewsletterMSBLL
{
    public class BONewsletterEntities
    {
        private NLMSDataClassesDataContext context;

        public BONewsletterEntities()
        {
            context = new NLMSDataClassesDataContext();
        }

        public NewsletterEntitySectionView GetEntitiesByNewsletterID(int newsletterID)
        {
            var news = (from e in context.NewsletterEntities
                        where e.NewsletterID == newsletterID
                        select new NewsletterEntityView()
                        {
                            EntityID = e.EntityID,
                            Type = e.Type,
                            Content = e.Content,
                            Section = e.Section
                        }).ToList();

            var sections = (from s in context.NewsletterSections
                            select new NewsletterSectionView()
                            {
                                Name = s.Name,
                                Code = s.Code
                            }).ToList();

            foreach (NewsletterSectionView sv in sections)
            {
                sv.Count = news.Where(o => o.Section == sv.Code).Count();
            }

            return new NewsletterEntitySectionView()
            {
                News = news,
                Sections = sections
            };
        }

        public NewsletterEntitySectionView GetEntitiesByNewsletterID(string nid)
        {
            Guid newsUniqueID;

            if (Guid.TryParse(nid, out newsUniqueID))
            {
                var news = (from e in context.NewsletterEntities
                            join n in context.Newsletters on e.NewsletterID equals n.NewsletterID
                            where n.UniqueID != null && n.UniqueID.Value == newsUniqueID && e.Content.Trim() != ""
                            select new NewsletterEntityView()
                            {
                                EntityID = e.EntityID,
                                Type = e.Type,
                                Content = e.Content,
                                Section = e.Section
                            }).ToList();

                var sections = (from s in context.NewsletterSections
                                select new NewsletterSectionView()
                                {
                                    Name = s.Name,
                                    Code = s.Code
                                }).ToList();

                foreach (NewsletterSectionView sv in sections)
                {
                    sv.Count = news.Where(o => o.Section == sv.Code).Count();
                }

                return new NewsletterEntitySectionView()
                {
                    News = news,
                    Sections = sections
                };
            }
            else
                return null;
        }

        public void AddNewsletterEntity()
        {

        }

        public bool UploadNewsletterEntities(int newsletterID, List<NewsletterEntityView> entities, string mode)
        {
            bool result = false;
            Newsletter newsletter;
            List<NewsletterSection> sections;
            using (NLMSDataClassesDataContext context = new NLMSDataClassesDataContext())
            {
                newsletter = (from n in context.Newsletters
                              where n.NewsletterID == newsletterID
                              select n).SingleOrDefault();

                sections = (from s in context.NewsletterSections
                            select s).ToList();

                var existingEntities = (from e in context.NewsletterEntities
                                        where e.NewsletterID == newsletterID
                                        select e).ToList();

                int currentID = existingEntities.Max(o => o.EntityID) + 1;

                foreach (NewsletterEntityView entity in entities)
                {
                    var record = existingEntities.Where(o => o.EntityID == entity.EntityID).SingleOrDefault();

                    if (record != null)
                    {
                        record.Content = entity.Content;
                    }
                    else
                    {
                        var newEntity = new NewsletterEntity()
                        {
                            NewsletterID = newsletterID,
                            EntityID = currentID,
                            Content = entity.Content != null ? entity.Content.Trim() : "",
                            Type = entity.Type,
                            Section = entity.Section
                        };
                        context.NewsletterEntities.InsertOnSubmit(newEntity);
                        currentID++;
                    }
                }

                context.SubmitChanges();
                result = true;
            }

            if (mode == "S" && result)
            {
                GenerateNewsletterFile(newsletter, entities, sections);
            }

            return result;
        }

        private void GenerateNewsletterFile(Newsletter newsletter, List<NewsletterEntityView> entities, List<NewsletterSection> sections)
        {
            string destPath = System.Web.HttpContext.Current.Server.MapPath("~/Archive") + "\\" + Util.GetNewsletterFileName(newsletter.NewsletterName);

            StringBuilder sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html>
<html xmlns=""http://www.w3.org/1999/xhtml"">
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <meta name=""description"" content="""" />
    <meta name=""author"" content="""" />
    <title>Newsletters</title>
    <link href=""../Styles/main.css"" rel=""stylesheet"" type=""text/css"" />
    <link href=""../Styles/bootstrap.min.css"" rel=""stylesheet"" type=""text/css"" />
    <script src=""../Scripts/jquery.js"" type=""text/javascript""></script>
    <script src=""../Scripts/bootstrap.min.js"" type=""text/javascript""></script>
    <script src=""../Scripts/jquery.formatDateTime.js"" type=""text/javascript""></script>
</head>
<body>
    ");

            if (!string.IsNullOrEmpty(newsletter.BackgroundColor))
            {
                sb.Append(@"<div style=""background-color: #");
                sb.Append(newsletter.BackgroundColor);
                sb.Append(@""">");
            }

            sb.Append(@"<!-- Navigation -->
    <nav class=""navbar navbar-inverse navbar-fixed-top"" role=""navigation"">
        <div class=""container"">
            <div class=""navbar-header"">
                <button type=""button"" class=""navbar-toggle"" data-toggle=""collapse"" data-target=""#bs-example-navbar-collapse-1"">
                    <span class=""sr-only"">Toggle navigation</span> <span class=""icon-bar""></span><span
                        class=""icon-bar""></span><span class=""icon-bar""></span>
                </button>
                <a class=""navbar-brand"" style=""cursor: hand;cursor: pointer;"" onclick=""$('html,body').scrollTop(0);"">Home</a>
            </div>
        </div>
    </nav>");

            sb.Append(@"<div class=""container"">");
            sb.Append(@"<div class=""row"">
            <div class=""col-lg-12 portfolio-item"">
                <div class=""row"" style=""padding: 5px;"">
                    <div class=""col-lg-12"">");
            var headerBanner = entities.Where(o => o.Type == "H" && o.EntityID == 1).SingleOrDefault();
            sb.Append(@"<div class=""row"">
                            <div class=""col-lg-12 portfolio-item"" id=""divHeaderBanner"">");
            sb.Append(@"<div class='row' style='margin-top:20px'>");
            sb.Append(headerBanner.Content);
            sb.Append(@"</div>");
            sb.Append(@"</div>
                        </div>
                    </div>");
            sb.Append(@"</div>
                <div class=""row"" style=""padding: 5px;"">
                    <div class=""col-lg-12 portfolio-item"" id=""divHeaderMain"">");
            var headerEntities = entities.Where(o => o.Type == "H" && o.EntityID > 1 && o.Content.Trim() != "").OrderBy(o => o.EntityID).ToList();
            if (headerEntities.Count > 0)
            {
                foreach (NewsletterEntityView headerEntity in headerEntities)
                {
                    sb.Append(@"<div class='row' style='margin-top:20px'>");
                    sb.Append(headerEntity.Content);
                    sb.Append(@"</div>");
                }
            }

            sb.Append(@"</div>
                </div>
            </div>
        </div>");
            sb.Append(@"<div class=""row"" id=""divMain"">
            <div class=""col-lg-8"">
                <div class=""row"" style=""padding: 5px;"">
                    <div class=""col-lg-12 portfolio-item"" id=""divDefaultBoxes"">");
            var newsEntities = entities.Where(o => o.Type == "N" && o.Content.Trim() != "").OrderBy(o => o.Section).ThenBy(o => o.EntityID).ToList();
            if (newsEntities.Count > 0)
            {
                string currentSection = "";
                string currentSectionName = "";
                foreach (NewsletterEntityView newsEntity in newsEntities)
                {
                    if (currentSection != newsEntity.Section)
                    {
                        if (currentSection != "")
                            sb.Append(@"</div></div></div>");

                        currentSection = newsEntity.Section;
                        currentSectionName = sections.Where(o => o.Code == newsEntity.Section).Select(o => o.Name).SingleOrDefault();
                        sb.Append(@"<div style='margin-bottom: 20px;' >");
                        if (!string.IsNullOrEmpty(newsletter.SectionColor))
                        {
                            sb.Append(@"<div class=""panel panel-primary"" style=""border-color: #");
                            sb.Append(newsletter.SectionColor);
                            sb.Append(@""">");
                            sb.Append(@"<div class=""panel-heading"" style=""background-color: #");
                            sb.Append(newsletter.SectionColor);
                            sb.Append("; border-color: #");
                            sb.Append(newsletter.SectionColor);
                            sb.Append(@""">");
                        }
                        else
                            sb.Append(@"<div class=""panel panel-primary""><div class=""panel-heading"">");

                        sb.Append(@"<h3 class=""panel-title"">");
                        sb.Append(currentSectionName);
                        sb.Append(@"</h3></div><div class=""panel-body"" id=""section");
                        sb.Append(newsEntity.Section);
                        sb.Append(@""">");
                    }
                    sb.Append(@"<div class='row' style='margin-top:20px'>");
                    sb.Append(newsEntity.Content);
                    sb.Append(@"</div>");
                }
                sb.Append(@"</div></div></div>");
            }

            sb.Append(@"</div>
                </div>
            </div>
            <div class=""col-lg-4"">
                <div class=""row"" style=""padding: 5px;"">
                    <div class=""col-lg-12 portfolio-item"" id=""divAdMain"">");
            var adEntities = entities.Where(o => o.Type == "A" && o.Content.Trim() != "").OrderBy(o => o.EntityID).ToList();
            if (adEntities.Count > 0)
            {
                foreach (NewsletterEntityView adEntity in adEntities)
                {
                    sb.Append(@"<div class='row' style='margin-top:20px'>");
                    sb.Append(adEntity.Content);
                    sb.Append(@"</div>");
                }
            }

            sb.Append(@"</div>
                </div>
            </div>
        </div>
        <hr>
    <!-- Footer -->
    <footer>
        <div class=""row"">
            <div class=""col-lg-12"">
                <p>
                    Copyright &copy; 2015</p>
            </div>
        </div>
        <!-- /.row -->
    </footer>
    </div>");

            if (!string.IsNullOrEmpty(newsletter.BackgroundColor))
                sb.Append(@"</div>");

            sb.Append(@"
    </body>
</html>");

            File.WriteAllText(destPath, sb.ToString());
        }

        public NewsletterMainView GetNewsletterViewByNewsletterID(int newsletterID)
        {
            var newsletter = new NewsletterMainView();

            newsletter.News = (from e in context.NewsletterEntities
                               where e.NewsletterID == newsletterID
                               select new NewsletterEntityView()
                               {
                                   EntityID = e.EntityID,
                                   Type = e.Type,
                                   Content = e.Content
                               }).ToList();

            return newsletter;
        }

        public Guid GetNewsletterUniqueID(int newsletterID)
        {
            return (from n in context.Newsletters
                    where n.NewsletterID == newsletterID && n.UniqueID != null
                    select n.UniqueID.Value).SingleOrDefault();
        }
    }

    [Serializable]
    public class NewsletterEntityView
    {
        public int EntityID { get; set; }
        public string Type { get; set; }
        public string Content { get; set; }
        public string Section { get; set; }
    }

    [Serializable]
    public class NewsletterSectionView
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
    }

    [Serializable]
    public class NewsletterMainView
    {
        public List<NewsletterEntityView> News { get; set; }
        public List<NewsletterEntityView> Ads { get; set; }
    }

    [Serializable]
    public class NewsletterEntitySectionView
    {
        public List<NewsletterEntityView> News { get; set; }
        public List<NewsletterSectionView> Sections { get; set; }
    }
}
