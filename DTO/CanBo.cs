using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class CanBo
    {
        public int ID { get; set; }
        public string HoTen { get; set; }
        public string CCCD { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string Email { get; set; }
    }
}
