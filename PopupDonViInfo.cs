using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupDonViInfo : Form
    {
        private DonViDAO donViDAO = new DonViDAO();
        private CapDonViDAO capDonViDAO = new CapDonViDAO();
        private TrangThaiDAO trangThaiDAO = new TrangThaiDAO();
        private DonVi currentDonVi;
        private bool isEditMode = false;

        private TextBox txtMaDV;
        private TextBox txtTenDV;
        private ComboBox cboCapDV;
        private ComboBox cboDonViCha;
        private ComboBox cboTrangThai;
        private Button btnSave;
        private Button btnCancel;
        private Label lblMaDV;
        private Label lblTenDV;
        private Label lblCapDV;
        private Label lblDonViCha;
        private Label lblTrangThai;

        public PopupDonViInfo()
        {
            InitializeComponent();
            LoadCapDonVi();
            LoadDonViCha();
            LoadTrangThai();
        }

        public PopupDonViInfo(DonVi donVi) : this()
        {
            currentDonVi = donVi;
            isEditMode = true;
            LoadDonViInfo();
        }

        private void InitializeComponent()
        {
            this.txtMaDV = new TextBox();
            this.txtTenDV = new TextBox();
            this.cboCapDV = new ComboBox();
            this.cboDonViCha = new ComboBox();
            this.cboTrangThai = new ComboBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblMaDV = new Label();
            this.lblTenDV = new Label();
            this.lblCapDV = new Label();
            this.lblDonViCha = new Label();
            this.lblTrangThai = new Label();

            this.SuspendLayout();

            // lblMaDV
            this.lblMaDV.AutoSize = true;
            this.lblMaDV.Location = new Point(20, 30);
            this.lblMaDV.Name = "lblMaDV";
            this.lblMaDV.Size = new Size(80, 13);
            this.lblMaDV.TabIndex = 0;
            this.lblMaDV.Text = "Mã đơn vị:";

            // txtMaDV
            this.txtMaDV.Location = new Point(120, 27);
            this.txtMaDV.Name = "txtMaDV";
            this.txtMaDV.Size = new Size(250, 20);
            this.txtMaDV.TabIndex = 1;

            // lblTenDV
            this.lblTenDV.AutoSize = true;
            this.lblTenDV.Location = new Point(20, 65);
            this.lblTenDV.Name = "lblTenDV";
            this.lblTenDV.Size = new Size(80, 13);
            this.lblTenDV.TabIndex = 2;
            this.lblTenDV.Text = "Tên đơn vị:";

            // txtTenDV
            this.txtTenDV.Location = new Point(120, 62);
            this.txtTenDV.Name = "txtTenDV";
            this.txtTenDV.Size = new Size(250, 20);
            this.txtTenDV.TabIndex = 3;

            // lblCapDV
            this.lblCapDV.AutoSize = true;
            this.lblCapDV.Location = new Point(20, 100);
            this.lblCapDV.Name = "lblCapDV";
            this.lblCapDV.Size = new Size(80, 13);
            this.lblCapDV.TabIndex = 4;
            this.lblCapDV.Text = "Cấp đơn vị:";

            // cboCapDV
            this.cboCapDV.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboCapDV.FormattingEnabled = true;
            this.cboCapDV.Location = new Point(120, 97);
            this.cboCapDV.Name = "cboCapDV";
            this.cboCapDV.Size = new Size(250, 21);
            this.cboCapDV.TabIndex = 5;

            // lblDonViCha
            this.lblDonViCha.AutoSize = true;
            this.lblDonViCha.Location = new Point(20, 135);
            this.lblDonViCha.Name = "lblDonViCha";
            this.lblDonViCha.Size = new Size(80, 13);
            this.lblDonViCha.TabIndex = 6;
            this.lblDonViCha.Text = "Đơn vị cha:";

            // cboDonViCha
            this.cboDonViCha.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDonViCha.FormattingEnabled = true;
            this.cboDonViCha.Location = new Point(120, 132);
            this.cboDonViCha.Name = "cboDonViCha";
            this.cboDonViCha.Size = new Size(250, 21);
            this.cboDonViCha.TabIndex = 7;

            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new Point(20, 170);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new Size(80, 13);
            this.lblTrangThai.TabIndex = 8;
            this.lblTrangThai.Text = "Trạng thái:";

            // cboTrangThai
            this.cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new Point(120, 167);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new Size(250, 21);
            this.cboTrangThai.TabIndex = 9;

            // btnSave
            this.btnSave.Location = new Point(120, 210);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(270, 210);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // PopupDonViInfo
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(400, 270);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.cboDonViCha);
            this.Controls.Add(this.lblDonViCha);
            this.Controls.Add(this.cboCapDV);
            this.Controls.Add(this.lblCapDV);
            this.Controls.Add(this.txtTenDV);
            this.Controls.Add(this.lblTenDV);
            this.Controls.Add(this.txtMaDV);
            this.Controls.Add(this.lblMaDV);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupDonViInfo";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông tin đơn vị";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadCapDonVi()
        {
            var capDonVis = capDonViDAO.GetAll();
            cboCapDV.DataSource = capDonVis;
            cboCapDV.DisplayMember = "TenCapDV";
            cboCapDV.ValueMember = "ID";
            cboCapDV.SelectedIndex = -1;
        }

        private void LoadDonViCha()
        {
            var donVis = donViDAO.GetAll();
            cboDonViCha.DataSource = donVis;
            cboDonViCha.DisplayMember = "TenDV";
            cboDonViCha.ValueMember = "ID";
            cboDonViCha.SelectedIndex = -1;
        }

        private void LoadTrangThai()
        {
            var trangThais = trangThaiDAO.GetAll();
            cboTrangThai.DataSource = trangThais;
            cboTrangThai.DisplayMember = "trangThai";
            cboTrangThai.ValueMember = "IDTrangThai";
            cboTrangThai.SelectedIndex = -1;
        }   

        private void LoadDonViInfo()
        {
            if (currentDonVi != null)
            {
                txtMaDV.Text = currentDonVi.MaDV;
                txtTenDV.Text = currentDonVi.TenDV;
                
                if (currentDonVi.CapDV.HasValue)
                {
                    cboCapDV.SelectedValue = currentDonVi.CapDV.Value;
                }
                
                if (currentDonVi.DonViChaID.HasValue)
                {
                    cboDonViCha.SelectedValue = currentDonVi.DonViChaID.Value;
                }
                
                if (currentDonVi.TrangThai.HasValue)
                {
                    cboTrangThai.SelectedValue = currentDonVi.TrangThai.Value;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenDV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đơn vị.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var donVi = new DonVi
            {
                MaDV = txtMaDV.Text.Trim(),
                TenDV = txtTenDV.Text.Trim(),
                CapDV = cboCapDV.SelectedValue != null ? (int?)cboCapDV.SelectedValue : null,
                DonViChaID = cboDonViCha.SelectedValue != null ? (int?)cboDonViCha.SelectedValue : null,
                TrangThai = cboTrangThai.SelectedValue != null ? (int?)cboTrangThai.SelectedValue : null,
                NgayTao = isEditMode ? currentDonVi.NgayTao : DateTime.Now
            };

            bool success = false;
            if (isEditMode)
            {
                donVi.ID = currentDonVi.ID;
                success = donViDAO.Update(donVi);
            }
            else
            {
                success = donViDAO.Insert(donVi);
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