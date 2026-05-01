using QuanLyThietBi.Common;
using QuanLyThietBi.DAO;
using QuanLyThietBi.DTO;
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
    public partial class QLCapDonVi : BaseForm
    {
        private UcCrudButtons ucCrudButtons;
        private CapDonViDAO capDonViDAO = new CapDonViDAO();
        private List<CapDonVi> capDonVis = new List<CapDonVi>();

        public QLCapDonVi()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadCapDonVi();

        }

        private void LoadCapDonVi()
        {
            try
            {
                capDonVis = capDonViDAO.GetAll();
                dgvCapDonVi.DataSource = capDonVis;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách cấp đơn vị: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            // TODO: Implement add logic for TrangThai
            PopupCapDonViInfo popup = new PopupCapDonViInfo();
            popup.FormClosed += (s, args) => LoadCapDonVi();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvCapDonVi.CurrentRow == null)
                return;

            // Lấy đối tượng CapDonVi từ dòng hiện tại
            CapDonVi selectedCapDonVi = dgvCapDonVi.CurrentRow.DataBoundItem as CapDonVi;
            if (selectedCapDonVi == null)
                return;

            // Mở popup và truyền đối tượng vào
            PopupCapDonViInfo popup = new PopupCapDonViInfo(selectedCapDonVi);
            popup.FormClosed += (s, args) => LoadCapDonVi();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
        
            int selectedID = Convert.ToInt32(dgvCapDonVi.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa cấp đơn vị này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                // ✅ QUAN TRỌNG: Truyền UserID từ SessionManager
                bool success = capDonViDAO.Delete(selectedID, SessionManager.GetCurrentUserID());
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCapDonVi();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ucCrudButtons1_SearchClicked(object sender, EventArgs e)
        {
            // TODO: Implement search logic for TrangThai
            string keyword = StringHelper.RemoveDiacritics(txtSearch.Text.Trim().ToLower());
            var result = capDonVis
                .Where(c =>
                    (!string.IsNullOrEmpty(c.TenCapDV) && StringHelper.RemoveDiacritics(c.TenCapDV.ToLower()).Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            dgvCapDonVi.DataSource = null;
            dgvCapDonVi.DataSource = result;
        }
        private void QLCapDonVi_Load(object sender, EventArgs e)
        {
            LoadCapDonVi();
        }
    }
}
