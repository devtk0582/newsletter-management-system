using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace NewsletterMSBLL
{
    public class BOUsers
    {
        private NLMSDataClassesDataContext context;

        public BOUsers()
        {
            context = new NLMSDataClassesDataContext();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<NewsletterUser> GetNewsletterUsersPagedAndSorted(string searchValue, long newsletterId, string sortExpression, int pageIndex, int pageSize)
        {
            var query = (from user in context.NewsletterUsers
                         where user.NewsletterID == newsletterId
                         select user);

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
                query = query.Where(o => o.UserName.StartsWith(searchValue));

            if (!sortDescending)
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderBy(o => o.UserName);
                        break;
                    case "Email":
                        query = query.OrderBy(o => o.UserEmail);
                        break;
                    case "Mobile":
                        query = query.OrderBy(o => o.UserMobile);
                        break;
                    case "Phone":
                        query = query.OrderBy(o => o.UserPhone);
                        break;
                    case "City":
                        query = query.OrderBy(o => o.UserCity);
                        break;
                    case "State":
                        query = query.OrderBy(o => o.UserState);
                        break;
                    case "Zip":
                        query = query.OrderBy(o => o.UserZip);
                        break;
                }
            }
            else
            {
                switch (sortExpression)
                {
                    case "Name":
                    default:
                        query = query.OrderByDescending(o => o.UserName);
                        break;
                    case "Email":
                        query = query.OrderByDescending(o => o.UserEmail);
                        break;
                    case "Mobile":
                        query = query.OrderByDescending(o => o.UserMobile);
                        break;
                    case "Phone":
                        query = query.OrderByDescending(o => o.UserPhone);
                        break;
                    case "City":
                        query = query.OrderByDescending(o => o.UserCity);
                        break;
                    case "State":
                        query = query.OrderByDescending(o => o.UserState);
                        break;
                    case "Zip":
                        query = query.OrderByDescending(o => o.UserZip);
                        break;
                }
            }

            List<NewsletterUser> users = query.Skip(pageIndex).Take(pageSize).ToList();

            return users;
        }

        public int TotalNumberOfNewsletterUsers(string searchValue, long newsletterId)
        {
            var query = (from user in context.NewsletterUsers
                         where user.NewsletterID == newsletterId
                         select user);

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(o => o.UserName.StartsWith(searchValue));

            return query.Count();
        }

        public NewsletterUser GetNewsletterUserByID(long userId)
        {
            return (from u in context.NewsletterUsers
                    where u.UserID == userId
                    select u).SingleOrDefault();
        }

        public void AddNewsletterUser(long newsletterId, string userName, string userEmail, string userMobile, 
            string userPhone, string userCity, string userState, string userZip)
        {
            NewsletterUser newUser = new NewsletterUser();
            newUser.UserName = userName;
            newUser.UserEmail = userEmail;
            newUser.UserMobile = userMobile;
            newUser.UserPhone = userPhone;
            newUser.UserCity = userCity;
            newUser.UserState = userState;
            newUser.UserZip = userZip;
            newUser.NewsletterID = newsletterId;

            context.NewsletterUsers.InsertOnSubmit(newUser);
            context.SubmitChanges();
        }

        public void UpdateNewsletterUser(long userId, string userName, string userEmail, string userMobile,
            string userPhone, string userCity, string userState, string userZip)
        {
            var user = (from u in context.NewsletterUsers
                        where u.UserID == userId
                        select u).SingleOrDefault();

            if (user != null)
            {
                user.UserName = userName;
                user.UserEmail = userEmail;
                user.UserMobile = userMobile;
                user.UserPhone = userPhone;
                user.UserCity = userCity;
                user.UserState = userState;
                user.UserZip = userZip;
                context.SubmitChanges();
            }
        }

        public void DeleteNewsletterUser(long userId)
        {
            var user = (from u in context.NewsletterUsers
                        where u.UserID == userId
                        select u).SingleOrDefault();
            if (user != null)
            {
                context.NewsletterUsers.DeleteOnSubmit(user);
                context.SubmitChanges();
            }
        }

        public int UploadNewsletterUsers(string userList, long newsletterId)
        {
            int result = 0;
            try
            {
                result = context.UploadUserList(userList, newsletterId);
            }
            catch (Exception ex)
            {
                result = 0;
            }

            return result;
        }
    }
}
