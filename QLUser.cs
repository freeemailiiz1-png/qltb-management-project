using QuanLyThietBi.DAO;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using QuanLyThietBi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class QLUser : BaseForm
    {
        private UserDAO userDAO = new UserDAO();
        private List<User> users = new List<User>();

        public QLUser()
        {
            InitializeComponent();
            ucCrudButtons.AddClicked += ucCrudButtons_AddClicked;
            ucCrudButtons.EditClicked += ucCrudButtons_EditClicked;
            ucCrudButtons.DeleteClicked += ucCrudButtons_DeleteClicked;
            ucCrudButtons.SearchClicked += ucCrudButtons_SearchClicked;
            LoadUsers();
        }

        private void LoadUsers()
        {
            try
            {
                users = userDAO.GetAll();
                dgUser.DataSource = users;

                // Tùy chỉnh hiển thị cột
                if (dgUser.Columns["ID"] != null)
                {
                    dgUser.Columns["ID"].HeaderText = "Mã";
                    dgUser.Columns["ID"].Width = 50;
                }
                if (dgUser.Columns["TenDangNhap"] != null)
                {
                    dgUser.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
                    dgUser.Columns["TenDangNhap"].Width = 150;
                }
                if (dgUser.Columns["TenDonVi"] != null)
                {
                    dgUser.Columns["TenDonVi"].HeaderText = "Đơn vị";
                    dgUser.Columns["TenDonVi"].Width = 200;
                }
                if (dgUser.Columns["TenCapDonVi"] != null)
                {
                    dgUser.Columns["TenCapDonVi"].HeaderText = "Cấp đơn vị";
                    dgUser.Columns["TenCapDonVi"].Width = 150;
                }
                if (dgUser.Columns["TenTrangThai"] != null)
                {
                    dgUser.Columns["TenTrangThai"].HeaderText = "Trạng thái";
                    dgUser.Columns["TenTrangThai"].Width = 100;
                }

                // Ẩn các cột không cần thiết
                if (dgUser.Columns["MatKhau"] != null)
                    dgUser.Columns["MatKhau"].Visible = false;
                if (dgUser.Columns["DonViID"] != null)
                    dgUser.Columns["DonViID"].Visible = false;
                if (dgUser.Columns["CapDonViID"] != null)
                    dgUser.Columns["CapDonViID"].Visible = false;
                if (dgUser.Columns["TrangThai"] != null)
                    dgUser.Columns["TrangThai"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCrudButtons_AddClicked(object sender, EventArgs e)
        {
            PopupUserInfor popup = new PopupUserInfor();
            popup.FormClosed += (s, args) => LoadUsers();
            popup.ShowDialog();
        }

        private void ucCrudButtons_EditClicked(object sender, EventArgs e)
        {
            if (dgUser.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            User selectedUser = dgUser.CurrentRow.DataBoundItem as User;
            if (selectedUser == null)
                return;

            PopupUserInfor popup = new PopupUserInfor(selectedUser);
            popup.FormClosed += (s, args) => LoadUsers();
            popup.ShowDialog();
        }

        private void ucCrudButtons_DeleteClicked(object sender, EventArgs e)
        {
            if (dgUser.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedID = Convert.ToInt32(dgUser.CurrentRow.Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = userDAO.DeleteUser(selectedID);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ucCrudButtons_SearchClicked(object sender, EventArgs e)
        {
            string keyword = StringHelper.RemoveDiacritics(txtSearch.Text.Trim().ToLower());
            var result = users
                .Where(u =>
                    (!string.IsNullOrEmpty(u.TenDangNhap) && StringHelper.RemoveDiacritics(u.TenDangNhap.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(u.TenDonVi) && StringHelper.RemoveDiacritics(u.TenDonVi.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(u.TenCapDonVi) && StringHelper.RemoveDiacritics(u.TenCapDonVi.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(u.TenTrangThai) && StringHelper.RemoveDiacritics(u.TenTrangThai.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //dgUser.DataSource = null;
            dgUser.DataSource = result;
        }

        private void QLUser_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}
