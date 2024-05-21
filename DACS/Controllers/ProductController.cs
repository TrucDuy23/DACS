using DACS.Common;
using DACS.DAO;
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

            //var sessionUser = (UserLogin)Session[CommonConstants.USER_SESSION];
            //ViewBag.UserID = sessionUser.UserID;
            //ViewBag.ListComment = new CommentDAO().ListCommentViewModel(0, id);

            ViewBag.DetailID = detailid.ToString();
            return View(product);
        }
    }
}