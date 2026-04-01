using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    internal class ThietBi
    {
        public int IDThietBi { get; set; }
        public int IDLoaiThietBi { get; set; }
        public string TenThietBi { get; set; }
        public string SerialNumber { get; set; }
        public int CanBoID { get; set; }
        public int DonViID { get; set; }
        public DateTime NgayCap { get; set; }
        public DateTime NgayHetHan { get; set; }
        public DateTime NgayHetHanVongDoi { get; set; }
        public int TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
