using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class BookpublisherController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();

        public ActionResult ListBookPublisher()
        {
            var listBookPublisher = db.NHAXUATBANs.Where(m=>m.DAXOA == false).ToList();
            return View(listBookPublisher);
        }
        [HttpGet]
        public ActionResult CreateBookPublisher()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateBookPublisher(NHAXUATBAN model)
        {
            if (ModelState.IsValid)
            {
                model.DAXOA = false;
                db.NHAXUATBANs.Add(model);
                db.SaveChanges();
                return RedirectToAction("ListBookPublisher");
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult EditBookPublisher(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
            }
            NHAXUATBAN BookPublisher = db.NHAXUATBANs.SingleOrDefault(m => m.MANHAXUATBAN == id);
            if (BookPublisher == null)
            {
                return HttpNotFound();
            }
            return View(BookPublisher);
        }
        [HttpPost]
        public ActionResult EditBookPublisher(NHAXUATBAN model)
        {
            if(ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ListBookPublisher");
            }
            return View(model) ;
        }
        public ActionResult RemoveBookPublisher(int? id)
        {
            try
            {
                if (id == null)
                {
                    Response.StatusCode = 404;
                    return null;
                }
                NHAXUATBAN BookPublisher = db.NHAXUATBANs.SingleOrDefault(m => m.MANHAXUATBAN == id);
                if (BookPublisher == null)
                {
                    return HttpNotFound();
                }
                // Xác nhận xóa ở đây
                if (!ConfirmedToDelete())
                {
                    // Nếu không xác nhận, có thể chuyển hướng hoặc hiển thị thông báo khác
                    return RedirectToAction("ListBookPublisher");
                }
                db.NHAXUATBANs.Remove(BookPublisher);
                db.SaveChanges();
                return RedirectToAction("ListLibrarian");
            }
            catch (Exception ex)
            {
                return RedirectToAction("ErrorRemoveBookPublisher", "Bookpublisher"); // Chuyển hướng đến trang lỗi chung
            }
        }
        private bool ConfirmedToDelete()
        {
            //  true để cho phép xóa
            return true;
        }
        public ActionResult ErrorRemoveBookPublisher()
        {
            return View();
        }
    }
}
