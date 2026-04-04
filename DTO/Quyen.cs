using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class Quyen
    {
        public int ID { get; set; }
        public string TenQuyen { get; set; }
        public int? TrangThai { get; set; }

        // Thuộc tính bổ sung cho hiển thị
        public string TenTrangThai { get; set; }
    }
}
