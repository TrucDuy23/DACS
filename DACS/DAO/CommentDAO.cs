using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DACS.Models;
using DACS.ViewModel;
using PagedList;

namespace DACS.DAO
{
    public class CommentDAO
    {
        DBModel db = null;
        public CommentDAO()
        {
            db = new DBModel();
        }
        public bool Insert(Comment entity)
        {
            db.Comments.Add(entity);
            db.SaveChanges();
            return true;
        }
        public List<Comment> ListComment(long parentId, long productId)
        {
            return db.Comments.Where(x => x.ParentID == parentId && x.ProductID == productId).ToList();
        }
        public List<CommentViewModel> ListCommentViewModel(long parentId, long productId)
        {
            var model = (from a in db.Comments
                         join b in db.Users
                             on a.UserID equals b.ID
                         where a.ParentID == parentId && a.ProductID == productId

                         select new
                         {
                             ID = a.ID,
                             CommentMsg = a.CommentMsg,
                             CommentDate = a.CommentDate,
                             ProductID = a.ProductID,
                             UserID = a.UserID,
                             FullName = b.Name,
                             ParentID = a.ParentID,
                             Rate = a.Rate
                         }).AsEnumerable().Select(x => new CommentViewModel()
                         {
                             ID = x.ID,
                             CommentMsg = x.CommentMsg,
                             CommentDate = x.CommentDate,
                             ProductID = x.ProductID,
                             UserID = x.UserID,
                             FullName = x.FullName,
                             ParentID = x.ParentID,
                             Rate = x.Rate
                         });
            return model.OrderByDescending(y => y.ID).ToList();
        }
    }
}