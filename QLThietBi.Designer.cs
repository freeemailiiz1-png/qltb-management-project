namespace QuanLyThietBi
{
    partial class QLThietBi
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
            this.dgvThietBi = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnCapDoi = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).BeginInit();
            this.SuspendLayout();
            // 
            // ucCrudButtons1
            // 
            this.ucCrudButtons1.Location = new System.Drawing.Point(12, 67);
            this.ucCrudButtons1.Name = "ucCrudButtons1";
            this.ucCrudButtons1.Size = new System.Drawing.Size(360, 30);
            this.ucCrudButtons1.TabIndex = 1;
            // 
            // dgvThietBi
            // 
            this.dgvThietBi.AllowUserToAddRows = false;
            this.dgvThietBi.AllowUserToDeleteRows = false;
            this.dgvThietBi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvThietBi.Location = new System.Drawing.Point(12, 103);
            this.dgvThietBi.Name = "dgvThietBi";
            this.dgvThietBi.ReadOnly = true;
            this.dgvThietBi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvThietBi.Size = new System.Drawing.Size(1060, 335);
            this.dgvThietBi.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(378, 77);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(229, 20);
            this.txtSearch.TabIndex = 8;
            // 
            // btnCapDoi
            // 
            this.btnCapDoi.Location = new System.Drawing.Point(613, 67);
            this.btnCapDoi.Name = "btnCapDoi";
            this.btnCapDoi.Size = new System.Drawing.Size(100, 30);
            this.btnCapDoi.TabIndex = 9;
            this.btnCapDoi.Text = "Cấp/Đổi TB";
            this.btnCapDoi.UseVisualStyleBackColor = true;
            this.btnCapDoi.Click += new System.EventHandler(this.btnCapDoi_Click);
            // 
            // QLThietBi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1084, 450);
            this.Controls.Add(this.btnCapDoi);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvThietBi);
            this.Controls.Add(this.ucCrudButtons1);
            this.Name = "QLThietBi";
            this.Text = "Quản lý thiết bị";
            this.Load += new System.EventHandler(this.QLThietBi_Load);
            this.Controls.SetChildIndex(this.ucCrudButtons1, 0);
            this.Controls.SetChildIndex(this.dgvThietBi, 0);
            this.Controls.SetChildIndex(this.txtSearch, 0);
            this.Controls.SetChildIndex(this.btnCapDoi, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvThietBi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcCrudButtons ucCrudButtons1;
        private System.Windows.Forms.DataGridView dgvThietBi;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button btnCapDoi;
    }
}