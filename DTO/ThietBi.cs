using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class ThietBi
    {
        public int ID { get; set; }
        public int? LoaiID { get; set; }
        public string SerialNumber { get; set; }
        public int? CanBoID { get; set; }
        public int? DonViID { get; set; }
        public DateTime? NgayCap { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public DateTime? NgayHetHanVongDoi { get; set; }
        public int? TrangThai { get; set; }
        public string GhiChu { get; set; }

        // Thuộc tính bổ sung cho hiển thị
        public string TenLoai { get; set; }
        public string TenCanBo { get; set; }
        public string TenDonVi { get; set; }
        public string TenTrangThai { get; set; }
    }
}
