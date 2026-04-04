namespace QuanLyThietBi
{
    partial class QLUser
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgUser = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.ucCrudButtons = new QuanLyThietBi.UcCrudButtons();
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(280, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Danh sách tài khoản";
            // 
            // dgUser
            // 
            this.dgUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUser.Location = new System.Drawing.Point(12, 128);
            this.dgUser.Name = "dgUser";
            this.dgUser.Size = new System.Drawing.Size(860, 346);
            this.dgUser.TabIndex = 1;
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(378, 102);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(229, 20);
            this.txtSearch.TabIndex = 6;
            // 
            // ucCrudButtons
            // 
            this.ucCrudButtons.Location = new System.Drawing.Point(12, 92);
            this.ucCrudButtons.Name = "ucCrudButtons";
            this.ucCrudButtons.Size = new System.Drawing.Size(360, 30);
            this.ucCrudButtons.TabIndex = 1;
            // 
            // QLUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 486);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.dgUser);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ucCrudButtons);
            this.Name = "QLUser";
            this.Text = "QLUser";
            this.Load += new System.EventHandler(this.QLUser_Load);
            this.Controls.SetChildIndex(this.ucCrudButtons, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.dgUser, 0);
            this.Controls.SetChildIndex(this.txtSearch, 0);
            ((System.ComponentModel.ISupportInitialize)(this.dgUser)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgUser;
        private System.Windows.Forms.TextBox txtSearch;
        private QuanLyThietBi.UcCrudButtons ucCrudButtons;
    }
}