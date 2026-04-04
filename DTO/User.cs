using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class User
    {
        public int ID { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public int? DonViID { get; set; }
        public int? CapDonViID { get; set; }
        public int? TrangThai { get; set; }

        // Thuộc tính bổ sung cho hiển thị
        public string TenDonVi { get; set; }
        public string TenCapDonVi { get; set; }
        public string TenTrangThai { get; set; }
    }
}
