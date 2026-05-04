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
    public partial class PopupCapDonViInfo : Form
    {
        private CapDonVi _currentCapDonVi;
        CapDonViDAO capDonViDAO = new CapDonViDAO();
        public CapDonVi CapDonVi { get; private set; }

        public PopupCapDonViInfo()
        {
            InitializeComponent();
        }

        public PopupCapDonViInfo(CapDonVi capDonVi) : this()
        {
            _currentCapDonVi = capDonVi;
            if (_currentCapDonVi != null)
            {
                txtTenCapDonVi.Text = _currentCapDonVi.TenCapDV;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenCapDonVi.Text))
            {
                MessageBox.Show("Tên cấp đơn vị không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenCapDonVi.Focus();
                return;
            }

            CapDonVi = new CapDonVi
            {
                ID = _currentCapDonVi?.ID ?? 0,
                TenCapDV = txtTenCapDonVi.Text,

                // ✅ QUAN TRỌNG: Truyền UserID từ SessionManager
                UserID = SessionManager.GetCurrentUserID()
            };

            bool result;
            if (_currentCapDonVi == null)
            {
                // Thêm mới
                result = capDonViDAO.Insert(CapDonVi);
            }
            else
            {
                // Cập nhật
                result = capDonViDAO.Update(CapDonVi);
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
