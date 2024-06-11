using DACS.Common;
using DACS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DACS.Controllers
{
    public class ExamController : BaseController
    {
        // GET: Exam
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Category(string searchString, string Type)
        {
            ViewBag.Category = Type;
            ViewBag.CategoryID = new ProductCategoryDAO().ListAll();
            var model = new ExamDAO().ListByType(searchString, Type);

            return View(model);
        }

        public ActionResult Detail(long id)
        {
            try
            {
                var dao = new ExamDAO().ViewDetail(Convert.ToInt16(id));
                ViewBag.ExamQuestion = new QuestionDAO().ListExamQuestion(dao.QuestionList);
                var session = (UserLogin)Session[CommonConstants.USER_SESSION];
                ViewBag.Result = new ResultDAO().GetByUserExamID(session.UserID, dao.ID);

                ViewBag.Msnv = session.UserName;
                ViewBag.UserID = session.UserID;

                if (!dao.UserList.Contains("*" + session.UserID.ToString() + "*"))
                {
                    return Redirect("/trang-chu");
                }
                return View(dao);
            }
            catch
            {
                return View();
            }
        }
    }
}