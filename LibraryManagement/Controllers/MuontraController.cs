using LibraryManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Controllers
{
    public class MuontraController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: Borrowbooks
        public ActionResult ChuaDuyetSach()
        {
            var notApprovedBook = db.MUONTRAs.Where(m=>m.TRANGTHAIMUON == false).OrderByDescending(m=>m.NGAYMUON).ToList();
            return View(notApprovedBook);
        }
        [HttpGet]
        public ActionResult DuyetPhieuMuon(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            MUONTRA muonTra = db.MUONTRAs.SingleOrDefault(m => m.MAMUON == id);
            if (muonTra == null)
            {
                return HttpNotFound();
            }

            // Hiển thị chi tiết phiếu lên view
            var listChiTietMuonSach = db.CHITIETMUONTRAs.Where(m => m.MAMUON == id);
            ViewBag.ChiTietMuonSach = listChiTietMuonSach;

            return View(muonTra);
        }
        [HttpPost]
        public ActionResult DuyetPhieuMuon(MUONTRA model)
        {
            // Truy vấn dữ liệu đơn hàng đó
            MUONTRA updatePhieuMuon = db.MUONTRAs.SingleOrDefault(m => m.MAMUON == model.MAMUON);
            updatePhieuMuon.TRANGTHAIMUON = model.TRANGTHAIMUON;
            updatePhieuMuon.HANTRA = model.HANTRA;

            var listChiTietMuonSach = db.CHITIETMUONTRAs.Where(m => m.MAMUON == model.MAMUON);
            ViewBag.ChiTietMuonSach = listChiTietMuonSach;
            // Duyệt qua từng chi tiết mượn sách để cập nhật số lượng tồn của sách
            foreach (var item in listChiTietMuonSach)
            {
                var book = db.SACHes.SingleOrDefault(s => s.MASACH == item.MASACH);
                if (book != null)
                {
                    // Trừ số lượng tồn của sách đi số lượng mượn
                    book.SOLUONGTON -= item.SOLUONGMUON;
                }
            }
            THUTHU librarian = db.THUTHUs.SingleOrDefault(m=>m.MATHUTHU == model.MATHUTHU);
            updatePhieuMuon.MATHUTHU = model.MATHUTHU;
            db.SaveChanges();
            return RedirectToAction("ChuaDuyetSach");
        }
        public ActionResult DaDuyetSach()
        {
            var Acceptance = db.MUONTRAs.Where(m => m.TRANGTHAIMUON == true && m.TRANGTHAITRA == false).OrderByDescending(m => m.NGAYMUON).ToList();
            return View(Acceptance);
        }
        [HttpGet]
        public ActionResult DuyetTraSach(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            MUONTRA muonTra = db.MUONTRAs.SingleOrDefault(m => m.MAMUON == id);
            if (muonTra == null)
            {
                return HttpNotFound();
            }

            // Hiển thị chi tiết phiếu lên view
            var listChiTietMuonSach = db.CHITIETMUONTRAs.Where(m => m.MAMUON == id);
            ViewBag.ChiTietMuonSach = listChiTietMuonSach;

            return View(muonTra);
        }
        [HttpPost]
        public ActionResult DuyetTraSach(MUONTRA model)
        {
            // Truy vấn dữ liệu đơn hàng đó
            MUONTRA updatePhieuMuon = db.MUONTRAs.SingleOrDefault(m => m.MAMUON == model.MAMUON);
            updatePhieuMuon.NGAYTRATHUCTE = model.NGAYTRATHUCTE;
            updatePhieuMuon.TRANGTHAITRA = model.TRANGTHAITRA;

            var listChiTietMuonSach = db.CHITIETMUONTRAs.Where(m => m.MAMUON == model.MAMUON);
            ViewBag.ChiTietMuonSach = listChiTietMuonSach;
            foreach (var item in listChiTietMuonSach)
            {
                var book = db.SACHes.SingleOrDefault(s => s.MASACH == item.MASACH);
                if (book != null)
                {
                    // Trừ số lượng tồn của sách đi số lượng mượn
                    book.SOLUONGTON += item.SOLUONGMUON;
                }
            }
            db.SaveChanges();
            return RedirectToAction("ChuaDuyetSach");
        }
        public ActionResult DaTraSach()
        {
            var Acceptance = db.MUONTRAs.Where(m => m.TRANGTHAITRA == true).OrderByDescending(m => m.NGAYMUON).ToList();
            return View(Acceptance);
        }
        public ActionResult ChiTiet(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            MUONTRA muonTra = db.MUONTRAs.SingleOrDefault(m => m.MAMUON == id);
            if (muonTra == null)
            {
                return HttpNotFound();
            }

            // Hiển thị chi tiết phiếu lên view
            var listChiTietMuonSach = db.CHITIETMUONTRAs.Where(m => m.MAMUON == id);
            ViewBag.ChiTietMuonSach = listChiTietMuonSach;

            return View(muonTra);
        }
    }
}