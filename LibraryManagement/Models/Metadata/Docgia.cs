using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    [MetadataTypeAttribute(typeof(DOCGIAMetadata))]
    public partial class DOCGIA
    {
        internal sealed class DOCGIAMetadata 
        {
            public int ID { get; set; }

            [Display(Name = "Mã sinh viên")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string MASINHVIEN { get; set; }
            [Display(Name = "Tên sinh viên")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string HOTEN { get; set; }
            [Display(Name = "Giới tính")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string GIOITINH { get; set; }
            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không hợp lệ")]
            public string EMAIL { get; set; }
            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(10, ErrorMessage = "Số điện thoại không quá 10 ký tự")]
            public string DIENTHOAI { get; set; }
            [Display(Name = "Tình trạng")]
            public Nullable<bool> TINHTRANG { get; set; }
            [Display(Name = "Tên Lớp")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<int> MALOP { get; set; }
            [Display(Name = "Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string USERNAME { get; set; }
            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string PASSWORD { get; set; }
            [Display(Name = "ConfirmPassword")]
            [Compare("PASSWORD", ErrorMessage = "Xác nhận mật khẩu không khớp")]
            public string ConfirmPassword { get; set; }
        }
    }
}