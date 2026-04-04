using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupUserInfor : Form
    {
        private UserDAO userDAO = new UserDAO();
        private DonViDAO donViDAO = new DonViDAO();
        private CapDonViDAO capDonViDAO = new CapDonViDAO();
        private TrangThaiDAO trangThaiDAO = new TrangThaiDAO();
        private User currentUser;
        private bool isEditMode = false;

        public PopupUserInfor()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public PopupUserInfor(User user) : this()
        {
            currentUser = user;
            isEditMode = true;
            LoadUserInfo();
        }

        private void LoadComboBoxData()
        {
            // Load Đơn vị
            var donVis = donViDAO.GetAll();
            cboDonVi.DataSource = donVis;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "ID";
            cboDonVi.SelectedIndex = -1;

            // Load Cấp đơn vị
            var capDonVis = capDonViDAO.GetAll();
            cboCapDonVi.DataSource = capDonVis;
            cboCapDonVi.DisplayMember = "TenCapDV";
            cboCapDonVi.ValueMember = "ID";
            cboCapDonVi.SelectedIndex = -1;

            // Load Trạng thái
            var trangThais = trangThaiDAO.GetAll();
            cboTrangThai.DataSource = trangThais;
            cboTrangThai.DisplayMember = "trangThai";
            cboTrangThai.ValueMember = "IDTrangThai";
            cboTrangThai.SelectedIndex = -1;
        }

        private void LoadUserInfo()
        {
            if (currentUser != null)
            {
                txtUsername.Text = currentUser.TenDangNhap;
                // Để trống mật khẩu

                if (currentUser.DonViID.HasValue)
                    cboDonVi.SelectedValue = currentUser.DonViID.Value;

                if (currentUser.CapDonViID.HasValue)
                    cboCapDonVi.SelectedValue = currentUser.CapDonViID.Value;

                if (currentUser.TrangThai.HasValue)
                    cboTrangThai.SelectedValue = currentUser.TrangThai.Value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isEditMode && string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var user = new User
            {
                TenDangNhap = txtUsername.Text.Trim(),
                MatKhau = txtPassword.Text, // Sẽ được hash trong DAO
                DonViID = cboDonVi.SelectedValue != null ? (int?)cboDonVi.SelectedValue : null,
                CapDonViID = cboCapDonVi.SelectedValue != null ? (int?)cboCapDonVi.SelectedValue : null,
                TrangThai = cboTrangThai.SelectedValue != null ? (int?)cboTrangThai.SelectedValue : null
            };

            bool success = false;
            if (isEditMode)
            {
                user.ID = currentUser.ID;
                success = userDAO.UpdateUser(user);
            }
            else
            {
                success = userDAO.AddUser(user);
            }

            if (success)
            {
                MessageBox.Show(isEditMode ? "Cập nhật thành công!" : "Thêm mới thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
