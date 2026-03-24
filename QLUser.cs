using QuanLyThietBi.DAO;
using System;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class QLUser : Form
    {
        // Khởi tạo DAO để có thể sử dụng trong toàn bộ form
        private UserDAO userDAO = new UserDAO();

        public QLUser()
        {
            InitializeComponent();
        }

        // Phương thức để tải và hiển thị danh sách người dùng lên DataGridView
        private void LoadUsers()
        {
            try
            {
                // Gọi phương thức từ DAO và gán kết quả cho DataGridView
                dgUser.DataSource = userDAO.GetAllUsers();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách người dùng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Mở form popup ở chế độ thêm mới
            PopupUserInfor popup = new PopupUserInfor(-1); // Truyền -1 cho chế độ thêm mới
            popup.FormClosed += (s, args) => LoadUsers(); // Tải lại dữ liệu sau khi form popup đóng
            popup.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem đã chọn dòng nào chưa
            if (dgUser.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy ID từ dòng được chọn
            int selectedUserID = Convert.ToInt32(dgUser.SelectedRows[0].Cells["ID"].Value);

            // Mở form popup ở chế độ chỉnh sửa
            PopupUserInfor popup = new PopupUserInfor(selectedUserID);
            popup.FormClosed += (s, args) => LoadUsers(); // Tải lại dữ liệu sau khi form popup đóng
            popup.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgUser.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một người dùng để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedUserID = Convert.ToInt32(dgUser.SelectedRows[0].Cells["ID"].Value);

            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa người dùng này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = userDAO.DeleteUser(selectedUserID);
                if (success)
                {
                    MessageBox.Show("Xóa người dùng thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadUsers(); // Tải lại danh sách
                }
                else
                {
                    MessageBox.Show("Xóa người dùng thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Giả sử bạn có một TextBox tên là txtSearch
            // string searchTerm = txtSearch.Text;
            // DataTable searchResult = userDAO.SearchUsers(searchTerm); // Bạn cần tạo phương thức này trong UserDAO
            // dgUser.DataSource = searchResult;
            MessageBox.Show("Chức năng tìm kiếm chưa được triển khai.");
        }

        private void QLUser_Load(object sender, EventArgs e)
        {
            LoadUsers();
        }
    }
}
