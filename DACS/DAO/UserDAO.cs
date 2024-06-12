using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DACS.Models;
using PagedList;


namespace DACS.DAO
{
    public class UserDAO
    {
        DBModel db = null;
        public UserDAO()
        {
            db = new DBModel();
        }
        public long Insert(User entity)
        {
            db.Users.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }
        public bool Update(User entity)
        {
            try
            {
                var user = db.Users.Find(entity.ID);
                user.Name = entity.Name;
                if (!string.IsNullOrEmpty(entity.Password))
                {
                    user.Password = entity.Password;
                }
                user.Address = entity.Address;
                user.Email = entity.Email;
                user.ModifiedBy = entity.ModifiedBy;
                user.ModifiedDate = entity.ModifiedDate;
                user.Status = entity.Status;
                user.Phone = entity.Phone;
                db.SaveChanges();
                return true;

            }
            catch (Exception)
            {
                return false;

            }
        }
        public bool Delete(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public User GetByUserName(string username)
        {
            return db.Users.SingleOrDefault(x => x.UserName == username);
        }
        public IEnumerable<User> ListAllPaging(string searchString, int page, int pagesize)
        {
            IQueryable<User> model = db.Users;
            model = model.Where(x => x.UserName != "admin");
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.UserName.Contains(searchString) || x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(page, pagesize);
        }
        public User ViewDetail(int id)
        {

            return db.Users.Find(id);
        }
        public int Login(string userName, string passWord, bool isLoginAdmin = false)
        {
            var result = db.Users.SingleOrDefault(x => x.UserName == userName);
            if (result == null)
                return 0;
            else
            {
                if (isLoginAdmin == true)
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
                else
                {
                    if (result.Status == false)
                    {
                        return -1;
                    }
                    else
                    {
                        if (result.Password == passWord)
                            return 1;
                        else
                            return -2;
                    }
                }
            }
        }
        public bool CheckUserName(string userName)
        {
            return db.Users.Any(x => x.UserName == userName);
        }

        public bool CheckEmail(string email)
        {
            return db.Users.Any(x => x.Email == email);
        }
    }
}
