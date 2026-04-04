using QuanLyThietBi.Common;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class QLTrangThai : BaseForm
    {
        private UcCrudButtons ucCrudButtons;
        private List<TrangThai> trangThais = new List<TrangThai>();
        private TrangThaiDAO _trangThaiDao = new TrangThaiDAO();
        public QLTrangThai()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadTrangThai();
        }
        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            // Mở form popup ở chế độ thêm mới
            TrangThaiEditForm popup = new TrangThaiEditForm(); // Không truyền tham số để thêm mới
            popup.FormClosed += (s, args) => LoadTrangThai(); // Tải lại dữ liệu sau khi form popup đóng
            popup.ShowDialog();
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvTrangThai.CurrentRow == null) return;
            TrangThai trangThai = (TrangThai)dgvTrangThai.CurrentRow.DataBoundItem;
            TrangThaiEditForm popup = new TrangThaiEditForm(trangThai); // Truyền trạng thái cần sửa
            popup.FormClosed += (s, args) => LoadTrangThai(); // Tải lại dữ liệu sau khi form đóng
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            if (dgvTrangThai.CurrentRow == null) return;
            var trangThai = (TrangThai)dgvTrangThai.CurrentRow.DataBoundItem;
            if (MessageBox.Show("Bạn có chắc muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _trangThaiDao.Delete(trangThai.IDTrangThai); // Sử dụng đúng thuộc tính IDTrangThai
                LoadTrangThai();
            }
        }

        private void ucCrudButtons1_SearchClicked(object sender, EventArgs e)
        {
            // ...
            string keyword = StringHelper.RemoveDiacritics(txtSearch.Text.Trim().ToLower());
            var result = trangThais
                .Where(t =>
                    (!string.IsNullOrEmpty(t.trangThai) && StringHelper.RemoveDiacritics(t.trangThai.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(t.MoTa) && StringHelper.RemoveDiacritics(t.MoTa.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvTrangThai.DataSource = null;
            dgvTrangThai.DataSource = result;
        }
        private void LoadTrangThai()
        {
            trangThais = _trangThaiDao.GetAll(); // Lấy danh sách trạng thái từ DB
            dgvTrangThai.DataSource = null;
            dgvTrangThai.DataSource = trangThais;
        }
        private void QLTrangThai_Load(object sender, EventArgs e)
        {
            LoadTrangThai();
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string keyword = txtSearch.Text.Trim().ToLower();
            var result = trangThais
                .Where(t => t.trangThai != null && t.trangThai.ToLower().Contains(keyword))
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvTrangThai.DataSource = null;
            dgvTrangThai.DataSource = result;
        }
        
    }
}
