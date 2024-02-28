using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    [MetadataTypeAttribute(typeof(NHAXUATBANMetadata))]
    public partial class NHAXUATBAN
    {
        internal sealed class NHAXUATBANMetadata
        {
            public int MANHAXUATBAN { get; set; }
            [Display(Name = "Tên nhà xuất bản")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TENNHAXUATBAN { get; set; }
            [Display(Name = "Địa chỉ")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DIACHI { get; set; }
            [Display(Name = "Điện thoại")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string DIENTHOAI { get; set; }
            [Display(Name = "Email")]
            [Required(ErrorMessage = "{0} không được để trống")]
            [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Email không hợp lệ")]
            public string EMAIL { get; set; }
            [Display(Name = "Đã xóa")]
            public Nullable<bool> DAXOA { get; set; }
        }
    }
}