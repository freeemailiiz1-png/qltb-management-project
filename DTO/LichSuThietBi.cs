using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class LichSuThietBi
    {
        public int ID { get; set; }
        public int? ThietBiID { get; set; }
        public int? HanhDongID { get; set; }
        public int? CanBoCuID { get; set; }
        public int? CanBoMoiID { get; set; }
        public int? DonViCuID { get; set; }
        public int? DonViMoiID { get; set; }
        public DateTime? ThoiDiem { get; set; }
        public string GhiChu { get; set; }

        // Thuộc tính bổ sung cho hiển thị
        public string SerialNumber { get; set; }
        public string TenHanhDong { get; set; }
        public string TenCanBoCu { get; set; }
        public string TenCanBoMoi { get; set; }
        public string TenDonViCu { get; set; }
        public string TenDonViMoi { get; set; }
    }
}
