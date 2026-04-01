namespace QuanLyThietBi
{
    partial class QLCapDonVi
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
            this.dgvCapDonVi = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapDonVi)).BeginInit();
            this.SuspendLayout();
            // 
            // ucCrudButtons1
            // 
            this.ucCrudButtons1.Location = new System.Drawing.Point(12, 67);
            this.ucCrudButtons1.Name = "ucCrudButtons1";
            this.ucCrudButtons1.Size = new System.Drawing.Size(360, 30);
            this.ucCrudButtons1.TabIndex = 1;
            // 
            // dgvCapDonVi
            // 
            this.dgvCapDonVi.AllowUserToAddRows = false;
            this.dgvCapDonVi.AllowUserToDeleteRows = false;
            this.dgvCapDonVi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCapDonVi.Location = new System.Drawing.Point(12, 103);
            this.dgvCapDonVi.Name = "dgvCapDonVi";
            this.dgvCapDonVi.ReadOnly = true;
            this.dgvCapDonVi.Size = new System.Drawing.Size(776, 335);
            this.dgvCapDonVi.TabIndex = 2;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(378, 77);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(229, 20);
            this.txtSearch.TabIndex = 8;
            // 
            // QLCapDonVi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgvCapDonVi);
            this.Controls.Add(this.ucCrudButtons1);
            this.Name = "QLCapDonVi";
            this.Text = "QLCapDonVi";
            this.Controls.SetChildIndex(this.ucCrudButtons1, 0);
            this.Controls.SetChildIndex(this.dgvCapDonVi, 0);
            this.Controls.SetChildIndex(this.txtSearch, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCapDonVi)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UcCrudButtons ucCrudButtons1;
        private System.Windows.Forms.DataGridView dgvCapDonVi;
        private System.Windows.Forms.TextBox txtSearch;
    }
}