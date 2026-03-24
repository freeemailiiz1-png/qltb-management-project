using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    internal class LichSuHeThong
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int HanhDongID { get; set; }
        public string BangTacDong { get; set; }
        public int BanGhiID { get; set; }
        public DateTime ThoiDiem { get; set; }
        public string NoiDungCu { get; set; }
        public string NoiDungMoi { get; set; }
    }
}
