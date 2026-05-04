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
    public partial class QLThietBi : BaseForm
    {
        private ThietBiDAO thietBiDAO = new ThietBiDAO();
        private LichSuThietBiDAO lichSuDAO = new LichSuThietBiDAO();
        private HanhDongDAO hanhDongDAO = new HanhDongDAO();
        private List<ThietBi> thietBis = new List<ThietBi>();

        public QLThietBi()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadThietBi();
        }

        private void LoadThietBi()
        {
            try
            {
                thietBis = thietBiDAO.GetAll();
                dgvThietBi.DataSource = thietBis;

                // Tùy chỉnh hiển thị cột
                if (dgvThietBi.Columns["ID"] != null)
                {
                    dgvThietBi.Columns["ID"].HeaderText = "Mã";
                    dgvThietBi.Columns["ID"].Width = 50;
                }
                if (dgvThietBi.Columns["SerialNumber"] != null)
                {
                    dgvThietBi.Columns["SerialNumber"].HeaderText = "Serial Number";
                    dgvThietBi.Columns["SerialNumber"].Width = 150;
                }
                if (dgvThietBi.Columns["TenLoai"] != null)
                {
                    dgvThietBi.Columns["TenLoai"].HeaderText = "Loại thiết bị";
                    dgvThietBi.Columns["TenLoai"].Width = 150;
                }
                if (dgvThietBi.Columns["TenCanBo"] != null)
                {
                    dgvThietBi.Columns["TenCanBo"].HeaderText = "Cán bộ";
                    dgvThietBi.Columns["TenCanBo"].Width = 150;
                }
                if (dgvThietBi.Columns["TenDonVi"] != null)
                {
                    dgvThietBi.Columns["TenDonVi"].HeaderText = "Đơn vị";
                    dgvThietBi.Columns["TenDonVi"].Width = 150;
                }
                if (dgvThietBi.Columns["NgayCap"] != null)
                {
                    dgvThietBi.Columns["NgayCap"].HeaderText = "Ngày cấp";
                    dgvThietBi.Columns["NgayCap"].Width = 100;
                    dgvThietBi.Columns["NgayCap"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvThietBi.Columns["NgayHetHan"] != null)
                {
                    dgvThietBi.Columns["NgayHetHan"].HeaderText = "Ngày hết hạn";
                    dgvThietBi.Columns["NgayHetHan"].Width = 100;
                    dgvThietBi.Columns["NgayHetHan"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvThietBi.Columns["NgayHetHanVongDoi"] != null)
                {
                    dgvThietBi.Columns["NgayHetHanVongDoi"].HeaderText = "Hết hạn vòng đời";
                    dgvThietBi.Columns["NgayHetHanVongDoi"].Width = 120;
                    dgvThietBi.Columns["NgayHetHanVongDoi"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvThietBi.Columns["TenTrangThai"] != null)
                {
                    dgvThietBi.Columns["TenTrangThai"].HeaderText = "Trạng thái";
                    dgvThietBi.Columns["TenTrangThai"].Width = 100;
                }
                if (dgvThietBi.Columns["GhiChu"] != null)
                {
                    dgvThietBi.Columns["GhiChu"].HeaderText = "Ghi chú";
                    dgvThietBi.Columns["GhiChu"].Width = 200;
                }

                // Ẩn các cột ID
                if (dgvThietBi.Columns["LoaiID"] != null)
                    dgvThietBi.Columns["LoaiID"].Visible = false;
                if (dgvThietBi.Columns["CanBoID"] != null)
                    dgvThietBi.Columns["CanBoID"].Visible = false;
                if (dgvThietBi.Columns["DonViID"] != null)
                    dgvThietBi.Columns["DonViID"].Visible = false;
                if (dgvThietBi.Columns["TrangThai"] != null)
                    dgvThietBi.Columns["TrangThai"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            PopupThietBiInfo popup = new PopupThietBiInfo();
            popup.FormClosed += (s, args) => LoadThietBi();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvThietBi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ThietBi selectedThietBi = dgvThietBi.CurrentRow.DataBoundItem as ThietBi;
            if (selectedThietBi == null)
                return;

            PopupThietBiInfo popup = new PopupThietBiInfo(selectedThietBi);
            popup.FormClosed += (s, args) => LoadThietBi();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            if (dgvThietBi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedID = Convert.ToInt32(dgvThietBi.SelectedRows[0].Cells["ID"].Value);
            ThietBi selectedThietBi = dgvThietBi.CurrentRow.DataBoundItem as ThietBi;

            if (selectedThietBi == null)
            {
                MessageBox.Show("Không thể lấy thông tin thiết bị.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa thiết bị '{selectedThietBi.SerialNumber}' không?\n\nLưu ý: Lịch sử của thiết bị này sẽ được lưu trữ.", 
                "Xác nhận xóa", 
                MessageBoxButtons.YesNo, 
                MessageBoxIcon.Warning
            );

            if (result == DialogResult.Yes)
            {
                // Lưu lịch sử trước khi xóa
                SaveDeleteHistory(selectedThietBi);

                // Thực hiện xóa thiết bị - QUAN TRỌNG: Truyền UserID từ SessionManager
                bool success = thietBiDAO.Delete(selectedID, SessionManager.GetCurrentUserID());
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadThietBi();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SaveDeleteHistory(ThietBi thietBi)
        {
            try
            {
                // Tìm ID hành động "Xóa"
                var hanhDongs = hanhDongDAO.GetAll();
                var xoaHD = hanhDongs.Find(hd => 
                    hd.name != null && 
                    (hd.name.ToLower().Contains("xóa") || 
                     hd.name.ToLower().Contains("xoa") ||
                     hd.name.ToLower().Contains("thu hồi") ||
                     hd.name.ToLower().Contains("thu hoi"))
                );

                int hanhDongID = xoaHD != null ? xoaHD.ID : 3; // Default to 3 if not found

                var lichSu = new LichSuThietBi
                {
                    ThietBiID = thietBi.ID,
                    HanhDongID = hanhDongID,
                    CanBoCuID = thietBi.CanBoID,
                    CanBoMoiID = null,
                    DonViCuID = thietBi.DonViID,
                    DonViMoiID = null,
                    ThoiDiem = DateTime.Now,
                    GhiChu = $"Xóa thiết bị khỏi hệ thống: {thietBi.SerialNumber}"
                };

                lichSuDAO.Insert(lichSu);
            }
            catch (Exception ex)
            {
                // Log lỗi nhưng vẫn tiếp tục xóa thiết bị
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lưu lịch sử xóa: {ex.Message}");
                MessageBox.Show("Lưu lịch sử thất bại nhưng sẽ tiếp tục xóa thiết bị.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ucCrudButtons1_SearchClicked(object sender, EventArgs e)
        {
            string keyword = StringHelper.RemoveDiacritics(txtSearch.Text.Trim().ToLower());
            var result = thietBis
                .Where(tb =>
                    (!string.IsNullOrEmpty(tb.SerialNumber) && StringHelper.RemoveDiacritics(tb.SerialNumber.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(tb.TenLoai) && StringHelper.RemoveDiacritics(tb.TenLoai.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(tb.TenCanBo) && StringHelper.RemoveDiacritics(tb.TenCanBo.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(tb.TenDonVi) && StringHelper.RemoveDiacritics(tb.TenDonVi.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(tb.TenTrangThai) && StringHelper.RemoveDiacritics(tb.TenTrangThai.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(tb.GhiChu) && StringHelper.RemoveDiacritics(tb.GhiChu.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //dgvThietBi.DataSource = null;
            dgvThietBi.DataSource = result;
        }

        private void btnCapDoi_Click(object sender, EventArgs e)
        {
            if (dgvThietBi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một thiết bị để cấp/đổi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ThietBi selectedThietBi = dgvThietBi.CurrentRow.DataBoundItem as ThietBi;
            if (selectedThietBi == null)
                return;

            PopupCapDoiThietBi popup = new PopupCapDoiThietBi(selectedThietBi);
            popup.FormClosed += (s, args) => LoadThietBi();
            popup.ShowDialog();
        }

        private void QLThietBi_Load(object sender, EventArgs e)
        {
            LoadThietBi();
        }
    }
}
