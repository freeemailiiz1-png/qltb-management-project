using System;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class UcCrudButtons : UserControl
    {
        public event EventHandler AddClicked;
        public event EventHandler EditClicked;
        public event EventHandler DeleteClicked;
        public event EventHandler SearchClicked;

        private Button btnAdd;
        private Button btnEdit;
        private Button btnDelete;
        private Button btnSearch;
        private TextBox txtSearch;

        public UcCrudButtons()
        {
            InitializeComponent();
        }
        public TextBox TxtSearch => txtSearch;

        private void InitializeComponent()
        {
            this.btnAdd = new Button();
            this.btnEdit = new Button();
            this.btnDelete = new Button();
            this.btnSearch = new Button();
            this.txtSearch = new TextBox();

            // Set properties
            this.btnAdd.Text = "Thêm";
            this.btnEdit.Text = "Sửa";
            this.btnDelete.Text = "Xóa";
            this.btnSearch.Text = "Tìm kiếm";

            // Set button locations (simple horizontal layout)
            this.btnAdd.Location = new System.Drawing.Point(0, 0);
            this.btnEdit.Location = new System.Drawing.Point(90, 0);
            this.btnDelete.Location = new System.Drawing.Point(180, 0);
            this.btnSearch.Location = new System.Drawing.Point(270, 0);
            this.txtSearch.Location = new System.Drawing.Point(360, 0);
            this.btnAdd.Size = this.btnEdit.Size = this.btnDelete.Size = this.btnSearch.Size = new System.Drawing.Size(80, 30);

            // Add event handlers
            this.btnAdd.Click += BtnAdd_Click;
            this.btnEdit.Click += BtnEdit_Click;
            this.btnDelete.Click += BtnDelete_Click;
            this.btnSearch.Click += BtnSearch_Click;

            // Add buttons to UserControl
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearch);
            // Set UserControl size
            this.Size = new System.Drawing.Size(490, 30);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            AddClicked?.Invoke(this, e);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            EditClicked?.Invoke(this, e);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DeleteClicked?.Invoke(this, e);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            SearchClicked?.Invoke(this, e);
        }
    }
}
