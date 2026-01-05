using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace QuanLyCongViec
{
    public partial class frmThongBao : Form
    {
        // Dùng static để tránh việc lặp lại tên User do trùng seed thời gian
        private static readonly Random _rnd = new Random();
        private string _currentUserName = "";
        private List<string[]> _originalJobsForUser = new List<string[]>();

        private List<string[]> _jobPool = new List<string[]>()
        {
            new[] { "Làm đồ án", "10/01/2026", "Cao", "3 ngày", "Chưa hoàn thành" },
            new[] { "Học tiếng Anh", "08/01/2026", "Trung bình", "1 ngày", "Đang làm" },
            new[] { "Đi chơi", "07/01/2026", "Thấp", "Hôm nay", "Hoàn thành" },
            new[] { "Viết báo cáo", "09/01/2026", "Cao", "2 ngày", "Chưa hoàn thành" },
            new[] { "Test phần mềm", "11/01/2026", "Trung bình", "4 ngày", "Chưa hoàn thành" },
            new[] { "Deploy hệ thống", "12/01/2026", "Cao", "5 ngày", "Chưa hoàn thành" },
            new[] { "Họp nhóm", "13/01/2026", "Trung bình", "6 ngày", "Chưa hoàn thành" },
            new[] { "Nghiên cứu AI", "15/01/2026", "Cao", "8 ngày", "Chưa hoàn thành" },
            new[] { "Gửi mail khách hàng", "17/01/2026", "Cao", "10 ngày", "Chưa hoàn thành" },
            new[] { "Sửa lỗi giao diện", "19/01/2026", "Trung bình", "12 ngày", "Chưa hoàn thành" }
        };

        public frmThongBao()
        {
            InitializeComponent();
            this.Load += (s, e) => {
                SetupGrid();
                InitNewSession();
            };

            btnThem.Click += btnThem_Click;

            // TẢI LẠI: Khôi phục dữ liệu gốc của User đó (Reset thao tác Xóa/Sửa)
            btnReload.Click += (s, e) => {
                LoadDataFromOriginal();
                MessageBox.Show($"Đã reset dữ liệu gốc cho: {_currentUserName}", "Tải lại");
            };

            btnDelete.Click += (s, e) => {
                if (dgvThongBao.SelectedRows.Count > 0) dgvThongBao.Rows.RemoveAt(dgvThongBao.SelectedRows[0].Index);
            };

            btnClose.Click += (s, e) => this.Close();

            btnMarkAsRead.Click += (s, e) => {
                if (dgvThongBao.SelectedRows.Count > 0)
                {
                    dgvThongBao.SelectedRows[0].Cells["TrangThai"].Value = "Hoàn thành";
                    UpdateFormatting();
                }
            };
        }

        private void InitNewSession()
        {
            // 20 User đầy đủ - Shuffle để không bị lặp
            string[] users = {
                "Nguyễn Văn An", "Trần Thị Bình", "Lê Văn Cường", "Phạm Thị Dung",
                "Hoàng Văn Đức", "Ngô Thị Hương", "Vũ Văn Hùng", "Đỗ Thị Lan",
                "Bùi Văn Minh", "Lý Thị Nga", "Đinh Văn Phong", "Mai Thị Quỳnh",
                "Tạ Văn Sơn", "Võ Thị Trang", "Phan Văn Tuấn", "Hồ Thị Uyên",
                "Dương Văn Việt", "Lưu Thị Yến", "Chu Văn Bảo", "Trịnh Thị Châu"
            };

            _currentUserName = users.OrderBy(x => _rnd.Next()).First();
            this.Text = "🔔 Thông Báo - " + _currentUserName;

            // Bốc 8 việc ngẫu nhiên từ kho
            _originalJobsForUser = _jobPool.OrderBy(x => _rnd.Next()).Take(8).ToList();

            LoadDataFromOriginal();
        }

        private void LoadDataFromOriginal()
        {
            dgvThongBao.Rows.Clear();
            int i = 1;
            foreach (var job in _originalJobsForUser)
            {
                dgvThongBao.Rows.Add(_currentUserName, "CV" + i, job[0], job[1], job[2], job[3], job[4]);
                i++;
            }
            UpdateFormatting();
        }

        private void SetupGrid()
        {
            dgvThongBao.Columns.Clear();
            dgvThongBao.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvThongBao.AllowUserToAddRows = false;
            dgvThongBao.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvThongBao.ReadOnly = true;

            dgvThongBao.Columns.Add("NguoiDung", "Người dùng");
            dgvThongBao.Columns.Add("MaCV", "Mã CV");
            dgvThongBao.Columns.Add("TieuDe", "Tiêu đề");
            dgvThongBao.Columns.Add("HanChot", "Hạn chót");
            dgvThongBao.Columns.Add("UuTien", "Ưu tiên");
            dgvThongBao.Columns.Add("ConNgay", "Còn (ngày)");
            dgvThongBao.Columns.Add("TrangThai", "Trạng thái");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            Form f = new Form() { Width = 350, Height = 150, Text = "Thêm mới", StartPosition = FormStartPosition.CenterParent };
            TextBox t = new TextBox() { Left = 20, Top = 20, Width = 280 };
            Button b = new Button() { Text = "OK", Left = 220, Top = 60, DialogResult = DialogResult.OK };
            f.Controls.Add(t); f.Controls.Add(b);

            if (f.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(t.Text))
            {
                dgvThongBao.Rows.Add(_currentUserName, "CV" + (dgvThongBao.Rows.Count + 1), t.Text, "04/01/2026", "Thường", "Mới", "Chưa hoàn thành");
                UpdateFormatting();
            }
        }

        private void UpdateFormatting()
        {
            foreach (DataGridViewRow row in dgvThongBao.Rows)
            {
                string tt = row.Cells["TrangThai"].Value?.ToString();
                string cn = row.Cells["ConNgay"].Value?.ToString();

                if (tt == "Chưa hoàn thành") row.DefaultCellStyle.ForeColor = Color.Red;
                else if (tt == "Đang làm") row.DefaultCellStyle.ForeColor = Color.Orange;
                else row.DefaultCellStyle.ForeColor = Color.Green;

                if (cn == "Hôm nay") row.DefaultCellStyle.BackColor = Color.Yellow;
                else row.DefaultCellStyle.BackColor = Color.White;

                if (row.Cells["MaCV"].Value?.ToString() == "CV1")
                {
                    row.DefaultCellStyle.BackColor = Color.FromArgb(0, 112, 192);
                    row.DefaultCellStyle.ForeColor = Color.White;
                }
            }
        }
    }
}