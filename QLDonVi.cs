using QuanLyThietBi.Common;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyThietBi
{
    public partial class QLDonVi : BaseForm
    {
        private DonViDAO donViDAO = new DonViDAO();
        private List<DonVi> donVis = new List<DonVi>();

        public QLDonVi()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadDonVi();
        }

        private void LoadDonVi()
        {

            try
            {
                donVis = donViDAO.GetAll();
                dgvDonVi.DataSource = donVis;

                // Tùy chỉnh hiển thị cột
                if (dgvDonVi.Columns["ID"] != null)
                {
                    dgvDonVi.Columns["ID"].HeaderText = "Mã";
                    dgvDonVi.Columns["ID"].Width = 50;
                }
                if (dgvDonVi.Columns["MaDV"] != null)
                {
                    dgvDonVi.Columns["MaDV"].HeaderText = "Mã đơn vị";
                    dgvDonVi.Columns["MaDV"].Width = 100;
                }
                if (dgvDonVi.Columns["TenDV"] != null)
                {
                    dgvDonVi.Columns["TenDV"].HeaderText = "Tên đơn vị";
                    dgvDonVi.Columns["TenDV"].Width = 200;
                }
                if (dgvDonVi.Columns["TenCapDV"] != null)
                {
                    dgvDonVi.Columns["TenCapDV"].HeaderText = "Cấp đơn vị";
                    dgvDonVi.Columns["TenCapDV"].Width = 120;
                }
                if (dgvDonVi.Columns["TenDonViCha"] != null)
                {
                    dgvDonVi.Columns["TenDonViCha"].HeaderText = "Đơn vị cha";
                    dgvDonVi.Columns["TenDonViCha"].Width = 150;
                }
                if (dgvDonVi.Columns["TenTrangThai"] != null)
                {
                    dgvDonVi.Columns["TenTrangThai"].HeaderText = "Trạng thái";
                    dgvDonVi.Columns["TenTrangThai"].Width = 100;
                }
                if (dgvDonVi.Columns["NgayTao"] != null)
                {
                    dgvDonVi.Columns["NgayTao"].HeaderText = "Ngày tạo";
                    dgvDonVi.Columns["NgayTao"].Width = 120;
                    dgvDonVi.Columns["NgayTao"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                
                // Ẩn các cột ID không cần thiết
                if (dgvDonVi.Columns["CapDV"] != null)
                    dgvDonVi.Columns["CapDV"].Visible = false;
                if (dgvDonVi.Columns["DonViChaID"] != null)
                    dgvDonVi.Columns["DonViChaID"].Visible = false;
                if (dgvDonVi.Columns["TrangThai"] != null)
                    dgvDonVi.Columns["TrangThai"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách đơn vị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            PopupDonViInfo popup = new PopupDonViInfo();
            popup.FormClosed += (s, args) => LoadDonVi();
            popup.ShowDialog();
        }


        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvDonVi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một đơn vị để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy đối tượng DonVi từ dòng hiện tại
            DonVi selectedDonVi = dgvDonVi.CurrentRow.DataBoundItem as DonVi;
            if (selectedDonVi == null)
                return;

            // Mở popup và truyền đối tượng vào
            PopupDonViInfo popup = new PopupDonViInfo(selectedDonVi);
            popup.FormClosed += (s, args) => LoadDonVi();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            if (dgvDonVi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một đơn vị để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedID = Convert.ToInt32(dgvDonVi.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn vị này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = donViDAO.Delete(selectedID);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDonVi();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ucCrudButtons1_SearchClicked(object sender, EventArgs e)
        {
            string keyword = StringHelper.RemoveDiacritics(txtSearch.Text.Trim().ToLower());
            var result = donVis
                .Where(d =>
                    (!string.IsNullOrEmpty(d.MaDV) && StringHelper.RemoveDiacritics(d.MaDV.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(d.TenDV) && StringHelper.RemoveDiacritics(d.TenDV.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(d.TenCapDV) && StringHelper.RemoveDiacritics(d.TenCapDV.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(d.TenDonViCha) && StringHelper.RemoveDiacritics(d.TenDonViCha.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(d.TenTrangThai) && StringHelper.RemoveDiacritics(d.TenTrangThai.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvDonVi.DataSource = null;
            dgvDonVi.DataSource = result;
        }

        private void QLDonVi_Load(object sender, EventArgs e)
        {
            LoadDonVi();
        }
    }
}
