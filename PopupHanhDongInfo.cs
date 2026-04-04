using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupHanhDongInfo : Form
    {
        private HanhDongDAO hanhDongDAO = new HanhDongDAO();
        private HanhDong currentHanhDong;
        private bool isEditMode = false;

        private TextBox txtName;
        private Button btnSave;
        private Button btnCancel;
        private Label lblName;

        public PopupHanhDongInfo()
        {
            InitializeComponent();
        }

        public PopupHanhDongInfo(HanhDong hanhDong) : this()
        {
            currentHanhDong = hanhDong;
            isEditMode = true;
            LoadHanhDongInfo();
        }

        private void InitializeComponent()
        {
            this.txtName = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblName = new Label();

            this.SuspendLayout();

            // lblName
            this.lblName.AutoSize = true;
            this.lblName.Location = new Point(20, 30);
            this.lblName.Name = "lblName";
            this.lblName.Size = new Size(100, 13);
            this.lblName.Text = "Tên hành động:";

            // txtName
            this.txtName.Location = new Point(130, 27);
            this.txtName.Name = "txtName";
            this.txtName.Size = new Size(250, 20);
            this.txtName.TabIndex = 1;

            // btnSave
            this.btnSave.Location = new Point(130, 75);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(280, 75);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // PopupHanhDongInfo
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(410, 130);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupHanhDongInfo";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông tin hành động";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadHanhDongInfo()
        {
            if (currentHanhDong != null)
            {
                txtName.Text = currentHanhDong.name;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên hành động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hanhDong = new HanhDong
            {
                name = txtName.Text.Trim()
            };

            bool success = false;
            if (isEditMode)
            {
                hanhDong.ID = currentHanhDong.ID;
                success = hanhDongDAO.Update(hanhDong);
            }
            else
            {
                success = hanhDongDAO.Insert(hanhDong);
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
