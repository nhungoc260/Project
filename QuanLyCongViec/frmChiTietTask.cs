using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    public partial class frmChiTietTask : Form
    {
        private List<string> _historyLogs = new List<string>();
        private CultureInfo viVN = new CultureInfo("vi-VN");

        public frmChiTietTask()
        {
            InitializeComponent();
            LoadAllData(); // Nạp User, Ưu tiên và Trạng thái
            ConfigDateTime();

            // 1. Đổi màu chữ theo Ưu tiên
            cboUuTien.SelectedIndexChanged += (s, e) => {
                if (cboUuTien.Text == "Cao") cboUuTien.ForeColor = Color.Red;
                else if (cboUuTien.Text == "Trung bình") cboUuTien.ForeColor = Color.DarkOrange;
                else cboUuTien.ForeColor = Color.Green;
            };

            // 2. Tự động cập nhật Trạng thái và Màu sắc theo thanh Tiến độ
            trkTienDo.Scroll += (s, e) => {
                int val = trkTienDo.Value;
                lblPhanTram.Text = val + "%";
                lblPhanTram.ForeColor = (val < 40) ? Color.Red : (val < 80 ? Color.Orange : Color.Green);

                if (val == 100) cboTrangThai.SelectedItem = "Hoàn thành";
                else if (val > 0) cboTrangThai.SelectedItem = "Đang làm";
                else cboTrangThai.SelectedItem = "Chưa hoàn thành";
            };

            // 3. Xem Nhật ký lưu (Tiếng Việt + Ghi chú)
            btnLichSu.Click += (s, e) => {
                if (_historyLogs.Count == 0)
                    MessageBox.Show("Chưa có nhật ký lưu nào.", "Thông báo");
                else
                    MessageBox.Show(string.Join("\n" + new string('-', 45) + "\n", _historyLogs), "Nhật ký Task");
            };

            // 4. Lưu dữ liệu và Ghi log
            btnLuu.Click += (s, e) => {
                if (string.IsNullOrWhiteSpace(txtTen.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên công việc!", "Nhắc nhở");
                    return;
                }

                string note = string.IsNullOrWhiteSpace(txtMoTa.Text) ? "Không có ghi chú" : txtMoTa.Text;
                string timeVN = DateTime.Now.ToString("HH:mm:ss - dddd, dd/MM/yyyy", viVN);

                string entry = $"[{timeVN}]\n" +
                               $"| Mã: {txtMa.Text} | Task: {txtTen.Text}\n" +
                               $"   => {cboTrangThai.Text} ({trkTienDo.Value}%) | Sửa bởi: {cboNguoiLam.Text}\n" +
                               $"   => Ghi chú: {note}";

                _historyLogs.Add(entry);
                MessageBox.Show("Đã lưu vào nhật ký hệ thống!", "Thành công");
            };

            btnHuy.Click += (s, e) => this.Close();
        }

        private void ConfigDateTime()
        {
            dtpHan.Format = DateTimePickerFormat.Custom;
            dtpHan.CustomFormat = "dd/MM/yyyy";
        }

        private void LoadAllData()
        {
            // Nạp 20 Người thực hiện
            string[] users = {
                "Nguyễn Văn An", "Trần Thị Bình", "Lê Văn Cường", "Phạm Thị Dung",
                "Hoàng Văn Đức", "Ngô Thị Hương", "Vũ Văn Hùng", "Đỗ Thị Lan",
                "Bùi Văn Minh", "Lý Thị Nga", "Đinh Văn Phong", "Mai Thị Quỳnh",
                "Tạ Văn Sơn", "Võ Thị Trang", "Phan Văn Tuấn", "Hồ Thị Uyên",
                "Dương Văn Việt", "Lưu Thị Yến", "Chu Văn Bảo", "Trịnh Thị Châu"
            };
            cboNguoiLam.Items.Clear();
            cboNguoiLam.Items.AddRange(users);

            // Nạp Mức ưu tiên
            cboUuTien.Items.Clear();
            cboUuTien.Items.AddRange(new object[] { "Cao", "Trung bình", "Thấp" });

            // Nạp Trạng thái (Đã sửa lỗi thiếu ở đây)
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { "Chưa hoàn thành", "Đang làm", "Hoàn thành" });
        }

        // Constructor dùng khi mở từ danh sách chính
        public frmChiTietTask(string ma, string ten, string han, string uutien, string trangthai, string nguoi) : this()
        {
            txtMa.Text = ma; txtTen.Text = ten;
            dtpHan.Value = DateTime.TryParse(han, out DateTime d) ? d : DateTime.Now;
            cboUuTien.SelectedItem = uutien;
            cboTrangThai.SelectedItem = trangthai;
            cboNguoiLam.SelectedItem = nguoi;
            trkTienDo.Value = (trangthai == "Hoàn thành") ? 100 : (trangthai == "Đang làm" ? 50 : 0);
            lblPhanTram.Text = trkTienDo.Value + "%";
        }
    }
}