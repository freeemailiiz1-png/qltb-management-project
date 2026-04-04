using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupLoaiThietBiInfo : Form
    {
        private LoaiThietBiDAO loaiThietBiDAO = new LoaiThietBiDAO();
        private LoaiThietBi currentLoaiThietBi;
        private bool isEditMode = false;

        private TextBox txtTenLoai;
        private TextBox txtMoTa;
        private NumericUpDown numVongDoiNam;
        private Button btnSave;
        private Button btnCancel;
        private Label lblTenLoai;
        private Label lblMoTa;
        private Label lblVongDoiNam;

        public PopupLoaiThietBiInfo()
        {
            InitializeComponent();
        }

        public PopupLoaiThietBiInfo(LoaiThietBi loaiThietBi) : this()
        {
            currentLoaiThietBi = loaiThietBi;
            isEditMode = true;
            LoadLoaiThietBiInfo();
        }

        private void InitializeComponent()
        {
            this.txtTenLoai = new TextBox();
            this.txtMoTa = new TextBox();
            this.numVongDoiNam = new NumericUpDown();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblTenLoai = new Label();
            this.lblMoTa = new Label();
            this.lblVongDoiNam = new Label();

            ((System.ComponentModel.ISupportInitialize)(this.numVongDoiNam)).BeginInit();
            this.SuspendLayout();

            // lblTenLoai
            this.lblTenLoai.AutoSize = true;
            this.lblTenLoai.Location = new Point(20, 30);
            this.lblTenLoai.Name = "lblTenLoai";
            this.lblTenLoai.Size = new Size(100, 13);
            this.lblTenLoai.TabIndex = 0;
            this.lblTenLoai.Text = "Tên loại thiết bị:";

            // txtTenLoai
            this.txtTenLoai.Location = new Point(130, 27);
            this.txtTenLoai.Name = "txtTenLoai";
            this.txtTenLoai.Size = new Size(250, 20);
            this.txtTenLoai.TabIndex = 1;

            // lblMoTa
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new Point(20, 65);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new Size(100, 13);
            this.lblMoTa.TabIndex = 2;
            this.lblMoTa.Text = "Mô tả:";

            // txtMoTa
            this.txtMoTa.Location = new Point(130, 62);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new Size(250, 60);
            this.txtMoTa.TabIndex = 3;

            // lblVongDoiNam
            this.lblVongDoiNam.AutoSize = true;
            this.lblVongDoiNam.Location = new Point(20, 135);
            this.lblVongDoiNam.Name = "lblVongDoiNam";
            this.lblVongDoiNam.Size = new Size(100, 13);
            this.lblVongDoiNam.TabIndex = 4;
            this.lblVongDoiNam.Text = "Vòng đời (năm):";

            // numVongDoiNam
            this.numVongDoiNam.Location = new Point(130, 133);
            this.numVongDoiNam.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.numVongDoiNam.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            this.numVongDoiNam.Name = "numVongDoiNam";
            this.numVongDoiNam.Size = new Size(120, 20);
            this.numVongDoiNam.TabIndex = 5;

            // btnSave
            this.btnSave.Location = new Point(130, 175);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(280, 175);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // PopupLoaiThietBiInfo
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(410, 230);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.numVongDoiNam);
            this.Controls.Add(this.lblVongDoiNam);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtTenLoai);
            this.Controls.Add(this.lblTenLoai);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupLoaiThietBiInfo";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông tin loại thiết bị";
            ((System.ComponentModel.ISupportInitialize)(this.numVongDoiNam)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadLoaiThietBiInfo()
        {
            if (currentLoaiThietBi != null)
            {
                txtTenLoai.Text = currentLoaiThietBi.TenLoai;
                txtMoTa.Text = currentLoaiThietBi.MoTa;

                if (currentLoaiThietBi.VongDoiNam.HasValue)
                {
                    numVongDoiNam.Value = currentLoaiThietBi.VongDoiNam.Value;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenLoai.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại thiết bị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var loaiThietBi = new LoaiThietBi
            {
                TenLoai = txtTenLoai.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(),
                VongDoiNam = (int)numVongDoiNam.Value
            };

            bool success = false;
            if (isEditMode)
            {
                loaiThietBi.ID = currentLoaiThietBi.ID;
                success = loaiThietBiDAO.Update(loaiThietBi);
            }
            else
            {
                success = loaiThietBiDAO.Insert(loaiThietBi);
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
