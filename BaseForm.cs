using System;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public class BaseForm : Form
    {
        protected MenuStrip mainMenu;

        public BaseForm()
        {
            if (!DesignMode)
            {
                InitializeMenu();
            }
        }

        private void InitializeMenu()
        {
            mainMenu = new MenuStrip();

            // Nhóm 1: Quản lý danh mục
            var danhMucMenu = new ToolStripMenuItem("Quản lý danh mục");
            var qlThietBi = new ToolStripMenuItem("Quản lý thiết bị");
            qlThietBi.Click += (s, e) => { OpenForm(new QLThietBi()); };

            var qlUser = new ToolStripMenuItem("Quản lý tài khoản");
            qlUser.Click += (s, e) => { OpenForm(new QLUser()); };

            var qlDonVi = new ToolStripMenuItem("Quản lý đơn vị");
            qlDonVi.Click += (s, e) => { OpenForm(new QLDonVi()); };

            var qlCapDonVi = new ToolStripMenuItem("Quản lý cấp đơn vị");
            qlCapDonVi.Click += (s, e) => { OpenForm(new QLCapDonVi()); };

            var qlTrangThai = new ToolStripMenuItem("Quản lý trạng thái");
            qlTrangThai.Click += (s, e) => { OpenForm(new QLTrangThai()); };
            
            var qlCanBo = new ToolStripMenuItem("Quản lý Cán bộ");
            qlCanBo.Click += (s, e) => { OpenForm(new QLCanBo()); };
            
            var qlQuyen = new ToolStripMenuItem("Quản lý Quyền");
            qlQuyen.Click += (s, e) => { OpenForm(new QLQuyen()); };
            
            var qlLoaiThietBi = new ToolStripMenuItem("Quản lý Loại thiết bị");
            qlLoaiThietBi.Click += (s, e) => { OpenForm(new QLLoaiThietBi()); };
            
            


            danhMucMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                qlCapDonVi, qlDonVi, qlUser, qlTrangThai, qlCanBo, qlQuyen, qlLoaiThietBi,  qlThietBi
            });

            // Nhóm 2: Quản lý hệ thống
            var heThongMenu = new ToolStripMenuItem("Quản lý hệ thống");
            var qlHanhDong = new ToolStripMenuItem("Quản lý hành động");
            qlHanhDong.Click += (s, e) => { OpenForm(new QLHanhDong()); };

            var qlLichSuThietBi = new ToolStripMenuItem("Quản lý lịch sử thiết bị");
            qlLichSuThietBi.Click += (s, e) => { OpenForm(new QLLichSuThietBi()); };

            var qlLichSuHeThong = new ToolStripMenuItem("Quản lý lịch sử hệ thống");
            qlLichSuHeThong.Click += (s, e) => { OpenForm(new QLLichSuHeThong()); };

            heThongMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                qlHanhDong, qlLichSuThietBi, qlLichSuHeThong
            });

            mainMenu.Items.AddRange(new ToolStripItem[]
            {
                danhMucMenu, heThongMenu
            });

            this.MainMenuStrip = mainMenu;
            this.Controls.Add(mainMenu);
        }

        protected void OpenForm(Form form)
        {
            if (form.GetType() == this.GetType())
            {
                // Nếu đã là form hiện tại thì không làm gì
                return;
            }
            form.StartPosition = FormStartPosition.CenterScreen;
            form.FormClosed += (s, e) => this.Show();
            this.Hide();
            form.Show();
        }
    }
}
