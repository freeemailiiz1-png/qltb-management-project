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
    public partial class QLHanhDong : BaseForm
    {
        private HanhDongDAO hanhDongDAO = new HanhDongDAO();
        private List<HanhDong> hanhDongs = new List<HanhDong>();

        public QLHanhDong()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadHanhDong();
        }

        private void LoadHanhDong()
        {
            try
            {
                hanhDongs = hanhDongDAO.GetAll();
                dgvHanhDong.DataSource = hanhDongs;

                // Tùy chỉnh hiển thị cột
                if (dgvHanhDong.Columns["ID"] != null)
                {
                    dgvHanhDong.Columns["ID"].HeaderText = "Mã";
                    dgvHanhDong.Columns["ID"].Width = 50;
                }
                if (dgvHanhDong.Columns["name"] != null)
                {
                    dgvHanhDong.Columns["name"].HeaderText = "Tên hành động";
                    dgvHanhDong.Columns["name"].Width = 300;
                }
                if (dgvHanhDong.Columns["TenHanhDong"] != null)
                {
                    dgvHanhDong.Columns["TenHanhDong"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hành động: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            PopupHanhDongInfo popup = new PopupHanhDongInfo();
            popup.FormClosed += (s, args) => LoadHanhDong();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvHanhDong.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một hành động để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            HanhDong selectedHanhDong = dgvHanhDong.CurrentRow.DataBoundItem as HanhDong;
            if (selectedHanhDong == null)
                return;

            PopupHanhDongInfo popup = new PopupHanhDongInfo(selectedHanhDong);
            popup.FormClosed += (s, args) => LoadHanhDong();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            if (dgvHanhDong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một hành động để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedID = Convert.ToInt32(dgvHanhDong.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hành động này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = hanhDongDAO.Delete(selectedID);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHanhDong();
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
            var result = hanhDongs
                .Where(hd =>
                    (!string.IsNullOrEmpty(hd.name) && StringHelper.RemoveDiacritics(hd.name.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvHanhDong.DataSource = null;
            dgvHanhDong.DataSource = result;
        }

        private void QLHanhDong_Load(object sender, EventArgs e)
        {
            LoadHanhDong();
        }
    }
}
