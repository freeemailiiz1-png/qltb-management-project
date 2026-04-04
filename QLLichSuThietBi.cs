using System;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using QuanLyThietBi.Common;
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
    public partial class QLLichSuThietBi : BaseForm
    {
        private LichSuThietBiDAO lichSuDAO = new LichSuThietBiDAO();
        private ThietBiDAO thietBiDAO = new ThietBiDAO();
        private List<LichSuThietBi> lichSuList = new List<LichSuThietBi>();
        private List<LichSuThietBi> allLichSuList = new List<LichSuThietBi>();

        public QLLichSuThietBi()
        {
            InitializeComponent();
            LoadLichSu();
        }

        private void LoadLichSu()
        {
            try
            {
                allLichSuList = lichSuDAO.GetAll();
                lichSuList = new List<LichSuThietBi>(allLichSuList);
                dgvLichSu.DataSource = lichSuList;

                // Tùy chỉnh hiển thị cột
                if (dgvLichSu.Columns["ID"] != null)
                {
                    dgvLichSu.Columns["ID"].HeaderText = "Mã";
                    dgvLichSu.Columns["ID"].Width = 50;
                }
                if (dgvLichSu.Columns["SerialNumber"] != null)
                {
                    dgvLichSu.Columns["SerialNumber"].HeaderText = "Thiết bị";
                    dgvLichSu.Columns["SerialNumber"].Width = 120;
                }
                if (dgvLichSu.Columns["TenHanhDong"] != null)
                {
                    dgvLichSu.Columns["TenHanhDong"].HeaderText = "Hành động";
                    dgvLichSu.Columns["TenHanhDong"].Width = 120;
                }
                if (dgvLichSu.Columns["TenCanBoCu"] != null)
                {
                    dgvLichSu.Columns["TenCanBoCu"].HeaderText = "Cán bộ cũ";
                    dgvLichSu.Columns["TenCanBoCu"].Width = 150;
                }
                if (dgvLichSu.Columns["TenCanBoMoi"] != null)
                {
                    dgvLichSu.Columns["TenCanBoMoi"].HeaderText = "Cán bộ mới";
                    dgvLichSu.Columns["TenCanBoMoi"].Width = 150;
                }
                if (dgvLichSu.Columns["TenDonViCu"] != null)
                {
                    dgvLichSu.Columns["TenDonViCu"].HeaderText = "Đơn vị cũ";
                    dgvLichSu.Columns["TenDonViCu"].Width = 150;
                }
                if (dgvLichSu.Columns["TenDonViMoi"] != null)
                {
                    dgvLichSu.Columns["TenDonViMoi"].HeaderText = "Đơn vị mới";
                    dgvLichSu.Columns["TenDonViMoi"].Width = 150;
                }
                if (dgvLichSu.Columns["ThoiDiem"] != null)
                {
                    dgvLichSu.Columns["ThoiDiem"].HeaderText = "Thời điểm";
                    dgvLichSu.Columns["ThoiDiem"].Width = 130;
                    dgvLichSu.Columns["ThoiDiem"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }
                if (dgvLichSu.Columns["GhiChu"] != null)
                {
                    dgvLichSu.Columns["GhiChu"].HeaderText = "Ghi chú";
                    dgvLichSu.Columns["GhiChu"].Width = 200;
                }

                // Ẩn các cột ID
                if (dgvLichSu.Columns["ThietBiID"] != null)
                    dgvLichSu.Columns["ThietBiID"].Visible = false;
                if (dgvLichSu.Columns["HanhDongID"] != null)
                    dgvLichSu.Columns["HanhDongID"].Visible = false;
                if (dgvLichSu.Columns["CanBoCuID"] != null)
                    dgvLichSu.Columns["CanBoCuID"].Visible = false;
                if (dgvLichSu.Columns["CanBoMoiID"] != null)
                    dgvLichSu.Columns["CanBoMoiID"].Visible = false;
                if (dgvLichSu.Columns["DonViCuID"] != null)
                    dgvLichSu.Columns["DonViCuID"].Visible = false;
                if (dgvLichSu.Columns["DonViMoiID"] != null)
                    dgvLichSu.Columns["DonViMoiID"].Visible = false;

                lblTongSo.Text = $"Tổng số: {lichSuList.Count} bản ghi";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lịch sử: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ApplyFilters();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            cboThietBi.SelectedIndex = -1;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            chkLoc.Checked = false;
            LoadLichSu();
        }

        private void ApplyFilters()
        {
            try
            {
                var filtered = new List<LichSuThietBi>(allLichSuList);

                // Filter theo từ khóa tìm kiếm
                string keyword = StringHelper.RemoveDiacritics(txtSearch.Text.Trim().ToLower());
                if (!string.IsNullOrEmpty(keyword))
                {
                    filtered = filtered.Where(ls =>
                        (!string.IsNullOrEmpty(ls.SerialNumber) && StringHelper.RemoveDiacritics(ls.SerialNumber.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.TenHanhDong) && StringHelper.RemoveDiacritics(ls.TenHanhDong.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.TenCanBoCu) && StringHelper.RemoveDiacritics(ls.TenCanBoCu.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.TenCanBoMoi) && StringHelper.RemoveDiacritics(ls.TenCanBoMoi.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.TenDonViCu) && StringHelper.RemoveDiacritics(ls.TenDonViCu.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.TenDonViMoi) && StringHelper.RemoveDiacritics(ls.TenDonViMoi.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.GhiChu) && StringHelper.RemoveDiacritics(ls.GhiChu.ToLower()).Contains(keyword))
                    ).ToList();
                }

                // Filter theo thiết bị
                if (cboThietBi.SelectedValue != null && cboThietBi.SelectedValue is int thietBiID)
                {
                    filtered = filtered.Where(ls => ls.ThietBiID == thietBiID).ToList();
                }

                // Filter theo ngày
                if (chkLoc.Checked)
                {
                    DateTime tuNgay = dtpTuNgay.Value.Date;
                    DateTime denNgay = dtpDenNgay.Value.Date.AddDays(1).AddSeconds(-1);
                    filtered = filtered.Where(ls => ls.ThoiDiem.HasValue && ls.ThoiDiem.Value >= tuNgay && ls.ThoiDiem.Value <= denNgay).ToList();
                }

                lichSuList = filtered;
                dgvLichSu.DataSource = null;
                dgvLichSu.DataSource = lichSuList;

                lblTongSo.Text = $"Tổng số: {lichSuList.Count} bản ghi";

                if (lichSuList.Count == 0)
                {
                    MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lọc dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void QLLichSuThietBi_Load(object sender, EventArgs e)
        {
            // Load danh sách thiết bị vào ComboBox
            try
            {
                var thietBis = thietBiDAO.GetAll();
                cboThietBi.DataSource = thietBis;
                cboThietBi.DisplayMember = "SerialNumber";
                cboThietBi.ValueMember = "ID";
                cboThietBi.SelectedIndex = -1;

                // Set ngày mặc định
                dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
                dtpDenNgay.Value = DateTime.Now;
                chkLoc.Checked = false;

                LoadLichSu();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi tạo: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chkLoc_CheckedChanged(object sender, EventArgs e)
        {
            dtpTuNgay.Enabled = chkLoc.Checked;
            dtpDenNgay.Enabled = chkLoc.Checked;

            if (chkLoc.Checked)
            {
                ApplyFilters();
            }
        }
    }
}
