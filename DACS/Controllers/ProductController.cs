using DACS.Common;
using DACS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACS.Controllers
{
    public class ProductController : Controller
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
    }
}