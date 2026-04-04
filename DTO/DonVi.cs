using System;

namespace QuanLyThietBi.DTO
{
    public class DonVi
    {
        public int ID { get; set; }
        public string MaDV { get; set; }
        public string TenDV { get; set; }
        public int? CapDV { get; set; }
        public int? DonViChaID { get; set; }
        public int? TrangThai { get; set; }
        public DateTime? NgayTao { get; set; }
        
        // Thuộc tính bổ sung cho hiển thị
        public string TenCapDV { get; set; }
        public string TenDonViCha { get; set; }
        public string TenTrangThai { get; set; }
    }
}
