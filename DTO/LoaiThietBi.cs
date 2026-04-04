using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class LoaiThietBi
    {
        public int ID { get; set; }
        public string TenLoai { get; set; }
        public string MoTa { get; set; }
        public int? VongDoiNam { get; set; }
    }
}
