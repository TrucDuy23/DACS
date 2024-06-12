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
        [HttpPost]
        public JsonResult AddResult(long examid, long userid)
        {
            try
            {
                var dao = new ResultDAO();
                Result result = new Result();

                result.ExamID = examid;
                result.UserID = userid;
                result.Status = false;
                result.ResultQuiz = "";
                result.ResultEssay = "";
                result.StartDateQuiz = DateTime.Now.ToShortDateString();
                result.StartTimeQuiz = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute;
                result.Score = "0";
                bool addresult = dao.Insert(result);
                if (addresult == true)
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

        [HttpPost]
        public JsonResult UpdateResult(long examid, long userid, string resultessay, string resultquiz)
        {
            try
            {
                var dao = new ResultDAO();
                Result result = new Result();

                result.ExamID = examid;
                result.UserID = userid;
                result.Status = true;
                result.ResultQuiz = resultquiz;
                result.ResultEssay = resultessay;
                result.FinishTimeEssay = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute;
                result.FinishTimeQuiz = DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute;

                bool addresult = dao.Update(result);
                if (addresult == true)
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
    }
}