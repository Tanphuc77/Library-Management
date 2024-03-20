using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
namespace LibraryManagement.Controllers
{
    public class BookDetailController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: BookDetail
        public ActionResult BookDetail(int maTheLoai, int maNhaXuatBan, int? maSach, string tensach)
        {
            if (maSach == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var sanpham = db.SACHes.SingleOrDefault(s => s.MATHELOAI == maTheLoai && s.MANHAXUATBAN == maNhaXuatBan && s.MASACH == maSach);
            if (sanpham == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(sanpham);
        }
        public ActionResult BookRelate(int maTheLoai, int maNhaXuatBan, int? maSach)
        {
            // Lấy sách đang xem
            var bookWatched = db.SACHes.SingleOrDefault(s => s.MATHELOAI == maTheLoai && s.MANHAXUATBAN == maNhaXuatBan && s.MASACH == maSach);
            if (bookWatched == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            // Lấy sản phẩm liên quan
            var bookRelate = db.SACHes
            .Where(s => s.MATHELOAI == bookWatched.MATHELOAI && s.MASACH != bookWatched.MASACH && s.DAXOA == false)
            .OrderByDescending(s => s.NGAYCAPNHAT).ToList();

            return View(bookRelate);
        }
    }

}