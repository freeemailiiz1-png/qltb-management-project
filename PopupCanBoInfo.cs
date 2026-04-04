using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupCanBoInfo : Form
    {
        private CanBoDAO canBoDAO = new CanBoDAO();
        private CanBo currentCanBo;
        private bool isEditMode = false;

        private TextBox txtHoTen;
        private TextBox txtCCCD;
        private TextBox txtEmail;
        private DateTimePicker dtpNgaySinh;
        private Button btnSave;
        private Button btnCancel;
        private Label lblHoTen;
        private Label lblCCCD;
        private Label lblEmail;
        private Label lblNgaySinh;

        public PopupCanBoInfo()
        {
            InitializeComponent();
        }

        public PopupCanBoInfo(CanBo canBo) : this()
        {
            currentCanBo = canBo;
            isEditMode = true;
            LoadCanBoInfo();
        }

        private void InitializeComponent()
        {
            this.txtHoTen = new TextBox();
            this.txtCCCD = new TextBox();
            this.txtEmail = new TextBox();
            this.dtpNgaySinh = new DateTimePicker();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblHoTen = new Label();
            this.lblCCCD = new Label();
            this.lblEmail = new Label();
            this.lblNgaySinh = new Label();

            this.SuspendLayout();

            // lblHoTen
            this.lblHoTen.AutoSize = true;
            this.lblHoTen.Location = new Point(20, 30);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new Size(100, 13);
            this.lblHoTen.Text = "Họ tên:";

            // txtHoTen
            this.txtHoTen.Location = new Point(130, 27);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Size = new Size(250, 20);
            this.txtHoTen.TabIndex = 1;

            // lblCCCD
            this.lblCCCD.AutoSize = true;
            this.lblCCCD.Location = new Point(20, 65);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new Size(100, 13);
            this.lblCCCD.Text = "CCCD:";

            // txtCCCD
            this.txtCCCD.Location = new Point(130, 62);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.Size = new Size(250, 20);
            this.txtCCCD.TabIndex = 2;

            // lblNgaySinh
            this.lblNgaySinh.AutoSize = true;
            this.lblNgaySinh.Location = new Point(20, 100);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new Size(100, 13);
            this.lblNgaySinh.Text = "Ngày sinh:";

            // dtpNgaySinh
            this.dtpNgaySinh.Format = DateTimePickerFormat.Short;
            this.dtpNgaySinh.Location = new Point(130, 97);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Size = new Size(250, 20);
            this.dtpNgaySinh.TabIndex = 3;

            // lblEmail
            this.lblEmail.AutoSize = true;
            this.lblEmail.Location = new Point(20, 135);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new Size(100, 13);
            this.lblEmail.Text = "Email:";

            // txtEmail
            this.txtEmail.Location = new Point(130, 132);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new Size(250, 20);
            this.txtEmail.TabIndex = 4;

            // btnSave
            this.btnSave.Location = new Point(130, 175);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(280, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // PopupCanBoInfo
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(410, 230);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtEmail);
            this.Controls.Add(this.lblEmail);
            this.Controls.Add(this.dtpNgaySinh);
            this.Controls.Add(this.lblNgaySinh);
            this.Controls.Add(this.txtCCCD);
            this.Controls.Add(this.lblCCCD);
            this.Controls.Add(this.txtHoTen);
            this.Controls.Add(this.lblHoTen);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupCanBoInfo";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông tin cán bộ";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadCanBoInfo()
        {
            if (currentCanBo != null)
            {
                txtHoTen.Text = currentCanBo.HoTen;
                txtCCCD.Text = currentCanBo.CCCD;
                txtEmail.Text = currentCanBo.Email;

                if (currentCanBo.NgaySinh.HasValue)
                    dtpNgaySinh.Value = currentCanBo.NgaySinh.Value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var canBo = new CanBo
            {
                HoTen = txtHoTen.Text.Trim(),
                CCCD = txtCCCD.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                NgaySinh = dtpNgaySinh.Value
            };

            bool success = false;
            if (isEditMode)
            {
                canBo.ID = currentCanBo.ID;
                success = canBoDAO.Update(canBo);
            }
            else
            {
                success = canBoDAO.Insert(canBo);
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
