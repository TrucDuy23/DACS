    using DACS.Common;
using DACS.DAO;
using DACS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACS.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Category(string searchString, long cateId)
        {
            var category = new ProductCategoryDAO().ViewDetail(cateId);
            ViewBag.Category = category;
            ViewBag.CategoryID = new ProductCategoryDAO().ListAll();
            var model = new ProductDAO().ListByCategoryID(searchString, cateId);

            return View(model);
        }
        public ActionResult Detail(long id, long detailid)
        {
            var product = new ProductDAO().ViewDetail(id);
            ViewBag.CategoryID = new ProductCategoryDAO().ListAll();

            var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];
            ViewBag.UserID = sessionUser.UserID;
            ViewBag.ListComment = new CommentDAO().ListCommentViewModel(0, id);

            ViewBag.DetailID = detailid.ToString();
            return View(product);
        }
        [ChildActionOnly]
        public ActionResult _ChildComment(long parentid, long productid)
        {
            var data = new CommentDAO().ListCommentViewModel(parentid, productid);
            var sessionuser = (UserLogin)Session[CommonConstants.USER_SESSION];
            for (int k = 0; k < data.Count; k++)
            {
                data[k].UserID = sessionuser.UserID;
            }
            return PartialView("~/Views/Shared/_ChildComment.cshtml", data);
        }
        [HttpPost]
        public JsonResult AddNewComment(long productid, long userid, long parentid, string commentmsg, string rate)
        {
            try
            {
                var dao = new CommentDAO();
                Comment comment = new Comment();

                comment.CommentMsg = commentmsg;
                comment.ProductID = productid;
                comment.UserID = userid;
                comment.ParentID = parentid;
                comment.Rate = Convert.ToInt16(rate);
                comment.CommentDate = DateTime.Now;

                bool addcomment = dao.Insert(comment);
                if (addcomment == true)
                {
                    return Json(new
                    {
                        status = true
                    });
                }
                else
                {
                    return Json(new
                    {
                        status = false
                    });
                }
            }
            catch
            {
                return Json(new
                {
                    status = false
                });
            }
        }
        public ActionResult GetComment(long productid)
        {
            var data = new CommentDAO().ListCommentViewModel(0, productid);
            return PartialView("~/Views/Shared/_ChildComment.cshtml", data);
        }
    }
}