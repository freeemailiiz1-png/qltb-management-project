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
    public partial class TrangThaiEditForm : Form
    {
        private TrangThai _currentTrangThai;
        TrangThaiDAO trangThaiDAO = new TrangThaiDAO();
        public TrangThai TrangThai { get; private set; }
        public TrangThaiEditForm()
        {
            InitializeComponent();
        }

        public TrangThaiEditForm(TrangThai trangThai)
        {
            InitializeComponent();
            _currentTrangThai = trangThai;

            if (_currentTrangThai != null)
            {
                // Ví dụ: gán giá trị lên các textbox
                txtTenTrangThai.Text = _currentTrangThai.trangThai;
                txtMoTa.Text = _currentTrangThai.MoTa;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (string.IsNullOrWhiteSpace(txtTenTrangThai.Text))
            {
                MessageBox.Show("Tên trạng thái không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTrangThai.Focus();
                return;
            }

            TrangThai = new TrangThai
            {
                IDTrangThai = _currentTrangThai?.IDTrangThai ?? 0,
                trangThai = txtTenTrangThai.Text,
                MoTa = txtMoTa.Text
            };

            bool result;
            if (_currentTrangThai == null)
            {
                // Thêm mới
                result = trangThaiDAO.Insert(TrangThai);
            }
            else
            {
                // Cập nhật
                result = trangThaiDAO.Update(TrangThai);
            }

            if (result)
            {
                MessageBox.Show("Lưu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lưu thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

}
