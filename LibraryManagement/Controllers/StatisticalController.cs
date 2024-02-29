using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class StatisticalController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities(); 
        // GET: Statistical
        public ActionResult Statistical()
        {
            ViewBag.ThongKeSachMuonChuaduyet = ThongKeSachMuonChuaduyet();
            ViewBag.ThongKeSachMuonDaduyet = ThongKeSachMuonDaduyet();
            ViewBag.ThongKeSachTra = ThongKeSachTra();
            ViewBag.ThongKeDocGia = ThongKeDocGia();
            return View();
        }
        public double ThongKeSachMuonChuaduyet()
        {
            int donMuonChuaDuyet = db.MUONTRAs.Count(m => m.TRANGTHAIMUON == false);
            return donMuonChuaDuyet;
        }
        public double ThongKeSachMuonDaduyet()
        {
            int donMuonDaDuyet = db.MUONTRAs.Count(m => m.TRANGTHAIMUON == true);
            return donMuonDaDuyet;
        }

        public double ThongKeSachTra()
        {
            int donTra = db.MUONTRAs.Count(m => m.TRANGTHAITRA == true);
            return donTra;
        }
        public double ThongKeDocGia()
        {
            int thanhVien = db.DOCGIAs.Count();
            return thanhVien;
        }
    }
}