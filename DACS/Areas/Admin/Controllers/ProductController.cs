using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DACS.Common;
using DACS.DAO;
using DACS.Models;
using System.Web.Mvc;

namespace DACS.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(string searchString, int page = 1, int pageSize = 200)
        {
            var dao = new ProductDAO();
            var model = dao.ListAllPaging(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);
            return RedirectToAction("Index");
        }
    }
}