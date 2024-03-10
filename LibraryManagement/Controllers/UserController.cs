using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
namespace LibraryManagement.Controllers
{
    public class UserController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: User
        public ActionResult ListUser()
        {
            var user = db.DOCGIAs.ToList();
            return View(user);
        }
        [HttpGet]
        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            DOCGIA User = db.DOCGIAs.SingleOrDefault(m => m.ID == id);
            if (User == null)
            {
                return HttpNotFound();
            }
            ViewBag.MALOP = new SelectList(db.LOPs.OrderBy(m => m.TENLOP), "MALOP", "TENLOP", User.MALOP);
            return View(User);
        }
        [HttpPost]
        public ActionResult EditUser(DOCGIA model)
        {
            if (ModelState.IsValid)
            {
                DOCGIA User = db.DOCGIAs.SingleOrDefault(m => m.ID == model.ID);
                if (User.PASSWORD == model.PASSWORD)
                {
                    User.MASINHVIEN = model.MASINHVIEN;
                    User.HOTEN = model.HOTEN;
                    User.GIOITINH = model.GIOITINH;
                    User.MALOP = model.MALOP;
                    User.DIENTHOAI = model.DIENTHOAI;
                    User.EMAIL = model.EMAIL;
                    User.TINHTRANG = model.TINHTRANG;
                    User.USERNAME = model.USERNAME;
                    User.PASSWORD = model.PASSWORD;
                    db.SaveChanges();
                    return RedirectToAction("ListUser");
                }
                else
                {
                    string salt = BCrypt.Net.BCrypt.GenerateSalt();
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.PASSWORD, salt);
                    model.PASSWORD = hashedPassword;
                    User.MASINHVIEN = model.MASINHVIEN;
                    User.HOTEN = model.HOTEN;
                    User.GIOITINH = model.GIOITINH;
                    User.MALOP = model.MALOP;
                    User.DIENTHOAI = model.DIENTHOAI;
                    User.EMAIL = model.EMAIL;
                    User.TINHTRANG = model.TINHTRANG;
                    User.USERNAME = model.USERNAME;
                    User.PASSWORD = model.PASSWORD;
                    db.SaveChanges();
                    return RedirectToAction("ListUser");
                }
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult RemoveUser(int? id)
        {
            try
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                DOCGIA User = db.DOCGIAs.SingleOrDefault(m => m.ID == id);
                if (User == null)
                {
                    return HttpNotFound();
                }
                // Xác nhận xóa ở đây
                if (!ConfirmedToDelete())
                {
                    // Nếu không xác nhận, có thể chuyển hướng hoặc hiển thị thông báo khác
                    return RedirectToAction("ListUser");
                }
                db.DOCGIAs.Remove(User);
                db.SaveChanges();
                return RedirectToAction("ListUser");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorRemoveUser", "User"); // Chuyển hướng đến trang lỗi chung
            }
        }
        public ActionResult ErrorRemoveUser()
        {
            return View();
        }
        // Hàm kiểm tra xác nhận
        private bool ConfirmedToDelete()
        {
            //  true để cho phép xóa
            return true;
        }
    }
}