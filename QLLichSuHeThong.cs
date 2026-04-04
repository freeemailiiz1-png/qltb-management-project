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
    public partial class QLLichSuHeThong : BaseForm
    {
        private LichSuHeThongDAO lichSuDAO = new LichSuHeThongDAO();
        private UserDAO userDAO = new UserDAO();
        private List<LichSuHeThong> lichSuList = new List<LichSuHeThong>();
        private List<LichSuHeThong> allLichSuList = new List<LichSuHeThong>();

        public QLLichSuHeThong()
        {
            InitializeComponent();
            LoadLichSu();
        }

        private void LoadLichSu()
        {
            try
            {
                allLichSuList = lichSuDAO.GetAll();
                lichSuList = new List<LichSuHeThong>(allLichSuList);
                dgvLichSu.DataSource = lichSuList;

                // Tùy chỉnh hiển thị cột
                if (dgvLichSu.Columns["ID"] != null)
                {
                    dgvLichSu.Columns["ID"].HeaderText = "Mã";
                    dgvLichSu.Columns["ID"].Width = 50;
                }
                if (dgvLichSu.Columns["TenUser"] != null)
                {
                    dgvLichSu.Columns["TenUser"].HeaderText = "Người thực hiện";
                    dgvLichSu.Columns["TenUser"].Width = 120;
                }
                if (dgvLichSu.Columns["HanhDong"] != null)
                {
                    dgvLichSu.Columns["HanhDong"].HeaderText = "Hành động";
                    dgvLichSu.Columns["HanhDong"].Width = 100;
                }
                if (dgvLichSu.Columns["BangTacDong"] != null)
                {
                    dgvLichSu.Columns["BangTacDong"].HeaderText = "Bảng tác động";
                    dgvLichSu.Columns["BangTacDong"].Width = 120;
                }
                if (dgvLichSu.Columns["BanGhiID"] != null)
                {
                    dgvLichSu.Columns["BanGhiID"].HeaderText = "ID bản ghi";
                    dgvLichSu.Columns["BanGhiID"].Width = 80;
                }
                if (dgvLichSu.Columns["ThoiDiem"] != null)
                {
                    dgvLichSu.Columns["ThoiDiem"].HeaderText = "Thời điểm";
                    dgvLichSu.Columns["ThoiDiem"].Width = 130;
                    dgvLichSu.Columns["ThoiDiem"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                }
                if (dgvLichSu.Columns["NoiDungCu"] != null)
                {
                    dgvLichSu.Columns["NoiDungCu"].HeaderText = "Nội dung cũ";
                    dgvLichSu.Columns["NoiDungCu"].Width = 200;
                }
                if (dgvLichSu.Columns["NoiDungMoi"] != null)
                {
                    dgvLichSu.Columns["NoiDungMoi"].HeaderText = "Nội dung mới";
                    dgvLichSu.Columns["NoiDungMoi"].Width = 200;
                }

                // Ẩn cột UserID
                if (dgvLichSu.Columns["UserID"] != null)
                    dgvLichSu.Columns["UserID"].Visible = false;

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
            cboUser.SelectedIndex = -1;
            cboBang.SelectedIndex = -1;
            cboHanhDong.SelectedIndex = -1;
            dtpTuNgay.Value = DateTime.Now.AddMonths(-1);
            dtpDenNgay.Value = DateTime.Now;
            chkLoc.Checked = false;
            LoadLichSu();
        }

        private void ApplyFilters()
        {
            try
            {
                var filtered = new List<LichSuHeThong>(allLichSuList);

                // Filter theo từ khóa tìm kiếm
                string keyword = StringHelper.RemoveDiacritics(txtSearch.Text.Trim().ToLower());
                if (!string.IsNullOrEmpty(keyword))
                {
                    filtered = filtered.Where(ls =>
                        (!string.IsNullOrEmpty(ls.TenUser) && StringHelper.RemoveDiacritics(ls.TenUser.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.HanhDong) && StringHelper.RemoveDiacritics(ls.HanhDong.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.BangTacDong) && StringHelper.RemoveDiacritics(ls.BangTacDong.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.NoiDungCu) && StringHelper.RemoveDiacritics(ls.NoiDungCu.ToLower()).Contains(keyword)) ||
                        (!string.IsNullOrEmpty(ls.NoiDungMoi) && StringHelper.RemoveDiacritics(ls.NoiDungMoi.ToLower()).Contains(keyword))
                    ).ToList();
                }

                // Filter theo user
                if (cboUser.SelectedValue != null && cboUser.SelectedValue is int userID)
                {
                    filtered = filtered.Where(ls => ls.UserID == userID).ToList();
                }

                // Filter theo bảng
                if (cboBang.SelectedItem != null && !string.IsNullOrEmpty(cboBang.SelectedItem.ToString()))
                {
                    string bang = cboBang.SelectedItem.ToString();
                    filtered = filtered.Where(ls => ls.BangTacDong == bang).ToList();
                }

                // Filter theo hành động
                if (cboHanhDong.SelectedItem != null && !string.IsNullOrEmpty(cboHanhDong.SelectedItem.ToString()))
                {
                    string hanhDong = cboHanhDong.SelectedItem.ToString();
                    filtered = filtered.Where(ls => ls.HanhDong == hanhDong).ToList();
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

        private void QLLichSuHeThong_Load(object sender, EventArgs e)
        {
            try
            {
                // Load danh sách user
                var users = userDAO.GetAll();
                cboUser.DataSource = users;
                cboUser.DisplayMember = "Username";
                cboUser.ValueMember = "ID";
                cboUser.SelectedIndex = -1;

                // Load danh sách bảng (có thể lấy từ database hoặc hardcode)
                var bangs = new List<string>
                {
                    "tblThietBi",
                    "tblCanBo",
                    "tblDonVi",
                    "tblLoaiThietBi",
                    "tblUser",
                    "tblQuyen",
                    "tblHanhDong",
                    "tblTrangThai"
                };
                cboBang.DataSource = bangs;
                cboBang.SelectedIndex = -1;

                // Load danh sách hành động
                var hanhDongs = new List<string>
                {
                    "Thêm",
                    "Sửa",
                    "Xóa",
                    "Cập nhật",
                    "Thêm mới",
                    "Đăng nhập",
                    "Đăng xuất"
                };
                cboHanhDong.DataSource = hanhDongs;
                cboHanhDong.SelectedIndex = -1;

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

        private void dgvLichSu_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                LichSuHeThong selected = dgvLichSu.CurrentRow.DataBoundItem as LichSuHeThong;
                if (selected != null)
                {
                    ShowDetailDialog(selected);
                }
            }
        }

        private void ShowDetailDialog(LichSuHeThong lichSu)
        {
            string detail = $"CHI TIẾT LỊCH SỮ\n\n";
            detail += $"ID: {lichSu.ID}\n";
            detail += $"Người thực hiện: {lichSu.TenUser}\n";
            detail += $"Hành động: {lichSu.HanhDong}\n";
            detail += $"Bảng tác động: {lichSu.BangTacDong}\n";
            detail += $"ID bản ghi: {lichSu.BanGhiID}\n";
            detail += $"Thời điểm: {lichSu.ThoiDiem:dd/MM/yyyy HH:mm:ss}\n\n";
            detail += $"Nội dung cũ:\n{lichSu.NoiDungCu}\n\n";
            detail += $"Nội dung mới:\n{lichSu.NoiDungMoi}";

            MessageBox.Show(detail, "Chi tiết lịch sử", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
