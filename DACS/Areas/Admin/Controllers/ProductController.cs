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
            SetViewBag();
            return View(model);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDAO().Delete(id);
            return RedirectToAction("Index");
        }
        public void SetViewBag(long? selectId = null)
        {
            var dao = new ProductCategoryDAO();
            ViewBag.CategoryID = dao.ListAll();
        }

        [HttpPost]
        public JsonResult AddProductAjax(string name, string code, string metatilte, string description, string image, string categoryid, string detail , string listtype, string listfile)
        {
            try
            {
                var dao = new ProductDAO();
                Product product = new Product();

                product.Name = name;
                product.CreateDate= DateTime.Now;
                product.Code = code;
                product.MetaTitle = metatilte;
                product.Description = description;
                product.Image = image;
                product.CategoryID = Convert.ToInt16(categoryid);
                product.Status = true;
                product.Detail = detail;
                product.ListType = listtype;
                product.ListFile = listfile;
                
                long id = dao.Insert(product);
                if (id > 0)
                {
                    return Json(new { status = true });
                }
                else
                {
                    return Json(new { status = false });
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

    }
}