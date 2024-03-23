using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibraryManagement.Models
{
    public class Cart
    {
        public int MASACH { get; set; }
        public string  TENSACH { get; set;}
        public int SOLUONG { get; set; }
        public string HINHANH { get; set; }
        public int MATHELOAI { get; set; }
        public int MANHAXUATBAN { get; set; }

        public Cart()
        {

        }

        public Cart(int maSach)
        {
            using (QuanLyThuVienEntities db = new QuanLyThuVienEntities())
            {
                this.MASACH = maSach;
                SACH book = db.SACHes.Single(m=>m.MASACH == maSach);
                this.TENSACH = book.TENSACH;
                this.HINHANH = book.HINHANH;
                this.MATHELOAI = (int)book.MATHELOAI;
                this.MANHAXUATBAN = (int)book.MANHAXUATBAN;
            }
        }
        public Cart(int maSach, int soLuong)
        {
            using (QuanLyThuVienEntities db = new QuanLyThuVienEntities())
            {
                this.MASACH = maSach;
                SACH book = db.SACHes.Single(m => m.MASACH == maSach);
                this.TENSACH = book.TENSACH;
                this.SOLUONG = soLuong;
                this.SOLUONG = 1;
                this.HINHANH = book.HINHANH;
                this.MATHELOAI = (int)book.MATHELOAI;
                this.MANHAXUATBAN = (int)book.MANHAXUATBAN;
            }
        }
    }
}