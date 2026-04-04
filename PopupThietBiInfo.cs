using QuanLyThietBi.DAO;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupThietBiInfo : Form
    {
        private ThietBiDAO thietBiDAO = new ThietBiDAO();
        private LoaiThietBiDAO loaiThietBiDAO = new LoaiThietBiDAO();
        private CanBoDAO canBoDAO = new CanBoDAO();
        private DonViDAO donViDAO = new DonViDAO();
        private TrangThaiDAO trangThaiDAO = new TrangThaiDAO();
        private LichSuThietBiDAO lichSuDAO = new LichSuThietBiDAO();
        private HanhDongDAO hanhDongDAO = new HanhDongDAO();
        private ThietBi currentThietBi;
        private bool isEditMode = false;

        private TextBox txtSerialNumber;
        private TextBox txtGhiChu;
        private ComboBox cboLoaiThietBi;
        private SearchableComboBox searchableCanBo;
        private ComboBox cboDonVi;
        private ComboBox cboTrangThai;
        private DateTimePicker dtpNgayCap;
        private DateTimePicker dtpNgayHetHan;
        private DateTimePicker dtpNgayHetHanVongDoi;
        private Button btnSave;
        private Button btnCancel;
        private Label lblSerialNumber;
        private Label lblLoaiThietBi;
        private Label lblCanBo;
        private Label lblDonVi;
        private Label lblNgayCap;
        private Label lblNgayHetHan;
        private Label lblNgayHetHanVongDoi;
        private Label lblTrangThai;
        private Label lblGhiChu;

        public PopupThietBiInfo()
        {
            InitializeComponent();
            LoadComboBoxData();
        }

        public PopupThietBiInfo(ThietBi thietBi) : this()
        {
            currentThietBi = thietBi;
            isEditMode = true;
            LoadThietBiInfo();
        }

        private void InitializeComponent()
        {
            this.txtSerialNumber = new TextBox();
            this.txtGhiChu = new TextBox();
            this.cboLoaiThietBi = new ComboBox();
            this.searchableCanBo = new SearchableComboBox();
            this.cboDonVi = new ComboBox();
            this.cboTrangThai = new ComboBox();
            this.dtpNgayCap = new DateTimePicker();
            this.dtpNgayHetHan = new DateTimePicker();
            this.dtpNgayHetHanVongDoi = new DateTimePicker();
            this.btnSave = new Button();
            this.btnCancel = new Button();
            this.lblSerialNumber = new Label();
            this.lblLoaiThietBi = new Label();
            this.lblCanBo = new Label();
            this.lblDonVi = new Label();
            this.lblNgayCap = new Label();
            this.lblNgayHetHan = new Label();
            this.lblNgayHetHanVongDoi = new Label();
            this.lblTrangThai = new Label();
            this.lblGhiChu = new Label();

            this.SuspendLayout();

            // lblSerialNumber
            this.lblSerialNumber.AutoSize = true;
            this.lblSerialNumber.Location = new Point(20, 30);
            this.lblSerialNumber.Name = "lblSerialNumber";
            this.lblSerialNumber.Size = new Size(120, 13);
            this.lblSerialNumber.Text = "Serial Number:";

            // txtSerialNumber
            this.txtSerialNumber.Location = new Point(150, 27);
            this.txtSerialNumber.Name = "txtSerialNumber";
            this.txtSerialNumber.Size = new Size(250, 20);
            this.txtSerialNumber.TabIndex = 1;

            // lblLoaiThietBi
            this.lblLoaiThietBi.AutoSize = true;
            this.lblLoaiThietBi.Location = new Point(20, 65);
            this.lblLoaiThietBi.Name = "lblLoaiThietBi";
            this.lblLoaiThietBi.Size = new Size(120, 13);
            this.lblLoaiThietBi.Text = "Loại thiết bị:";

            // cboLoaiThietBi
            this.cboLoaiThietBi.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboLoaiThietBi.FormattingEnabled = true;
            this.cboLoaiThietBi.Location = new Point(150, 62);
            this.cboLoaiThietBi.Name = "cboLoaiThietBi";
            this.cboLoaiThietBi.Size = new Size(250, 21);
            this.cboLoaiThietBi.TabIndex = 2;

            // lblCanBo
            this.lblCanBo.AutoSize = true;
            this.lblCanBo.Location = new Point(20, 100);
            this.lblCanBo.Name = "lblCanBo";
            this.lblCanBo.Size = new Size(120, 13);
            this.lblCanBo.Text = "Cán bộ:";

            // searchableCanBo
            this.searchableCanBo.Location = new Point(150, 97);
            this.searchableCanBo.Name = "searchableCanBo";
            this.searchableCanBo.Size = new Size(250, 43);
            this.searchableCanBo.TabIndex = 3;

            // lblDonVi
            this.lblDonVi.AutoSize = true;
            this.lblDonVi.Location = new Point(20, 155);
            this.lblDonVi.Name = "lblDonVi";
            this.lblDonVi.Size = new Size(120, 13);
            this.lblDonVi.Text = "Đơn vị:";

            // cboDonVi
            this.cboDonVi.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDonVi.FormattingEnabled = true;
            this.cboDonVi.Location = new Point(150, 152);
            this.cboDonVi.Name = "cboDonVi";
            this.cboDonVi.Size = new Size(250, 21);
            this.cboDonVi.TabIndex = 4;

            // lblNgayCap
            this.lblNgayCap.AutoSize = true;
            this.lblNgayCap.Location = new Point(20, 190);
            this.lblNgayCap.Name = "lblNgayCap";
            this.lblNgayCap.Size = new Size(120, 13);
            this.lblNgayCap.Text = "Ngày cấp:";

            // dtpNgayCap
            this.dtpNgayCap.Format = DateTimePickerFormat.Short;
            this.dtpNgayCap.Location = new Point(150, 187);
            this.dtpNgayCap.Name = "dtpNgayCap";
            this.dtpNgayCap.Size = new Size(250, 20);
            this.dtpNgayCap.TabIndex = 5;
            this.dtpNgayCap.ValueChanged += new EventHandler(this.dtpNgayCap_ValueChanged);

            // lblNgayHetHan
            this.lblNgayHetHan.AutoSize = true;
            this.lblNgayHetHan.Location = new Point(20, 225);
            this.lblNgayHetHan.Name = "lblNgayHetHan";
            this.lblNgayHetHan.Size = new Size(120, 13);
            this.lblNgayHetHan.Text = "Ngày hết hạn:";

            // dtpNgayHetHan
            this.dtpNgayHetHan.Format = DateTimePickerFormat.Short;
            this.dtpNgayHetHan.Location = new Point(150, 222);
            this.dtpNgayHetHan.Name = "dtpNgayHetHan";
            this.dtpNgayHetHan.Size = new Size(250, 20);
            this.dtpNgayHetHan.TabIndex = 6;
            this.dtpNgayHetHan.ShowCheckBox = true;
            this.dtpNgayHetHan.Checked = false;

            // lblNgayHetHanVongDoi
            this.lblNgayHetHanVongDoi.AutoSize = true;
            this.lblNgayHetHanVongDoi.Location = new Point(20, 260);
            this.lblNgayHetHanVongDoi.Name = "lblNgayHetHanVongDoi";
            this.lblNgayHetHanVongDoi.Size = new Size(120, 13);
            this.lblNgayHetHanVongDoi.Text = "Hết hạn vòng đời:";

            // dtpNgayHetHanVongDoi
            this.dtpNgayHetHanVongDoi.Format = DateTimePickerFormat.Short;
            this.dtpNgayHetHanVongDoi.Location = new Point(150, 257);
            this.dtpNgayHetHanVongDoi.Name = "dtpNgayHetHanVongDoi";
            this.dtpNgayHetHanVongDoi.Size = new Size(250, 20);
            this.dtpNgayHetHanVongDoi.TabIndex = 7;
            this.dtpNgayHetHanVongDoi.ShowCheckBox = true;
            this.dtpNgayHetHanVongDoi.Checked = false;

            // lblTrangThai
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new Point(20, 295);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new Size(120, 13);
            this.lblTrangThai.Text = "Trạng thái:";

            // cboTrangThai
            this.cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new Point(150, 292);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new Size(250, 21);
            this.cboTrangThai.TabIndex = 8;

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new Point(20, 330);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new Size(120, 13);
            this.lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            this.txtGhiChu.Location = new Point(150, 327);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new Size(250, 60);
            this.txtGhiChu.TabIndex = 9;

            // btnSave
            this.btnSave.Location = new Point(150, 405);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(300, 405);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // PopupThietBiInfo
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(430, 460);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.cboTrangThai);
            this.Controls.Add(this.lblTrangThai);
            this.Controls.Add(this.dtpNgayHetHanVongDoi);
            this.Controls.Add(this.lblNgayHetHanVongDoi);
            this.Controls.Add(this.dtpNgayHetHan);
            this.Controls.Add(this.lblNgayHetHan);
            this.Controls.Add(this.dtpNgayCap);
            this.Controls.Add(this.lblNgayCap);
            this.Controls.Add(this.cboDonVi);
            this.Controls.Add(this.lblDonVi);
            this.Controls.Add(this.searchableCanBo);
            this.Controls.Add(this.lblCanBo);
            this.Controls.Add(this.cboLoaiThietBi);
            this.Controls.Add(this.lblLoaiThietBi);
            this.Controls.Add(this.txtSerialNumber);
            this.Controls.Add(this.lblSerialNumber);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupThietBiInfo";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Thông tin thiết bị";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadComboBoxData()
        {
            // Load Loại thiết bị
            var loaiThietBis = loaiThietBiDAO.GetAll();
            cboLoaiThietBi.DataSource = loaiThietBis;
            cboLoaiThietBi.DisplayMember = "TenLoai";
            cboLoaiThietBi.ValueMember = "ID";
            cboLoaiThietBi.SelectedIndex = -1;
            cboLoaiThietBi.SelectedIndexChanged += new EventHandler(this.cboLoaiThietBi_SelectedIndexChanged);

            // Load Cán bộ với SearchableComboBox
            var canBos = canBoDAO.GetAll();
            searchableCanBo.DataSource = canBos;
            searchableCanBo.DisplayMember = "HoTen";
            searchableCanBo.ValueMember = "ID";
            searchableCanBo.SelectedIndex = -1;

            // Load Đơn vị
            var donVis = donViDAO.GetAll();
            cboDonVi.DataSource = donVis;
            cboDonVi.DisplayMember = "TenDV";
            cboDonVi.ValueMember = "ID";
            cboDonVi.SelectedIndex = -1;

            // Load Trạng thái
            var trangThais = trangThaiDAO.GetAll();
            cboTrangThai.DataSource = trangThais;
            cboTrangThai.DisplayMember = "trangThai";
            cboTrangThai.ValueMember = "IDTrangThai";
            cboTrangThai.SelectedIndex = -1;
        }

        private void LoadThietBiInfo()
        {
            if (currentThietBi != null)
            {
                txtSerialNumber.Text = currentThietBi.SerialNumber;
                txtGhiChu.Text = currentThietBi.GhiChu;

                if (currentThietBi.LoaiID.HasValue)
                    cboLoaiThietBi.SelectedValue = currentThietBi.LoaiID.Value;

                if (currentThietBi.CanBoID.HasValue)
                    searchableCanBo.SelectedValue = currentThietBi.CanBoID.Value;

                if (currentThietBi.DonViID.HasValue)
                    cboDonVi.SelectedValue = currentThietBi.DonViID.Value;

                if (currentThietBi.TrangThai.HasValue)
                    cboTrangThai.SelectedValue = currentThietBi.TrangThai.Value;

                if (currentThietBi.NgayCap.HasValue)
                    dtpNgayCap.Value = currentThietBi.NgayCap.Value;

                if (currentThietBi.NgayHetHan.HasValue)
                {
                    dtpNgayHetHan.Value = currentThietBi.NgayHetHan.Value;
                    dtpNgayHetHan.Checked = true;
                }

                if (currentThietBi.NgayHetHanVongDoi.HasValue)
                {
                    dtpNgayHetHanVongDoi.Value = currentThietBi.NgayHetHanVongDoi.Value;
                    dtpNgayHetHanVongDoi.Checked = true;
                }
            }
        }

        private void cboLoaiThietBi_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculateExpiryDates();
        }

        private void dtpNgayCap_ValueChanged(object sender, EventArgs e)
        {
            CalculateExpiryDates();
        }

        private void CalculateExpiryDates()
        {
            if (cboLoaiThietBi.SelectedValue != null && cboLoaiThietBi.SelectedValue is int loaiID)
            {
                var loaiThietBi = (cboLoaiThietBi.DataSource as System.Collections.Generic.List<LoaiThietBi>)?.Find(l => l.ID == loaiID);

                if (loaiThietBi != null && loaiThietBi.VongDoiNam.HasValue && loaiThietBi.VongDoiNam.Value > 0)
                {
                    DateTime ngayCap = dtpNgayCap.Value;

                    // Tính ngày hết hạn = Ngày cấp + Vòng đời (năm)
                    DateTime ngayHetHan = ngayCap.AddYears(loaiThietBi.VongDoiNam.Value);
                    dtpNgayHetHan.Value = ngayHetHan;
                    dtpNgayHetHan.Checked = true;

                    // Tính ngày hết hạn vòng đời = Ngày cấp + Vòng đời (năm)
                    DateTime ngayHetHanVongDoi = ngayCap.AddYears(loaiThietBi.VongDoiNam.Value);
                    dtpNgayHetHanVongDoi.Value = ngayHetHanVongDoi;
                    dtpNgayHetHanVongDoi.Checked = true;
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSerialNumber.Text))
            {
                MessageBox.Show("Vui lòng nhập Serial Number.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var thietBi = new ThietBi
            {
                SerialNumber = txtSerialNumber.Text.Trim(),
                LoaiID = cboLoaiThietBi.SelectedValue != null ? (int?)cboLoaiThietBi.SelectedValue : null,
                CanBoID = searchableCanBo.SelectedValue != null ? (int?)searchableCanBo.SelectedValue : null,
                DonViID = cboDonVi.SelectedValue != null ? (int?)cboDonVi.SelectedValue : null,
                TrangThai = cboTrangThai.SelectedValue != null ? (int?)cboTrangThai.SelectedValue : null,
                NgayCap = dtpNgayCap.Value,
                NgayHetHan = dtpNgayHetHan.Checked ? (DateTime?)dtpNgayHetHan.Value : null,
                NgayHetHanVongDoi = dtpNgayHetHanVongDoi.Checked ? (DateTime?)dtpNgayHetHanVongDoi.Value : null,
                GhiChu = txtGhiChu.Text.Trim()
            };

            bool success = false;
            int thietBiID = 0;

            if (isEditMode)
            {
                thietBi.ID = currentThietBi.ID;
                thietBiID = thietBi.ID;
                success = thietBiDAO.Update(thietBi);

                // Lưu lịch sử cập nhật nếu có thay đổi Cán bộ hoặc Đơn vị
                if (success && (currentThietBi.CanBoID != thietBi.CanBoID || currentThietBi.DonViID != thietBi.DonViID))
                {
                    SaveUpdateHistory(thietBi);
                }
            }
            else
            {
                success = thietBiDAO.Insert(thietBi);

                if (success)
                {
                    // Lấy ID của thiết bị vừa thêm
                    var allThietBis = thietBiDAO.GetAll();
                    var newThietBi = allThietBis.Find(tb => tb.SerialNumber == thietBi.SerialNumber);
                    if (newThietBi != null)
                    {
                        thietBiID = newThietBi.ID;
                        SaveInsertHistory(thietBiID, thietBi);
                    }
                }
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

        private void SaveInsertHistory(int thietBiID, ThietBi thietBi)
        {
            try
            {
                // Tìm ID hành động "Thêm mới"
                var hanhDongs = hanhDongDAO.GetAll();
                var themMoiHD = hanhDongs.Find(hd => 
                    hd.name != null && 
                    (hd.name.ToLower().Contains("thêm") || hd.name.ToLower().Contains("them"))
                );

                int hanhDongID = themMoiHD != null ? themMoiHD.ID : 1; // Default to 1 if not found

                var lichSu = new LichSuThietBi
                {
                    ThietBiID = thietBiID,
                    HanhDongID = hanhDongID,
                    CanBoCuID = null,
                    CanBoMoiID = thietBi.CanBoID,
                    DonViCuID = null,
                    DonViMoiID = thietBi.DonViID,
                    ThoiDiem = DateTime.Now,
                    GhiChu = $"Thêm thiết bị mới: {thietBi.SerialNumber}"
                };

                lichSuDAO.Insert(lichSu);
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không block quá trình thêm thiết bị
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lưu lịch sử thêm mới: {ex.Message}");
            }
        }

        private void SaveUpdateHistory(ThietBi thietBi)
        {
            try
            {
                // Tìm ID hành động "Cập nhật"
                var hanhDongs = hanhDongDAO.GetAll();
                var capNhatHD = hanhDongs.Find(hd => 
                    hd.name != null && 
                    (hd.name.ToLower().Contains("cập nhật") || 
                     hd.name.ToLower().Contains("cap nhat") ||
                     hd.name.ToLower().Contains("sửa") ||
                     hd.name.ToLower().Contains("sua"))
                );

                int hanhDongID = capNhatHD != null ? capNhatHD.ID : 2; // Default to 2 if not found

                var lichSu = new LichSuThietBi
                {
                    ThietBiID = thietBi.ID,
                    HanhDongID = hanhDongID,
                    CanBoCuID = currentThietBi.CanBoID,
                    CanBoMoiID = thietBi.CanBoID,
                    DonViCuID = currentThietBi.DonViID,
                    DonViMoiID = thietBi.DonViID,
                    ThoiDiem = DateTime.Now,
                    GhiChu = $"Cập nhật thông tin thiết bị: {thietBi.SerialNumber}"
                };

                lichSuDAO.Insert(lichSu);
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng không block quá trình cập nhật thiết bị
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lưu lịch sử cập nhật: {ex.Message}");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
