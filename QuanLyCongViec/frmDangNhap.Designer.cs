namespace QuanLyCongViec
{
    partial class frmDangNhap
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
            this.lblTaiKhoan = new System.Windows.Forms.Label();
            this.lblMatKhau = new System.Windows.Forms.Label();
            this.txtTaiKhoan = new System.Windows.Forms.TextBox();
            this.txtMatKhau = new System.Windows.Forms.TextBox();
            this.btnXacNhan = new System.Windows.Forms.Button();
            this.linklblDangKy = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblHintTaiKhoan = new System.Windows.Forms.Label();
            this.lblHintMatKhau = new System.Windows.Forms.Label();
            this.chkGhiNhoDangNhap = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblTaiKhoan
            // 
            this.lblTaiKhoan.AutoSize = true;
            this.lblTaiKhoan.Location = new System.Drawing.Point(50, 50);
            this.lblTaiKhoan.Name = "lblTaiKhoan";
            this.lblTaiKhoan.Size = new System.Drawing.Size(70, 16);
            this.lblTaiKhoan.TabIndex = 0;
            this.lblTaiKhoan.Text = "Tài khoản:";
            // 
            // lblMatKhau
            // 
            this.lblMatKhau.AutoSize = true;
            this.lblMatKhau.Location = new System.Drawing.Point(50, 100);
            this.lblMatKhau.Name = "lblMatKhau";
            this.lblMatKhau.Size = new System.Drawing.Size(64, 16);
            this.lblMatKhau.TabIndex = 0;
            this.lblMatKhau.Text = "Mật khẩu:";
            // 
            // txtTaiKhoan
            // 
            this.txtTaiKhoan.Location = new System.Drawing.Point(140, 47);
            this.txtTaiKhoan.Name = "txtTaiKhoan";
            this.txtTaiKhoan.Size = new System.Drawing.Size(250, 22);
            this.txtTaiKhoan.TabIndex = 1;
            this.txtTaiKhoan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTaiKhoan_KeyDown);
            // 
            // txtMatKhau
            // 
            this.txtMatKhau.Location = new System.Drawing.Point(140, 97);
            this.txtMatKhau.Name = "txtMatKhau";
            this.txtMatKhau.PasswordChar = '*';
            this.txtMatKhau.Size = new System.Drawing.Size(250, 22);
            this.txtMatKhau.TabIndex = 2;
            this.txtMatKhau.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMatKhau_KeyDown);
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(140, 175);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(250, 40);
            this.btnXacNhan.TabIndex = 4;
            this.btnXacNhan.Text = "Đăng nhập";
            this.btnXacNhan.UseVisualStyleBackColor = true;
            this.btnXacNhan.Click += new System.EventHandler(this.btnXacNhan_Click);
            // 
            // linklblDangKy
            // 
            this.linklblDangKy.AutoSize = true;
            this.linklblDangKy.Location = new System.Drawing.Point(280, 235);
            this.linklblDangKy.Name = "linklblDangKy";
            this.linklblDangKy.Size = new System.Drawing.Size(56, 16);
            this.linklblDangKy.TabIndex = 4;
            this.linklblDangKy.TabStop = true;
            this.linklblDangKy.Text = "Đăng ký";
            this.linklblDangKy.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linklblDangKy_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label1.Location = new System.Drawing.Point(140, 230);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chưa có tài khoản?";
            // 
            // lblHintTaiKhoan
            // 
            this.lblHintTaiKhoan.AutoSize = true;
            this.lblHintTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintTaiKhoan.ForeColor = System.Drawing.Color.Gray;
            this.lblHintTaiKhoan.Location = new System.Drawing.Point(140, 72);
            this.lblHintTaiKhoan.Name = "lblHintTaiKhoan";
            this.lblHintTaiKhoan.Size = new System.Drawing.Size(179, 16);
            this.lblHintTaiKhoan.TabIndex = 5;
            this.lblHintTaiKhoan.Text = "Nhập tên đăng nhập của bạn";
            // 
            // lblHintMatKhau
            // 
            this.lblHintMatKhau.AutoSize = true;
            this.lblHintMatKhau.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHintMatKhau.ForeColor = System.Drawing.Color.Gray;
            this.lblHintMatKhau.Location = new System.Drawing.Point(140, 122);
            this.lblHintMatKhau.Name = "lblHintMatKhau";
            this.lblHintMatKhau.Size = new System.Drawing.Size(193, 16);
            this.lblHintMatKhau.TabIndex = 5;
            this.lblHintMatKhau.Text = "Nhập mật khẩu (tối thiểu 6 ký tự)";
            // 
            // chkGhiNhoDangNhap
            // 
            this.chkGhiNhoDangNhap.AutoSize = true;
            this.chkGhiNhoDangNhap.Location = new System.Drawing.Point(140, 145);
            this.chkGhiNhoDangNhap.Name = "chkGhiNhoDangNhap";
            this.chkGhiNhoDangNhap.Size = new System.Drawing.Size(141, 20);
            this.chkGhiNhoDangNhap.TabIndex = 3;
            this.chkGhiNhoDangNhap.Text = "Ghi nhớ đăng nhập";
            this.chkGhiNhoDangNhap.UseVisualStyleBackColor = true;
            // 
            // frmDangNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 270);
            this.Controls.Add(this.chkGhiNhoDangNhap);
            this.Controls.Add(this.lblHintMatKhau);
            this.Controls.Add(this.lblHintTaiKhoan);
            this.Controls.Add(this.linklblDangKy);
            this.Controls.Add(this.btnXacNhan);
            this.Controls.Add(this.txtMatKhau);
            this.Controls.Add(this.txtTaiKhoan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblMatKhau);
            this.Controls.Add(this.lblTaiKhoan);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDangNhap";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập - Ứng dụng Quản lý Công việc";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTaiKhoan;
        private System.Windows.Forms.Label lblMatKhau;
        private System.Windows.Forms.TextBox txtTaiKhoan;
        private System.Windows.Forms.TextBox txtMatKhau;
        private System.Windows.Forms.Button btnXacNhan;
        private System.Windows.Forms.LinkLabel linklblDangKy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblHintTaiKhoan;
        private System.Windows.Forms.Label lblHintMatKhau;
        private System.Windows.Forms.CheckBox chkGhiNhoDangNhap;
    }
}

