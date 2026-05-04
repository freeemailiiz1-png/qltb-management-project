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
    public partial class QLQuyen : BaseForm
    {
        private QuyenDAO quyenDAO = new QuyenDAO();
        private List<Quyen> quyens = new List<Quyen>();

        public QLQuyen()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadQuyen();
        }

        private void LoadQuyen()
        {
            try
            {
                quyens = quyenDAO.GetAll();
                dgvQuyen.DataSource = quyens;

                // Tùy chỉnh hiển thị cột
                if (dgvQuyen.Columns["ID"] != null)
                {
                    dgvQuyen.Columns["ID"].HeaderText = "Mã";
                    dgvQuyen.Columns["ID"].Width = 50;
                }
                if (dgvQuyen.Columns["TenQuyen"] != null)
                {
                    dgvQuyen.Columns["TenQuyen"].HeaderText = "Tên quyền";
                    dgvQuyen.Columns["TenQuyen"].Width = 300;
                }
                if (dgvQuyen.Columns["TenTrangThai"] != null)
                {
                    dgvQuyen.Columns["TenTrangThai"].HeaderText = "Trạng thái";
                    dgvQuyen.Columns["TenTrangThai"].Width = 150;
                }

                // Ẩn cột ID trạng thái
                if (dgvQuyen.Columns["TrangThai"] != null)
                    dgvQuyen.Columns["TrangThai"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách quyền: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            PopupQuyenInfo popup = new PopupQuyenInfo();
            popup.FormClosed += (s, args) => LoadQuyen();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvQuyen.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một quyền để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Lấy đối tượng Quyen từ dòng hiện tại
            Quyen selectedQuyen = dgvQuyen.CurrentRow.DataBoundItem as Quyen;
            if (selectedQuyen == null)
                return;

            // Mở popup và truyền đối tượng vào
            PopupQuyenInfo popup = new PopupQuyenInfo(selectedQuyen);
            popup.FormClosed += (s, args) => LoadQuyen();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            if (dgvQuyen.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một quyền để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedID = Convert.ToInt32(dgvQuyen.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa quyền này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // ✅ QUAN TRỌNG: Truyền UserID từ SessionManager
                bool success = quyenDAO.Delete(selectedID, SessionManager.GetCurrentUserID());
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadQuyen();
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
            var result = quyens
                .Where(q =>
                    (!string.IsNullOrEmpty(q.TenQuyen) && StringHelper.RemoveDiacritics(q.TenQuyen.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(q.TenTrangThai) && StringHelper.RemoveDiacritics(q.TenTrangThai.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //dgvQuyen.DataSource = null;
            dgvQuyen.DataSource = result;
        }

        private void QLQuyen_Load(object sender, EventArgs e)
        {
            LoadQuyen();
        }
    }
}
