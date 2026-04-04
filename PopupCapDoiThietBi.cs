using QuanLyThietBi.DAO;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class PopupCapDoiThietBi : Form
    {
        private ThietBiDAO thietBiDAO = new ThietBiDAO();
        private CanBoDAO canBoDAO = new CanBoDAO();
        private DonViDAO donViDAO = new DonViDAO();
        private HanhDongDAO hanhDongDAO = new HanhDongDAO();
        private LichSuThietBiDAO lichSuDAO = new LichSuThietBiDAO();
        private ThietBi currentThietBi;

        private Label lblThietBi;
        private TextBox txtThietBi;
        private Label lblHanhDong;
        private ComboBox cboHanhDong;
        private Label lblCanBoCu;
        private TextBox txtCanBoCu;
        private Label lblCanBoMoi;
        private SearchableComboBox searchableCanBoMoi;
        private Label lblDonViCu;
        private TextBox txtDonViCu;
        private Label lblDonViMoi;
        private ComboBox cboDonViMoi;
        private Label lblGhiChu;
        private TextBox txtGhiChu;
        private Button btnSave;
        private Button btnCancel;

        public PopupCapDoiThietBi(ThietBi thietBi)
        {
            currentThietBi = thietBi;
            InitializeComponent();
            LoadComboBoxData();
            LoadThietBiInfo();
        }

        private void InitializeComponent()
        {
            this.lblThietBi = new Label();
            this.txtThietBi = new TextBox();
            this.lblHanhDong = new Label();
            this.cboHanhDong = new ComboBox();
            this.lblCanBoCu = new Label();
            this.txtCanBoCu = new TextBox();
            this.lblCanBoMoi = new Label();
            this.searchableCanBoMoi = new SearchableComboBox();
            this.lblDonViCu = new Label();
            this.txtDonViCu = new TextBox();
            this.lblDonViMoi = new Label();
            this.cboDonViMoi = new ComboBox();
            this.lblGhiChu = new Label();
            this.txtGhiChu = new TextBox();
            this.btnSave = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // lblThietBi
            this.lblThietBi.AutoSize = true;
            this.lblThietBi.Location = new Point(20, 30);
            this.lblThietBi.Name = "lblThietBi";
            this.lblThietBi.Size = new Size(100, 13);
            this.lblThietBi.Text = "Thiết bị:";

            // txtThietBi
            this.txtThietBi.Location = new Point(130, 27);
            this.txtThietBi.Name = "txtThietBi";
            this.txtThietBi.Size = new Size(250, 20);
            this.txtThietBi.ReadOnly = true;
            this.txtThietBi.BackColor = System.Drawing.Color.LightGray;
            this.txtThietBi.TabIndex = 0;

            // lblHanhDong
            this.lblHanhDong.AutoSize = true;
            this.lblHanhDong.Location = new Point(20, 65);
            this.lblHanhDong.Name = "lblHanhDong";
            this.lblHanhDong.Size = new Size(100, 13);
            this.lblHanhDong.Text = "Hành động:";

            // cboHanhDong
            this.cboHanhDong.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboHanhDong.FormattingEnabled = true;
            this.cboHanhDong.Location = new Point(130, 62);
            this.cboHanhDong.Name = "cboHanhDong";
            this.cboHanhDong.Size = new Size(250, 21);
            this.cboHanhDong.TabIndex = 1;
            this.cboHanhDong.SelectedIndexChanged += new EventHandler(this.cboHanhDong_SelectedIndexChanged);

            // lblCanBoCu
            this.lblCanBoCu.AutoSize = true;
            this.lblCanBoCu.Location = new Point(20, 100);
            this.lblCanBoCu.Name = "lblCanBoCu";
            this.lblCanBoCu.Size = new Size(100, 13);
            this.lblCanBoCu.Text = "Cán bộ cũ:";

            // txtCanBoCu
            this.txtCanBoCu.Location = new Point(130, 97);
            this.txtCanBoCu.Name = "txtCanBoCu";
            this.txtCanBoCu.Size = new Size(250, 20);
            this.txtCanBoCu.ReadOnly = true;
            this.txtCanBoCu.BackColor = System.Drawing.Color.LightGray;
            this.txtCanBoCu.TabIndex = 2;

            // lblCanBoMoi
            this.lblCanBoMoi.AutoSize = true;
            this.lblCanBoMoi.Location = new Point(20, 135);
            this.lblCanBoMoi.Name = "lblCanBoMoi";
            this.lblCanBoMoi.Size = new Size(100, 13);
            this.lblCanBoMoi.Text = "Cán bộ mới:";

            // searchableCanBoMoi
            this.searchableCanBoMoi.Location = new Point(130, 132);
            this.searchableCanBoMoi.Name = "searchableCanBoMoi";
            this.searchableCanBoMoi.Size = new Size(250, 43);
            this.searchableCanBoMoi.TabIndex = 3;

            // lblDonViCu
            this.lblDonViCu.AutoSize = true;
            this.lblDonViCu.Location = new Point(20, 190);
            this.lblDonViCu.Name = "lblDonViCu";
            this.lblDonViCu.Size = new Size(100, 13);
            this.lblDonViCu.Text = "Đơn vị cũ:";

            // txtDonViCu
            this.txtDonViCu.Location = new Point(130, 187);
            this.txtDonViCu.Name = "txtDonViCu";
            this.txtDonViCu.Size = new Size(250, 20);
            this.txtDonViCu.ReadOnly = true;
            this.txtDonViCu.BackColor = System.Drawing.Color.LightGray;
            this.txtDonViCu.TabIndex = 4;

            // lblDonViMoi
            this.lblDonViMoi.AutoSize = true;
            this.lblDonViMoi.Location = new Point(20, 225);
            this.lblDonViMoi.Name = "lblDonViMoi";
            this.lblDonViMoi.Size = new Size(100, 13);
            this.lblDonViMoi.Text = "Đơn vị mới:";

            // cboDonViMoi
            this.cboDonViMoi.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboDonViMoi.FormattingEnabled = true;
            this.cboDonViMoi.Location = new Point(130, 222);
            this.cboDonViMoi.Name = "cboDonViMoi";
            this.cboDonViMoi.Size = new Size(250, 21);
            this.cboDonViMoi.TabIndex = 5;

            // lblGhiChu
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Location = new Point(20, 260);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new Size(100, 13);
            this.lblGhiChu.Text = "Ghi chú:";

            // txtGhiChu
            this.txtGhiChu.Location = new Point(130, 257);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new Size(250, 60);
            this.txtGhiChu.TabIndex = 6;

            // btnSave
            this.btnSave.Location = new Point(130, 335);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new Size(100, 30);
            this.btnSave.TabIndex = 7;
            this.btnSave.Text = "Lưu";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new EventHandler(this.btnSave_Click);

            // btnCancel
            this.btnCancel.Location = new Point(280, 335);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(100, 30);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new EventHandler(this.btnCancel_Click);

            // PopupCapDoiThietBi
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(410, 390);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.cboDonViMoi);
            this.Controls.Add(this.lblDonViMoi);
            this.Controls.Add(this.txtDonViCu);
            this.Controls.Add(this.lblDonViCu);
            this.Controls.Add(this.searchableCanBoMoi);
            this.Controls.Add(this.lblCanBoMoi);
            this.Controls.Add(this.txtCanBoCu);
            this.Controls.Add(this.lblCanBoCu);
            this.Controls.Add(this.cboHanhDong);
            this.Controls.Add(this.lblHanhDong);
            this.Controls.Add(this.txtThietBi);
            this.Controls.Add(this.lblThietBi);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PopupCapDoiThietBi";
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Cấp/Đổi thiết bị";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private void LoadComboBoxData()
        {
            // Load Hành động
            var hanhDongs = hanhDongDAO.GetAll();
            cboHanhDong.DataSource = hanhDongs;
            cboHanhDong.DisplayMember = "TenHanhDong";
            cboHanhDong.ValueMember = "ID";
            cboHanhDong.SelectedIndex = -1;

            // Load Cán bộ với SearchableComboBox
            var canBos = canBoDAO.GetAll();
            searchableCanBoMoi.DataSource = canBos;
            searchableCanBoMoi.DisplayMember = "HoTen";
            searchableCanBoMoi.ValueMember = "ID";
            searchableCanBoMoi.SelectedIndex = -1;

            // Load Đơn vị
            var donVis = donViDAO.GetAll();
            cboDonViMoi.DataSource = donVis;
            cboDonViMoi.DisplayMember = "TenDV";
            cboDonViMoi.ValueMember = "ID";
            cboDonViMoi.SelectedIndex = -1;
        }

        private void LoadThietBiInfo()
        {
            if (currentThietBi != null)
            {
                txtThietBi.Text = $"{currentThietBi.SerialNumber} - {currentThietBi.TenLoai}";
                txtCanBoCu.Text = currentThietBi.TenCanBo;
                txtDonViCu.Text = currentThietBi.TenDonVi;

                // Pre-select cán bộ mới nếu đã có
                if (currentThietBi.CanBoID.HasValue)
                    searchableCanBoMoi.SelectedValue = currentThietBi.CanBoID.Value;

                // Pre-select đơn vị mới nếu đã có
                if (currentThietBi.DonViID.HasValue)
                    cboDonViMoi.SelectedValue = currentThietBi.DonViID.Value;
            }
        }

        private void cboHanhDong_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Ẩn/hiện các trường dựa trên loại hành động
            if (cboHanhDong.SelectedItem != null)
            {
                var hanhDong = cboHanhDong.SelectedItem as HanhDong;
                if (hanhDong != null)
                {
                    // Nếu là "Cấp phát" hoặc "Thu hồi" -> ẩn cán bộ cũ
                    // Nếu là "Điều chuyển" -> hiện tất cả
                    // Có thể tùy chỉnh logic này dựa trên yêu cầu
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (cboHanhDong.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn hành động.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo lịch sử thiết bị
            var lichSu = new LichSuThietBi
            {
                ThietBiID = currentThietBi.ID,
                HanhDongID = (int)cboHanhDong.SelectedValue,
                CanBoCuID = currentThietBi.CanBoID,
                CanBoMoiID = searchableCanBoMoi.SelectedValue != null ? (int?)searchableCanBoMoi.SelectedValue : null,
                DonViCuID = currentThietBi.DonViID,
                DonViMoiID = cboDonViMoi.SelectedValue != null ? (int?)cboDonViMoi.SelectedValue : null,
                ThoiDiem = DateTime.Now,
                GhiChu = txtGhiChu.Text.Trim()
            };

            // Lưu lịch sử
            bool lichSuSuccess = lichSuDAO.Insert(lichSu);

            if (!lichSuSuccess)
            {
                MessageBox.Show("Lỗi khi lưu lịch sử!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Cập nhật thiết bị với thông tin mới
            currentThietBi.CanBoID = searchableCanBoMoi.SelectedValue != null ? (int?)searchableCanBoMoi.SelectedValue : null;
            currentThietBi.DonViID = cboDonViMoi.SelectedValue != null ? (int?)cboDonViMoi.SelectedValue : null;

            bool thietBiSuccess = thietBiDAO.Update(currentThietBi);

            if (thietBiSuccess)
            {
                MessageBox.Show("Cấp/Đổi thiết bị thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Lỗi khi cập nhật thiết bị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
