using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DACS.Common;
using DACS.DAO;
using DACS.Models;
using DACS.ViewModel;

namespace DACS.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.MD5Hash(model.Password), true);
                if (result == 1)
                {
                    var user = dao.GetByUserName(model.UserName);
                    var usersession = new UserLogin();
                    usersession.UserName = user.UserName;
                    usersession.UserID = user.ID;
                    Session.Add(CommonConstants.USER_SESSION, usersession);
                    return RedirectToAction("Index", "Home");
                }
                else if (result == 0)
                {
                    ModelState.AddModelError("", "Tài khoản không tồn tai");
                }
                else if (result == -1)
                {
                    ModelState.AddModelError("", "Tài khoản đang bị khóa");
                }
                else if (result == -2)
                {
                    ModelState.AddModelError("", "Mật khẩu không đúng");
                }
                else if (result == -3)
                {
                    ModelState.AddModelError("", "Tài khoản không có quyền đăng nhập");
                }
            }
            return View("model");
        }
        public ActionResult Logout()
        {
            Session[CommonConstants.USER_SESSION] = null;
            return RedirectToAction("Login", "User");
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                // Kiểm tra và xử lý lỗi như trước (nếu cần)
            }

            var dao = new UserDAO();
            if (dao.CheckUserName(model.Register.UserName))
            {
                ModelState.AddModelError("", "Tên tài khoản đã tồn tại.");
            }
            else if (dao.CheckEmail(model.Register.Email))
            {
                ModelState.AddModelError("", "Email đã tồn tại.");
            }
            else
            {
                // Tiến hành tạo mới User từ dữ liệu trong RegisterViewModel
                var user = model.Register;
                user.Password = Encryptor.MD5Hash(user.Password); // Bạn có thể mã hóa mật khẩu ở đây
                user.CreateDate = DateTime.Now;
                user.Status = true;

                var result = dao.Insert(user);
                if (result > 0)
                {
                    ViewBag.Success = "Đăng ký thành công.";
                    model = new RegisterViewModel(); // Xóa form
                }
                else
                {
                    ModelState.AddModelError("", "Đăng ký không thành công.");
                }
            }

            return View(model);
        }
    }
}