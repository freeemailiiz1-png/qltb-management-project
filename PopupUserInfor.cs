using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Data;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupUserInfor : Form
    {
        private int currentUserID; // Biến để lưu ID, -1 là thêm mới
        private UserDAO userDAO = new UserDAO();

        // Constructor nhận UserID
        public PopupUserInfor(int userID)
        {
            InitializeComponent();
            this.currentUserID = userID;
        }

        private void PopupUserInfor_Load(object sender, EventArgs e)
        {
            // Tải dữ liệu cho các ComboBox (nếu có)
            // LoadDonViComboBox(); 
            // LoadCapDonViComboBox();

            if (currentUserID != -1)
            {
                // Chế độ chỉnh sửa: Tải thông tin người dùng
                this.Text = "Cập nhật thông tin người dùng";
                LoadUserData();
            }
            else
            {
                // Chế độ thêm mới
                this.Text = "Thêm người dùng mới";
            }
        }

        private void LoadUserData()
        {
            // Bạn cần một phương thức trong UserDAO để lấy thông tin một người dùng bằng ID
            // Ví dụ: User user = userDAO.GetUserByID(this.currentUserID);
            User user = userDAO.GetUserByID(this.currentUserID);
            // Giả sử bạn đã có phương thức GetUserByID và nó trả về một đối tượng User

            if (user != null)
            {
                txtUsername.Text = user.TenDangNhap;
                // Không fill mật khẩu
                cboDonVi.SelectedValue = user.DonViID;
                cboCapDonVi.SelectedValue = user.CapDonViID;
                // ... fill các control khác
            }
            
            MessageBox.Show("Chức năng tải thông tin chi tiết người dùng chưa được triển khai.");
        }

        private void btnLuu_Click(object sender, EventArgs e) // Giả sử nút lưu của bạn tên là btnLuu
        {
            
        }

        private void btnDong_Click(object sender, EventArgs e) // Giả sử nút đóng của bạn tên là btnDong
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu từ các control trên form
            User user = new User();
            user.TenDangNhap = txtUsername.Text.Trim();
            user.MatKhau = txtPassword.Text; // Để trống nếu không muốn đổi mật khẩu
            // user.DonViID = (int)cboDonVi.SelectedValue;
            // ...

            // 2. Kiểm tra dữ liệu hợp lệ (validation)
            if (string.IsNullOrEmpty(user.TenDangNhap))
            {
                MessageBox.Show("Tên đăng nhập không được để trống.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (currentUserID == -1 && string.IsNullOrEmpty(user.MatKhau))
            {
                MessageBox.Show("Mật khẩu không được để trống khi thêm mới.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Gọi DAO để lưu
            bool success;
            if (currentUserID == -1) // Thêm mới
            {
                user.TrangThai = 1; // Mặc định là hoạt động
                success = userDAO.AddUser(user);
            }
            else // Cập nhật
            {
                user.ID = this.currentUserID;
                success = userDAO.UpdateUser(user);
            }

            // 4. Thông báo kết quả và đóng form
            if (success)
            {
                MessageBox.Show(currentUserID == -1 ? "Thêm mới thành công!" : "Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(currentUserID == -1 ? "Thêm mới thất bại." : "Cập nhật thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
