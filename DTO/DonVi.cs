using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    internal class DonVi
    {
        public int IDDonVi { get; set; }
        public string TenDonVi { get; set; }
        public string MaDonVi { get; set; }
        public int IDCapDonVi { get; set; }
        public int IDDonViCha { get; set; }
        public int TrangThai { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
