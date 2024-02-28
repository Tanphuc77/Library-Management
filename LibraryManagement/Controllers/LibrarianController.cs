using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class LibrarianController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        public ActionResult ListLibrarian()
        {
            var Librarian = db.THUTHUs.ToList();
            return View(Librarian);
        }
        [HttpGet]
        public ActionResult CreateLibrarian()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLibrarian(THUTHU model)
        {
            if (ModelState.IsValid)
            {
                THUTHU Librarian = db.THUTHUs.SingleOrDefault(m => m.USERNAME == model.USERNAME);
                if (Librarian == null)
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.PASSWORD, salt);
                    model.PASSWORD = hashedPassword;
                    model.TINHTRANG = true;
                    db.THUTHUs.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("ListLibrarian");
                }
                else
                {
                    TempData["Notification"] = "Tên tài khoản giống với tài khoản khác";
                    return View();
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditLibrarian(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            THUTHU Librarian = db.THUTHUs.SingleOrDefault(m => m.MATHUTHU == id);
            if (Librarian == null)
            {
                return HttpNotFound();
            }
            return View(Librarian);
        }
        [HttpPost]
        public ActionResult EditLibrarian(THUTHU model)
        {
            if (ModelState.IsValid)
            {
                THUTHU Librarian = db.THUTHUs.SingleOrDefault(m => m.MATHUTHU == model.MATHUTHU);
                if (Librarian.PASSWORD == model.PASSWORD)
                {
                    Librarian.TENTHUTHU = model.TENTHUTHU;
                    Librarian.EMAIL = model.EMAIL;
                    Librarian.SODIENTHOAI = model.SODIENTHOAI;
                    Librarian.USERNAME = model.USERNAME;
                    Librarian.PASSWORD = model.PASSWORD;
                    Librarian.TINHTRANG = model.TINHTRANG;
                    db.SaveChanges();
                    return RedirectToAction("ListLibrarian");
                }
                else
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.PASSWORD, salt);
                    model.PASSWORD = hashedPassword;
                    Librarian.TENTHUTHU = model.TENTHUTHU;
                    Librarian.EMAIL = model.EMAIL;
                    Librarian.SODIENTHOAI = model.SODIENTHOAI;
                    Librarian.USERNAME = model.USERNAME;
                    Librarian.PASSWORD = model.PASSWORD;
                    Librarian.TINHTRANG = model.TINHTRANG;
                    db.SaveChanges();
                    return RedirectToAction("ListLibrarian");
                }
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Notification()
        {
            return View();
        }
        public ActionResult RemoveLibrarian(int? id)
        {
            try
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                THUTHU Librarian = db.THUTHUs.SingleOrDefault(m => m.MATHUTHU == id);
                if (Librarian == null)
                {
                    return HttpNotFound();
                }
                // Xác nhận xóa ở đây
                if (!ConfirmedToDelete())
                {
                    // Nếu không xác nhận, có thể chuyển hướng hoặc hiển thị thông báo khác
                    return RedirectToAction("ListLibrarian");
                }
                if (IsLoggedInLibrarian((int)id))
                {
                    // Nếu tài khoản đang đăng nhập là thủ thư đang xóa,
                    // bạn có thể chuyển hướng hoặc hiển thị thông báo khác
                    return RedirectToAction("Notification");
                }
                db.THUTHUs.Remove(Librarian);
                db.SaveChanges();
                return RedirectToAction("ListLibrarian");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorRemoveLibrarian", "Bookpublisher"); // Chuyển hướng đến trang lỗi chung
            }
        }
        public ActionResult ErrorRemoveLibrarian()
        {
            return View();
        }
        // Hàm kiểm tra xác nhận
        private bool ConfirmedToDelete()
        {
            //  true để cho phép xóa
            return true;
        }
        private bool IsLoggedInLibrarian(int librarianId)
        {
            THUTHU loggedInUser = Session["TaiKhoan"] as THUTHU;
            return loggedInUser != null && loggedInUser.MATHUTHU == librarianId;
        }
    }
}