using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class LoginController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string Username = f["USERNAME"].ToString();
            string Password = f["PASSWORD"].ToString();

            THUTHU thuThu = db.THUTHUs.SingleOrDefault(m => m.USERNAME == Username);
            if (thuThu != null)
            {
                bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(Password, thuThu.PASSWORD);
                if (isPasswordCorrect)
                {
                    Session["TaiKhoan"] = thuThu;
                    return RedirectToAction("ListBook", "Book");
                }
            }
            TempData["ThongBao"] = "Tài khoản hoặc mật khẩu không đúng";
            return View();
        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Login");
        }
    }
}
