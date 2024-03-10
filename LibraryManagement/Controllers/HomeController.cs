using CaptchaMvc.HtmlHelpers;
using LibraryManagement.Models;
using System.Linq;
using System.Web.Mvc;
namespace LibraryManagement.Controllers
{
    public class HomeController : Controller
    {
        QuanLyThuVienEntities db = new QuanLyThuVienEntities();
        // GET: Home
        public ActionResult ListBookLibrary()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SingUp()
        {
            ViewBag.MALOP = new SelectList(db.LOPs.OrderBy(m => m.TENLOP), "MALOP", "TENLOP");
            return View();
        }
        [HttpPost]
        public ActionResult SingUp(DOCGIA model, FormCollection f)
        {
            ViewBag.MALOP = new SelectList(db.LOPs.OrderBy(m => m.TENLOP), "MALOP", "TENLOP");
            // Kiem tra capcha 
            if (this.IsCaptchaValid("Capcha is not valid"))
            {

                if (ModelState.IsValid)//Kiểm tra xem liệu dữ liệu được submit từ trang web có hợp lệ hay không. 
                {
                    DOCGIA User = db.DOCGIAs.SingleOrDefault(m => m.USERNAME == model.USERNAME);
                    if (User == null)
                    {
                        string salt = BCrypt.Net.BCrypt.GenerateSalt();
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.PASSWORD, salt);
                        string hashedConfirmPassword = BCrypt.Net.BCrypt.HashPassword(model.ConfirmPassword, salt); // Hash mật khẩu xác nhận
                        model.PASSWORD = hashedPassword;
                        model.ConfirmPassword = hashedConfirmPassword;
                        model.TINHTRANG = true;
                        if (model.PASSWORD != model.ConfirmPassword)
                        {
                            ViewBag.NoMatch = "Mật khẩu không trùng khớp";
                            return View();
                        }
                        db.DOCGIAs.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    ViewBag.TrungLap = "Tên tài khoản giống với tài khoản khác";
                    return View();
                }
                else
                {
                    ViewBag.ThongBao = "Đăng ký không thành công";
                }
                return View();
            }
            ViewBag.Captcha = "Sai mã Captcha";
            return View();
        }
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

            DOCGIA User = db.DOCGIAs.SingleOrDefault(m => m.USERNAME == Username);
            if (User.TINHTRANG == true)
            {
                if (User != null)
                {
                    bool isPasswordCorrect = BCrypt.Net.BCrypt.Verify(Password, User.PASSWORD);
                    if (isPasswordCorrect)
                    {
                        Session["TaiKhoan"] = User;
                        return RedirectToAction("ListBookLibrary", "Home");
                    }
                }
                TempData["Notification"] = "Tài khoản hoặc mật khẩu không đúng";
                return View();
            }
            TempData["Blockaccess"] = "Tài khoản của bạn đã bị chặn!";
            return View();
        }
        public ActionResult Logout()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Login");
        }
        public ActionResult Menu()
        {
            var listCategory = db.THELOAIs.Where(m=>m.DAXOA== false);
            return View(listCategory);
        }
        public ActionResult Navbar()
        {
            var listPublisher = db.NHAXUATBANs.Where(m=>m.DAXOA == false);
            return View(listPublisher);
        }
    }
}