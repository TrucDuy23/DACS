using DACS.DAO;
using DACS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACS.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var dao = new ProductCategoryDAO();
            ViewBag.CategoryID = dao.ListAll();
            var productdao = new ProductDAO();
            ViewBag.HomeProducts = productdao.ListAllProduct();

            var examDAO = new ExamDAO();
            ViewBag.HomeExams = examDAO.ListAllExam();
            return View();
        }
    }
}