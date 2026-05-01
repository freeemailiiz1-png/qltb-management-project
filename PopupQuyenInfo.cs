using QuanLyThietBi.Common;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupQuyenInfo : Form
    {
        private QuyenDAO quyenDAO = new QuyenDAO();
        private TrangThaiDAO trangThaiDAO = new TrangThaiDAO();
        private Quyen currentQuyen;
        private bool isEditMode = false;

        private TextBox txtTenQuyen;
        private ComboBox cboTrangThai;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTenQuyen;
        private Label lblTrangThai;

        public PopupQuyenInfo()
        {
            InitializeComponent();
            LoadTrangThai();
        }

        public PopupQuyenInfo(Quyen quyen) : this()
        {
            currentQuyen = quyen;
            isEditMode = true;
            LoadQuyenInfo();
        }

        private void InitializeComponent()
        {
            this.txtTenQuyen = new TextBox();
            this.cboTrangThai = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblTenQuyen = new Label();
            this.lblTrangThai = new Label();

            this.SuspendLayout();

            // lblTenQuyen
            this.lblTenQuyen.AutoSize = true;
            this.lblTenQuyen.Location = new Point(20, 30);
            this.lblTenQuyen.Name = "lblTenQuyen";
            this.lblTenQuyen.Size = new Size(80, 13);
            this.lblTenQuyen.TabIndex = 0;
            this.lblTenQuyen.Text = "Tên quyền:";

            // txtTenQuyen
            this.txtTenQuyen.Location = new Point(120, 27);
            this.txtTenQuyen.Name = "txtTenQuyen";
            this.txtTenQuyen.Size = new Size(250, 20);
            this.txtTenQuyen.TabIndex = 1;

            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new Point(20, 65);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new Size(80, 13);
            this.lblTrangThai.TabIndex = 2;
            this.lblTrangThai.Text = "Trạng thái:";

            // cboTrangThai
            this.cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new Point(120, 62);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new Size(250, 21);
            this.cboTrangThai.TabIndex = 3;

            // btnSave
            this.btnSave.Location = new Point(120, 105);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(270, 105);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // PopupQuyenInfo
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 165);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.txtTenQuyen);
            this.Controls.Add(this.lblTenQuyen);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupQuyenInfo";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông tin quyền";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadTrangThai()
        {
            var trangThais = trangThaiDAO.GetAll();
            cboTrangThai.DataSource = trangThais;
            cboTrangThai.DisplayMember = "trangThai";
            cboTrangThai.ValueMember = "IDTrangThai";
            cboTrangThai.SelectedIndex = -1;
        }

        private void LoadQuyenInfo()
        {
            if (currentQuyen != null)
            {
                txtTenQuyen.Text = currentQuyen.TenQuyen;

                if (currentQuyen.TrangThai.HasValue)
                {
                    cboTrangThai.SelectedValue = currentQuyen.TrangThai.Value;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenQuyen.Text))
            {
                MessageBox.Show("Vui lòng nhập tên quyền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var quyen = new Quyen
            {
                TenQuyen = txtTenQuyen.Text.Trim(),
                TrangThai = cboTrangThai.SelectedValue != null ? (int?)cboTrangThai.SelectedValue : null,

                // ✅ QUAN TRỌNG: Truyền UserID từ SessionManager
                UserID = SessionManager.GetCurrentUserID()
            };

            bool success = false;
            if (isEditMode)
            {
                quyen.ID = currentQuyen.ID;
                success = quyenDAO.Update(quyen);
            }
            else
            {
                success = quyenDAO.Insert(quyen);
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
