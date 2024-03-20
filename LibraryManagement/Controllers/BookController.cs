using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using LibraryManagement.Models;
using PagedList;
using PagedList.Mvc;


namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();

        public ActionResult ListBook(int? page)
        {
            var listBook = db.SACHes.OrderByDescending(m => m.MASACH).ToList();
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return View(listBook.ToPagedList(pageNumber, pageSize));
        }
        private bool ValidateBook(SACH model, HttpPostedFileBase HinhAnh)
        {
            if (HinhAnh != null && HinhAnh.ContentLength > 0)
            {
                var fileName = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/assest/imgs/Product/"), fileName);

                if (System.IO.File.Exists(path))
                {
                    TempData["ThongBao"] = "Hình ảnh đã tồn tại trong thư mục";
                    return false;
                }
                else
                {
                    HinhAnh.SaveAs(path);
                    model.HINHANH = fileName;
                }
            }
            return true;
        }
        [HttpGet]
        public ActionResult CreateBook()
        {
            ViewBag.MANHAXUATBAN = new SelectList(db.NHAXUATBANs.OrderBy(m => m.TENNHAXUATBAN), "MANHAXUATBAN", "TENNHAXUATBAN");
            ViewBag.MATHELOAI = new SelectList(db.THELOAIs.OrderBy(m => m.TENTHELOAI), "MATHELOAI", "TENTHELOAI");
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult CreateBook(SACH model, HttpPostedFileBase HinhAnh) // giao thức truyền dữ liệu hình ảnh 
        {
            // load DropDownList
            ViewBag.MANHAXUATBAN = new SelectList(db.NHAXUATBANs.OrderBy(m => m.TENNHAXUATBAN), "MANHAXUATBAN", "TENNHAXUATBAN");
            ViewBag.MATHELOAI = new SelectList(db.THELOAIs.OrderBy(m => m.TENTHELOAI), "MATHELOAI", "TENTHELOAI");
            if (!ValidateBook(model, HinhAnh))
            {
                return View();
            }
            bool hasError = false;

            if (model.TENSACH == null)
            {
                ViewBag.Name = "Tên sách không được để trống";
                hasError = true;
            }

            if (model.TACGIA == null)
            {
                ViewBag.Author = "Tác giả không được để trống";
                hasError = true;
            }

            if (model.SOLUONGTON == null || model.SOLUONGTON < 0)
            {
                ViewBag.Quantity = "Số lượng tồn không được để trống và phải lớn hơn 0";
                hasError = true;
            }

            if (hasError)
            {
                return View();
            }
            model.DAXOA = false;
            db.SACHes.Add(model);
            db.SaveChanges();
            return RedirectToAction("ListBook");
        }
        [HttpGet]
        public ActionResult EditBook(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Http404", "Error");
            }
            SACH book = db.SACHes.SingleOrDefault(m => m.MASACH == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.MANHAXUATBAN = new SelectList(db.NHAXUATBANs.OrderBy(m => m.TENNHAXUATBAN), "MANHAXUATBAN", "TENNHAXUATBAN", book.MANHAXUATBAN);
            ViewBag.MATHELOAI = new SelectList(db.THELOAIs.OrderBy(m => m.TENTHELOAI), "MATHELOAI", "TENTHELOAI", book.MATHELOAI);
            return View(book);
        }
        [HttpPost]
        public ActionResult EditBook(SACH model, HttpPostedFileBase HinhAnh) // giao thức truyền dữ liệu hình ảnh 
        {
            SACH data = db.SACHes.Find(model.MASACH);
            ViewBag.MANHAXUATBAN = new SelectList(db.NHAXUATBANs.OrderBy(m => m.TENNHAXUATBAN), "MANHAXUATBAN", "TENNHAXUATBAN", data.MANHAXUATBAN);
            ViewBag.MATHELOAI = new SelectList(db.THELOAIs.OrderBy(m => m.TENTHELOAI), "MATHELOAI", "TENTHELOAI", data.MATHELOAI);
            if (ModelState.IsValid)
            {
                data.TENSACH = model.TENSACH;
                data.TACGIA = model.TACGIA;
                data.SOLUONGTON = model.SOLUONGTON;
                data.MOTA = model.MOTA;
                data.NGAYCAPNHAT = model.NGAYCAPNHAT;
                data.SOLUONGMUON = model.SOLUONGMUON;
                data.LUOTXEM = model.LUOTXEM;
                data.DAXOA = model.DAXOA;
                data.MANHAXUATBAN = model.MANHAXUATBAN;
                data.MATHELOAI = model.MATHELOAI;
                if (HinhAnh != null && HinhAnh.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(HinhAnh.FileName);
                    var path = Path.Combine(Server.MapPath("~/assest/imgs/Product/"), fileName);
                    HinhAnh.SaveAs(path);
                    model.HINHANH = fileName;
                }
                else
                {
                    db.Entry(model).State = EntityState.Detached;// // Tách đối tượng hiện tại khỏi context
                    var book = db.SACHes.Find(model.MASACH);
                    model.HINHANH = book.HINHANH;
                    model = book; //Đặt biến model thành đối tượng hiện tại
                }
                db.SaveChanges();
                return RedirectToAction("ListBook");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult DetailBook(int? id)
        {
            SACH book = db.SACHes.SingleOrDefault(m => m.MASACH == id);
            ViewBag.MANHAXUATBAN = new SelectList(db.NHAXUATBANs.OrderBy(m => m.TENNHAXUATBAN), "MANHAXUATBAN", "TENNHAXUATBAN", book.MANHAXUATBAN);
            ViewBag.MATHELOAI = new SelectList(db.THELOAIs.OrderBy(m => m.TENTHELOAI), "MATHELOAI", "TENTHELOAI", book.MATHELOAI);
            return View(book);
        }
        public ActionResult DeleteBook(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SACH sp = db.SACHes.SingleOrDefault(m => m.MASACH == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            // Xác nhận xóa ở đây
            if (!ConfirmedToDelete())
            {
                // Nếu không xác nhận, có thể chuyển hướng hoặc hiển thị thông báo khác
                return RedirectToAction("ListBook");
            }

            db.SACHes.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("ListBook");
        }

        // Hàm kiểm tra xác nhận
        private bool ConfirmedToDelete()
        {
            //  true để cho phép xóa
            return true;
        }
    }
}