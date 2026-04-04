using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class HanhDong
    {
        public int ID { get; set; }
        public string name { get; set; }

        // Thuộc tính bổ sung (alias) để tương thích
        public string TenHanhDong
        {
            get { return name; }
            set { name = value; }
        }
    }
}
