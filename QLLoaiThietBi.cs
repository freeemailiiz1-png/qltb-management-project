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
    public partial class QLLoaiThietBi : BaseForm
    {
        private LoaiThietBiDAO loaiThietBiDAO = new LoaiThietBiDAO();
        private List<LoaiThietBi> loaiThietBis = new List<LoaiThietBi>();

        public QLLoaiThietBi()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadLoaiThietBi();
        }

        private void LoadLoaiThietBi()
        {
            try
            {
                loaiThietBis = loaiThietBiDAO.GetAll();
                dgvLoaiThietBi.DataSource = loaiThietBis;

                // Tùy chỉnh hiển thị cột
                if (dgvLoaiThietBi.Columns["ID"] != null)
                {
                    dgvLoaiThietBi.Columns["ID"].HeaderText = "Mã";
                    dgvLoaiThietBi.Columns["ID"].Width = 50;
                }
                if (dgvLoaiThietBi.Columns["TenLoai"] != null)
                {
                    dgvLoaiThietBi.Columns["TenLoai"].HeaderText = "Tên loại thiết bị";
                    dgvLoaiThietBi.Columns["TenLoai"].Width = 250;
                }
                if (dgvLoaiThietBi.Columns["MoTa"] != null)
                {
                    dgvLoaiThietBi.Columns["MoTa"].HeaderText = "Mô tả";
                    dgvLoaiThietBi.Columns["MoTa"].Width = 300;
                }
                if (dgvLoaiThietBi.Columns["VongDoiNam"] != null)
                {
                    dgvLoaiThietBi.Columns["VongDoiNam"].HeaderText = "Vòng đời (năm)";
                    dgvLoaiThietBi.Columns["VongDoiNam"].Width = 120;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại thiết bị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            PopupLoaiThietBiInfo popup = new PopupLoaiThietBiInfo();
            popup.FormClosed += (s, args) => LoadLoaiThietBi();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvLoaiThietBi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một loại thiết bị để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LoaiThietBi selectedLoaiThietBi = dgvLoaiThietBi.CurrentRow.DataBoundItem as LoaiThietBi;
            if (selectedLoaiThietBi == null)
                return;

            PopupLoaiThietBiInfo popup = new PopupLoaiThietBiInfo(selectedLoaiThietBi);
            popup.FormClosed += (s, args) => LoadLoaiThietBi();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            if (dgvLoaiThietBi.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một loại thiết bị để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedID = Convert.ToInt32(dgvLoaiThietBi.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa loại thiết bị này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = loaiThietBiDAO.Delete(selectedID);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadLoaiThietBi();
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
            var result = loaiThietBis
                .Where(l =>
                    (!string.IsNullOrEmpty(l.TenLoai) && StringHelper.RemoveDiacritics(l.TenLoai.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(l.MoTa) && StringHelper.RemoveDiacritics(l.MoTa.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //dgvLoaiThietBi.DataSource = null;
            dgvLoaiThietBi.DataSource = result;
        }

        private void QLLoaiThietBi_Load(object sender, EventArgs e)
        {
            LoadLoaiThietBi();
        }
    }
}
