using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsletterMSBLL
{
    public class BOAdmins
    {
        private NLMSDataClassesDataContext context;

        public BOAdmins()
        {
            context = new NLMSDataClassesDataContext();
        }

        public AdminUser AuthenticateUser(string username, string password)
        {
            return (from user in context.AdminUsers
                    where user.UserID == username && user.Password == password
                    && user.Active == true
                    select user).SingleOrDefault();
        }

        public string[] SearchAdmins(string userid)
        {
            return (from user in context.AdminUsers
                    where user.UserID.StartsWith(userid)
                    && user.Active == true
                    select user.UserID).Take(5).ToArray();
        }

        public AdminUser GetAdminByID(long userId)
        {
            return (from u in context.AdminUsers
                    where u.AdminUserID == userId
                    select u).SingleOrDefault();
        }

        public List<long> GetAdminNewsletters(long userId)
        {
            return (from u in context.AdminNewsletters
                    where u.AdminUserID == userId
                    select u.NewsletterID).ToList();
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, false)]
        public List<AdminUser> GetAdminsPagedAndSorted(string searchValue, string sortExpression, int pageIndex, int pageSize)
        {
            var query = (from user in context.AdminUsers
                         where user.Active == true
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
                query = query.Where(o => o.UserID.StartsWith(searchValue));

            if (!sortDescending)
            {
                switch (sortExpression)
                {
                    case "UserID":
                    default:
                        query = query.OrderBy(o => o.UserID);
                        break;
                    case "Name":
                        query = query.OrderBy(o => o.Name);
                        break;
                    case "Phone":
                        query = query.OrderBy(o => o.Phone);
                        break;
                    case "Role":
                        query = query.OrderBy(o => o.Role);
                        break;
                    case "ContactEmail":
                        query = query.OrderBy(o => o.ContactEmail);
                        break;
                }
            }
            else
            {
                switch (sortExpression)
                {
                    case "UserID":
                    default:
                        query = query.OrderByDescending(o => o.UserID);
                        break;
                    case "Name":
                        query = query.OrderByDescending(o => o.Name);
                        break;
                    case "Phone":
                        query = query.OrderByDescending(o => o.Phone);
                        break;
                    case "Role":
                        query = query.OrderByDescending(o => o.Role);
                        break;
                    case "ContactEmail":
                        query = query.OrderByDescending(o => o.ContactEmail);
                        break;
                }
            }

            List<AdminUser> users = query.Skip(pageIndex).Take(pageSize).ToList();

            return users;
        }

        public int TotalNumberOfAdmins(string searchValue)
        {
            var query = (from user in context.AdminUsers
                         where user.Active == true
                         select user);

            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(o => o.UserID.StartsWith(searchValue));

            return query.Count();
        }

        public void AddAdmin(string userName, string password, string name, string role, string phone, string email, List<long> newsletterIds)
        {
            AdminUser newUser = new AdminUser();
            newUser.UserID = userName;
            newUser.Password = password;
            newUser.Name = name;
            newUser.Role = role;
            newUser.ContactEmail = email;
            newUser.Phone = phone;

            if (newsletterIds.Count > 0)
            {
                foreach (long newsletterId in newsletterIds)
                {
                    newUser.AdminNewsletters.Add(new AdminNewsletter()
                    {
                        NewsletterID = newsletterId,
                        AddDate = DateTime.Now
                    });
                }
            }

            newUser.Active = true;

            context.AdminUsers.InsertOnSubmit(newUser);
            context.SubmitChanges();
        }

        public void UpdateAdmin(long userId, string userName, string name, string role, string phone, string email, List<long> newsletterIds)
        {
            var user = (from u in context.AdminUsers
                        where u.AdminUserID == userId
                        select u).SingleOrDefault();

            if (user != null)
            {
                user.UserID = userName;
                user.Name = name;
                user.Role = role;
                user.ContactEmail = email;
                user.Phone = phone;

                context.AdminNewsletters.DeleteAllOnSubmit(context.AdminNewsletters.Where(o => o.AdminUserID == user.AdminUserID).ToList());
                foreach (long newsletterId in newsletterIds)
                {
                    user.AdminNewsletters.Add(new AdminNewsletter()
                    {
                        NewsletterID = newsletterId,
                        AddDate = DateTime.Now
                    });
                }
                context.SubmitChanges();
            }
        }

        public void DeleteAdmin(long userId)
        {
            var user = (from u in context.AdminUsers
                        where u.AdminUserID == userId
                        select u).SingleOrDefault();
            if (user != null)
            {
                user.Active = false;
                context.SubmitChanges();
            }
        }

        public void UpdateProfile(long userId, string name, string password, bool updatePassword, string email, string phone)
        {
            var user = (from o in context.AdminUsers
                        where o.AdminUserID == userId
                        select o).SingleOrDefault();

            if (user != null)
            {
                user.Name = name;
                if(updatePassword)
                    user.Password = password;
                user.ContactEmail = email;
                user.Phone = phone;
                context.SubmitChanges();
            }
        }
    }
}
