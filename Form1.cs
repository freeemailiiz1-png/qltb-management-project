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
    public partial class Form1 : Form
    {
        UserDAO dao = new UserDAO();
        User user = new User();
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            user = dao.Login(txtUsername.Text, txtPassword.Text);
            if (user != null)
            {
                // Lưu thông tin user vào Session
                SessionManager.SetCurrentUser(user);

                MessageBox.Show("Đăng nhập thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                QLUser mainForm = new QLUser();
                mainForm.FormClosed += (s, args) =>
                {
                    // Đăng xuất khi đóng form chính
                    SessionManager.Logout();
                    this.Show();
                };
                mainForm.Show();
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại! Vui lòng kiểm tra lại tên đăng nhập và mật khẩu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
