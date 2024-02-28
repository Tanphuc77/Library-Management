using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    [MetadataTypeAttribute(typeof(THUTHUMetadata))]
    public partial class THUTHU
    {
        internal sealed class THUTHUMetadata
        {
            public int MATHUTHU { get; set; }
            [Display(Name = "Tên thủ thư")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TENTHUTHU { get; set; }
            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không hợp lệ")]
            public string EMAIL { get; set; }
            [Display(Name = "Số điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [StringLength(10, ErrorMessage = "Số điện thoại không quá 10 ký tự")]
            public string SODIENTHOAI { get; set; }
            [Display(Name = "Tài khoản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string USERNAME { get; set; }
            [Display(Name = "Mật khẩu")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string PASSWORD { get; set; }
            [Display(Name = "Tình trạng")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public Nullable<bool> TINHTRANG { get; set; }
        }
    }
}