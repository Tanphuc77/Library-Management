using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibraryManagement.Models
{ 
    [MetadataTypeAttribute(typeof(SACHMetadata))]
    public partial class SACH
    {
        internal sealed class SACHMetadata
        {
            public int MASACH { get; set; }
            [Display(Name = "Tên sách")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TENSACH { get; set; }
            [Display(Name = "Tác giả")]
            [Required(ErrorMessage = "{0} không được để trống")]
            public string TACGIA { get; set; }
            [Display(Name = "Số lượng tồn")]
            [Required(ErrorMessage = "Số lượng tồn không được để trống")]
            [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải là số không âm")]
            public Nullable<int> SOLUONGTON { get; set; }
            [Display(Name = "Mô tả")]
            [AllowHtml]
            public string MOTA { get; set; }
            [Display(Name = "Ngày cập nhật")]
            //[DataType(DataType.Date)]
            //[Required(ErrorMessage = "Ngày cập nhật là bắt buộc")]
            public Nullable<System.DateTime> NGAYCAPNHAT { get; set; }
            [Display(Name = "Số lượng mượn")]
            public Nullable<int> SOLUONGMUON { get; set; }
            [Display(Name = "Lượt xem")]
            public Nullable<int> LUOTXEM { get; set; }
            [Display(Name = "Đã xóa")]
            public Nullable<bool> DAXOA { get; set; }
            [Display(Name = "Hình ảnh")]
            public string HINHANH { get; set; }
            public Nullable<int> MANHAXUATBAN { get; set; }
            public Nullable<int> MATHELOAI { get; set; }
        }
    }
}