namespace QuanLyThietBi
{
    partial class QLLoaiThietBi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucCrudButtons1 = new QuanLyThietBi.UcCrudButtons();
            this.dgvLoaiThietBi = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiThietBi)).BeginInit();
            this.SuspendLayout();
            // 
            // ucCrudButtons1
            // 
            this.ucCrudButtons1.Location = new System.Drawing.Point(12, 67);
            this.ucCrudButtons1.Name = "ucCrudButtons1";
            this.ucCrudButtons1.Size = new System.Drawing.Size(360, 30);
            this.ucCrudButtons1.TabIndex = 1;
            // 
            // dgvLoaiThietBi
            // 
            this.dgvLoaiThietBi.AllowUserToAddRows = false;
            this.dgvLoaiThietBi.AllowUserToDeleteRows = false;
            this.dgvLoaiThietBi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoaiThietBi.Location = new System.Drawing.Point(12, 103);
            this.dgvLoaiThietBi.Name = "dgvLoaiThietBi";
            this.dgvLoaiThietBi.ReadOnly = true;
            this.dgvLoaiThietBi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoaiThietBi.Size = new System.Drawing.Size(776, 335);
            this.dgvLoaiThietBi.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(378, 77);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(229, 20);
            this.txtSearch.TabIndex = 8;
            // 
            // QLLoaiThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvLoaiThietBi);
            this.Controls.Add(this.ucCrudButtons1);
            this.Name = "QLLoaiThietBi";
            this.Text = "Quản lý loại thiết bị";
            this.Load += new System.EventHandler(this.QLLoaiThietBi_Load);
            this.Controls.SetChildIndex(this.ucCrudButtons1, 0);
            this.Controls.SetChildIndex(this.dgvLoaiThietBi, 0);
            this.Controls.SetChildIndex(this.txtSearch, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiThietBi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcCrudButtons ucCrudButtons1;
        private System.Windows.Forms.DataGridView dgvLoaiThietBi;
        private System.Windows.Forms.TextBox txtSearch;
    }
}