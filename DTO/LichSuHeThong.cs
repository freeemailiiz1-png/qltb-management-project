using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DTO
{
    public class LichSuHeThong
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string HanhDong { get; set; }  // Tên hành động (Thêm, Sửa, Xóa...)
        public string BangTacDong { get; set; }  // Tên bảng (tblThietBi, tblCanBo...)
        public int? BanGhiID { get; set; }  // ID của bản ghi bị tác động
        public DateTime? ThoiDiem { get; set; }
        public string NoiDungCu { get; set; }  // JSON hoặc text mô tả trước khi thay đổi
        public string NoiDungMoi { get; set; }  // JSON hoặc text mô tả sau khi thay đổi

        // Thuộc tính bổ sung để hiển thị
        public string TenUser { get; set; }  // Tên user thực hiện
    }
}
