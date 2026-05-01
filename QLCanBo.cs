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
    public partial class QLCanBo : BaseForm
    {
        private CanBoDAO canBoDAO = new CanBoDAO();
        private List<CanBo> canBos = new List<CanBo>();

        public QLCanBo()
        {
            InitializeComponent();
            ucCrudButtons1.AddClicked += ucCrudButtons1_AddClicked;
            ucCrudButtons1.EditClicked += ucCrudButtons1_EditClicked;
            ucCrudButtons1.DeleteClicked += ucCrudButtons1_DeleteClicked;
            ucCrudButtons1.SearchClicked += ucCrudButtons1_SearchClicked;
            LoadCanBo();
        }

        private void LoadCanBo()
        {
            try
            {
                canBos = canBoDAO.GetAll();
                dgvCanBo.DataSource = canBos;

                // Tùy chỉnh hiển thị cột
                if (dgvCanBo.Columns["ID"] != null)
                {
                    dgvCanBo.Columns["ID"].HeaderText = "Mã";
                    dgvCanBo.Columns["ID"].Width = 50;
                }
                if (dgvCanBo.Columns["HoTen"] != null)
                {
                    dgvCanBo.Columns["HoTen"].HeaderText = "Họ tên";
                    dgvCanBo.Columns["HoTen"].Width = 200;
                }
                if (dgvCanBo.Columns["CCCD"] != null)
                {
                    dgvCanBo.Columns["CCCD"].HeaderText = "CCCD";
                    dgvCanBo.Columns["CCCD"].Width = 120;
                }
                if (dgvCanBo.Columns["NgaySinh"] != null)
                {
                    dgvCanBo.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgvCanBo.Columns["NgaySinh"].Width = 100;
                    dgvCanBo.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                if (dgvCanBo.Columns["Email"] != null)
                {
                    dgvCanBo.Columns["Email"].HeaderText = "Email";
                    dgvCanBo.Columns["Email"].Width = 200;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách cán bộ: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ucCrudButtons1_AddClicked(object sender, EventArgs e)
        {
            PopupCanBoInfo popup = new PopupCanBoInfo();
            popup.FormClosed += (s, args) => LoadCanBo();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_EditClicked(object sender, EventArgs e)
        {
            if (dgvCanBo.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một cán bộ để sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            CanBo selectedCanBo = dgvCanBo.CurrentRow.DataBoundItem as CanBo;
            if (selectedCanBo == null)
                return;

            PopupCanBoInfo popup = new PopupCanBoInfo(selectedCanBo);
            popup.FormClosed += (s, args) => LoadCanBo();
            popup.ShowDialog();
        }

        private void ucCrudButtons1_DeleteClicked(object sender, EventArgs e)
        {
            if (dgvCanBo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn một cán bộ để xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int selectedID = Convert.ToInt32(dgvCanBo.SelectedRows[0].Cells["ID"].Value);

            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa cán bộ này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                bool success = canBoDAO.Delete(selectedID);
                if (success)
                {
                    MessageBox.Show("Xóa thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCanBo();
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
            var result = canBos
                .Where(cb =>
                    (!string.IsNullOrEmpty(cb.HoTen) && StringHelper.RemoveDiacritics(cb.HoTen.ToLower()).Contains(keyword)) ||
                    (!string.IsNullOrEmpty(cb.CCCD) && cb.CCCD.ToLower().Contains(keyword)) ||
                    (!string.IsNullOrEmpty(cb.Email) && cb.Email.ToLower().Contains(keyword))
                )
                .ToList();

            if (result.Count == 0)
            {
                MessageBox.Show("Không có kết quả", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            //dgvCanBo.DataSource = null;
            dgvCanBo.DataSource = result;
        }

        private void QLCanBo_Load(object sender, EventArgs e)
        {
            LoadCanBo();
        }
    }
}
