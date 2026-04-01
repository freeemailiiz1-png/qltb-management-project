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
    public partial class QLHanhDong : BaseForm
    {
        private UcCrudButtons ucCrudButtons;
        public QLHanhDong()
        {
            InitializeComponent();

            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
        }
        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            // TODO: Implement add logic for TrangThai
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            // TODO: Implement edit logic for TrangThai
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            // TODO: Implement delete logic for TrangThai
        }

        private void ucCrudButtons1_SearchClicked(object sender, EventArgs e)
        {
            // TODO: Implement search logic for TrangThai
        }
    }
}
