using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LibraryManagement.Models;
namespace LibraryManagement.Controllers
{
    public class CartController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: Cart
        public List<Cart> CartTake()
        {
            // Giỏ hàng đã tồn tại 
            List<Cart> lstCart = Session["Cart"] as List<Cart>;
            if (lstCart == null)
            {
                // nếu giỏ session giỏ hàng chưa tồn tại thì khởi tạo giỏ hàng 
                lstCart = new List<Cart>();
                Session["Cart"] = lstCart;
            }
            return lstCart;
        }
        public ActionResult CreateCart(int idBook, string strULR)
        {
            //Kiểm tra Sách có tồn tại trong cơ sở dữ liệu hay không
            SACH book = db.SACHes.SingleOrDefault(m => m.MASACH == idBook);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy Giỏ Hàng
            List<Cart> listCart = CartTake();
            //Sách đã tồn tại ở giỏ hàng
            Cart bookCheck = listCart.SingleOrDefault(m => m.MASACH == idBook);
            if (bookCheck != null)
            {
                //kiểm tra số lượng tồn trước khi mượn sách
                if (book.SOLUONGTON < bookCheck.SOLUONG)
                {
                    return View("Notification");
                }
                bookCheck.SOLUONG++;
                return Redirect(strULR);
            }
            Cart Cart = new Cart(idBook);
            if (book.SOLUONGTON < Cart.SOLUONG)
            {
                return View("Notification");
            }
            listCart.Add(Cart);
            return Redirect(strULR);
        }
        public double SumQuantity()
        {
            List<Cart> listCart = Session["Cart"] as List<Cart>;
            if (listCart == null)
            {
                return 0;
            }
            return listCart.Sum(m => m.SOLUONG);
        }
        public ActionResult CreateCartAjax(int idBook, string strULR)
        {
            // Kiểm tra sách có tồn tại trong cơ sở dữ liệu hay không 
            SACH book = db.SACHes.SingleOrDefault(m => m.MASACH == idBook);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // Lấy Giỏ Hàng 
            List<Cart> lstGioHang = CartTake();
            // Sách đã tồn tại ở giỏ hàng 
            Cart bookCheck = lstGioHang.SingleOrDefault(m => m.MASACH == idBook);
            if (bookCheck != null)
            {
                // kiểm tra số lượng tồn trước khi khách hàng mua hàng 
                if (book.SOLUONGTON < bookCheck.SOLUONG)
                {
                    return View("ThongBao");
                }
                bookCheck.SOLUONG++;
                ViewBag.SumQuantity = SumQuantity();
                return PartialView("CartPartial");
            }
            Cart itemGioHang = new Cart(idBook);
            if (book.SOLUONGTON < itemGioHang.SOLUONG)
            {
                return View("Notification");
            }
            lstGioHang.Add(itemGioHang);
            ViewBag.SumQuantity = SumQuantity();
            return PartialView("CartPartial");
        }
        public ActionResult CartPartial()
        {
            if (SumQuantity() == 0)
            {
                ViewBag.SumQuantity = 0;
                return PartialView();
            }
            ViewBag.SumQuantity = SumQuantity();    
            return View();
        }

        public ActionResult SeeCart()
        {
            // Lấy giỏ hàng 
            List<Cart> listCart = CartTake();
            ViewBag.SumQuantity = SumQuantity();
            return View(listCart);
        }

        public ActionResult RemoveCart(int idBook)
        {
            // kiểm tra session giỏ hàng tồn tại hay chưa 
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            // Kiểm tra Sách tồn tại trong csdl hay chưa 
            SACH book = db.SACHes.SingleOrDefault(m => m.MASACH == idBook);
            if (book == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lấy list giỏ hàng từ session 
            List<Cart> listCart = CartTake();
            // Kiểm tra Sách đó có tôn tại trong giỏ hàng hay không 
            Cart spCheck = listCart.SingleOrDefault(m => m.MASACH == idBook);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            listCart.Remove(spCheck);
            return RedirectToAction("SeeCart", "Cart");
        }

        [HttpPost]
        public ActionResult BorrowBooks()
        {
            // Kiểm tra session đã tồn tại hay chưa 
            if (Session["Cart"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            DOCGIA User = new DOCGIA();
            // Thêm đơn mượn
            MUONTRA borrowBooks = new MUONTRA();
            borrowBooks.MADOCGIA = User.ID;
            borrowBooks.NGAYMUON = DateTime.Now;
            borrowBooks.TRANGTHAIMUON = false;
            borrowBooks.TRANGTHAITRA = false;
            borrowBooks.DAXOA = false;
            db.MUONTRAs.Add(borrowBooks);
            db.SaveChanges();
            // Thêm chi tiết đơn đơn mượn 
            List<Cart> lstborrowBooks = CartTake();
            // Sau khi save thì mã trong đơn đơn mượn sẽ được lưu vào chi tiết đơn mượn
            foreach (var item in lstborrowBooks)
            {
                CHITIETMUONTRA detailborrowBooks = new CHITIETMUONTRA();
                detailborrowBooks.MAMUON = borrowBooks.MAMUON;
                detailborrowBooks.MASACH = item.MASACH;
                detailborrowBooks.TENSACH = item.TENSACH;
                detailborrowBooks.SOLUONGMUON = item.SOLUONG;
                db.CHITIETMUONTRAs.Add(detailborrowBooks);
            }
            db.SaveChanges();
            Session["Cart"] = null;
            return RedirectToAction("SeeCart", "Cart");
        }
    }
}