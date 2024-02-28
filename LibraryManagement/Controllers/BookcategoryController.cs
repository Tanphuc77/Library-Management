using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookcategoryController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();

        public ActionResult ListBookcategory()
        {
            var listBookcategory = db.THELOAIs.Where(m => m.DAXOA == false).ToList();
            return View(listBookcategory);
        }
        [HttpGet]
        public ActionResult CreateBookcategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBookcategory(THELOAI model)
        {
            model.DAXOA = false;
            db.THELOAIs.Add(model);
            db.SaveChanges();
            return RedirectToAction("ListBookcategory");
        }
        [HttpGet]
        public ActionResult EditBookcategory(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            THELOAI Bookcategory = db.THELOAIs.SingleOrDefault(m => m.MATHELOAI == id);
            if (Bookcategory == null)
            {
                return HttpNotFound();
            }
            return View(Bookcategory);
        }
        [HttpPost]
        public ActionResult EditBookcategory(THELOAI model)
        {
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("ListBookcategory");
        }
        public ActionResult RemoveBookcategory(int? id)
        {
            try
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                THELOAI Bookcategory = db.THELOAIs.SingleOrDefault(m => m.MATHELOAI == id);
                if (Bookcategory == null)
                {
                    return HttpNotFound();
                }
                // Xác nhận xóa ở đây
                if (!ConfirmedToDelete())
                {
                    // Nếu không xác nhận, có thể chuyển hướng hoặc hiển thị thông báo khác
                    return RedirectToAction("ListBookcategory");
                }
                db.THELOAIs.Remove(Bookcategory);
                db.SaveChanges();
                return RedirectToAction("ListBookcategory");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorRemoveBookcategory", "Bookcategory"); // Chuyển hướng đến trang lỗi chung
            }
        }
        private bool ConfirmedToDelete()
        {
            //  true để cho phép xóa
            return true;
        }
        public ActionResult ErrorRemoveBookcategory()
        {
            return View();
        }
    }
}