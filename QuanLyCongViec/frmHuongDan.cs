using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    public partial class frmHuongDan : Form
    {
        public frmHuongDan()
        {
            InitializeComponent();
            LoadMenuItems();
            ShowContent("Cập nhật Tiến độ Task"); // Mở mặc định trang quan trọng nhất
        }

        private void LoadMenuItems()
        {
            // Danh sách Menu (Sắp xếp theo thứ tự hiển thị từ trên xuống)
            string[] menuItems = {
                "Mẹo & Thủ thuật",
                "Nhập liệu Công việc",
                "Nhật ký Lịch thay đổi",
                "Cập nhật Tiến độ Task",
                "Chi tiết Công việc",
                "Quản lý Thông báo"
            };

            foreach (string item in menuItems)
            {
                Button btn = new Button();
                btn.Text = item;
                btn.Dock = DockStyle.Top;
                btn.Height = 50;
                btn.TextAlign = ContentAlignment.MiddleLeft;
                btn.Padding = new Padding(25, 0, 0, 0);
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 0;
                btn.BackColor = Color.FromArgb(245, 245, 245);
                btn.Font = new Font("Segoe UI", 10, FontStyle.Regular);

                btn.Click += (s, e) => {
                    ShowContent(item);
                    HighlightButton(btn);
                };
                pnlSidebar.Controls.Add(btn);
            }
        }

        private void HighlightButton(Button clickedBtn)
        {
            foreach (Control ctrl in pnlSidebar.Controls)
                if (ctrl is Button btn) { btn.BackColor = Color.FromArgb(245, 245, 245); btn.ForeColor = Color.Black; }

            clickedBtn.BackColor = Color.FromArgb(0, 120, 215); // Màu xanh dương đậm
            clickedBtn.ForeColor = Color.White;
        }

        private void ShowContent(string menuItem)
        {
            pnlContent.Controls.Clear();
            pnlContent.Padding = new Padding(40); // Tạo không gian thoáng cho văn bản

            switch (menuItem)
            {
                case "Quản lý Thông báo":
                    ShowGenericContent("HỆ THỐNG QUẢN LÝ THÔNG BÁO",
                        "Đây là điểm khởi đầu của quy trình. Mọi thay đổi về Task đều được gửi về đây để bạn kịp thời xử lý.",
                        "• Theo dõi danh sách: Các thông báo mới sẽ xuất hiện ở đầu danh sách với biểu tượng chuông.\n" +
                        "• Tương tác nhanh: Nhấp vào thông báo để hệ thống tự động mở mục 'Chi tiết Công việc' tương ứng.\n" +
                        "• Phân loại ưu tiên: Các thông báo màu đỏ yêu cầu xử lý ngay vì sắp hết hạn (Deadline).\n" +
                        "• Dọn dẹp: Đánh dấu 'Đã đọc' để ẩn các thông báo cũ, giúp tập trung vào công việc hiện tại.");
                    break;

                case "Chi tiết Công việc":
                    ShowGenericContent("HƯỚNG DẪN XEM CHI TIẾT TASK",
                        "Màn hình này cung cấp toàn bộ hồ sơ nghiệp vụ để bạn thực hiện công việc chính xác nhất.",
                        "• Tiếp nhận hồ sơ: Xem mô tả mục tiêu, yêu cầu kỹ thuật và các tài liệu đính kèm (PDF, Hình ảnh).\n" +
                        "• Danh sách công việc con (Checklist): Hoàn thành từng hạng mục nhỏ để đạt được mục tiêu lớn.\n" +
                        "• Nhân sự liên quan: Biết rõ ai là người giao việc (Owner) và ai là người hỗ trợ (Collaborator).\n" +
                        "• Phối hợp: Sử dụng khung thảo luận để gửi báo cáo nhanh hoặc hỏi đáp về các vướng mắc tại chỗ.");
                    break;

                case "Cập nhật Tiến độ Task":
                    ShowProgressContent(); // Hàm xử lý riêng cho phần có TrackBar
                    break;

                case "Nhật ký Lịch thay đổi":
                    ShowGenericContent("TRUY XUẤT NHẬT KÝ VẬN HÀNH",
                        "Hệ thống lưu trữ mọi dấu vết chỉnh sửa để đảm bảo tính minh bạch và phục vụ công tác đối soát.",
                        "• Lịch sử thay đổi: Xem lại bạn đã cập nhật tiến độ vào những khung giờ nào.\n" +
                        "• Truy vết người dùng: Hệ thống ghi nhận chính xác ID người đã thay đổi nội dung hoặc thời hạn Task.\n" +
                        "• Đối chiếu dữ liệu: Khi có sai lệch, nhật ký là bằng chứng để xác định nguyên nhân và thời điểm phát sinh.\n" +
                        "• Báo cáo: Hỗ trợ trích xuất lịch sử làm việc để làm căn cứ đánh giá hiệu quả cuối tháng.");
                    break;

                case "Nhập liệu Công việc":
                    ShowGenericContent("QUY TRÌNH KHỞI TẠO TASK MỚI",
                        "Hướng dẫn dành cho cấp quản lý để đưa công việc mới vào hệ thống quản lý tập trung.",
                        "• Bước 1: Nhập tiêu đề rõ ràng và đính kèm tài liệu hướng dẫn vận hành từ mục 'Chi tiết Task'.\n" +
                        "• Bước 2: Thiết lập thời hạn (Deadline) để hệ thống tự động đẩy thông báo nhắc nhở.\n" +
                        "• Bước 3: Phân quyền cho đúng nhân sự chịu trách nhiệm chính để tránh chồng chéo công việc.\n" +
                        "• Bước 4: Kiểm tra lại trạng thái ban đầu trước khi bấm 'Phát hành' thông báo tới nhân viên.");
                    break;

                case "Mẹo & Thủ thuật":
                    ShowGenericContent("MẸO TỐI ƯU HÓA THAO TÁC",
                        "Sử dụng các phím tắt và thao tác thông minh giúp bạn làm việc nhanh hơn 30%.",
                        "• Phím tắt F5: Làm mới (Refresh) toàn bộ danh sách để cập nhật thông báo mới nhất ngay lập tức.\n" +
                        "• Kéo thả tài liệu: Bạn có thể kéo file từ máy tính vào khung Chi tiết Task để đính kèm cực nhanh.\n" +
                        "• Chuột phải: Sử dụng menu chuột phải để thay đổi trạng thái Task mà không cần mở sâu bên trong.\n" +
                        "• Chế độ lọc: Sử dụng từ khóa hoặc màu sắc để tìm nhanh các công việc đang bị chậm tiến độ.");
                    break;
            }
        }

        private void ShowGenericContent(string title, string desc, string bullets)
        {
            AddLabel(title, new Font("Segoe UI", 18, FontStyle.Bold), Color.FromArgb(30, 60, 100), 10);

            Panel line = new Panel { Height = 2, BackColor = Color.FromArgb(30, 60, 100), Dock = DockStyle.Top };
            pnlContent.Controls.Add(line);

            AddLabel(desc, new Font("Segoe UI", 11, FontStyle.Italic), Color.FromArgb(50, 50, 50), 25);
            AddLabel(bullets, new Font("Segoe UI", 11), Color.FromArgb(30, 30, 30), 0);
        }

        private void ShowProgressContent()
        {
            AddLabel("CẬP NHẬT TIẾN ĐỘ & TRẠNG THÁI CÔNG VIỆC", new Font("Segoe UI", 18, FontStyle.Bold), Color.FromArgb(30, 60, 100), 5);

            Panel line = new Panel { Height = 2, BackColor = Color.FromArgb(30, 60, 100), Dock = DockStyle.Top };
            pnlContent.Controls.Add(line);

            AddLabel("Sau khi tiếp nhận Task từ Thông báo và xem Chi tiết, hãy kéo thanh trượt dưới đây để cập nhật kết quả thực hiện của bạn.", new Font("Segoe UI", 11), Color.Black, 25);

            // --- Panel chứa TrackBar biến thiên ---
            Panel progPanel = new Panel { Dock = DockStyle.Top, Height = 140, BorderStyle = BorderStyle.FixedSingle, BackColor = Color.White, Margin = new Padding(0, 0, 0, 30) };
            pnlContent.Controls.Add(progPanel);

            Label lblPct = new Label { Text = "75%", Font = new Font("Segoe UI", 24, FontStyle.Bold), ForeColor = Color.DarkOrange, Location = new Point(35, 20), AutoSize = true };
            progPanel.Controls.Add(lblPct);

            Label lblStatus = new Label { Text = "Đang thực hiện", Font = new Font("Segoe UI", 10, FontStyle.Bold), Location = new Point(130, 32), Size = new Size(130, 30), TextAlign = ContentAlignment.MiddleCenter, BorderStyle = BorderStyle.FixedSingle };
            progPanel.Controls.Add(lblStatus);

            TrackBar track = new TrackBar { Location = new Point(30, 80), Size = new Size(550, 45), Minimum = 0, Maximum = 100, Value = 75, TickFrequency = 10 };
            progPanel.Controls.Add(track);

            // Xử lý sự kiện kéo thanh trượt thay đổi nội dung trực tiếp
            track.ValueChanged += (s, e) => {
                int v = track.Value;
                lblPct.Text = v + "%";
                if (v < 40) { lblPct.ForeColor = Color.Red; lblStatus.Text = "Chậm tiến độ"; lblStatus.ForeColor = Color.Red; }
                else if (v < 100) { lblPct.ForeColor = Color.DarkOrange; lblStatus.Text = "Đang thực hiện"; lblStatus.ForeColor = Color.DarkOrange; }
                else { lblPct.ForeColor = Color.Green; lblStatus.Text = "Đã hoàn thành"; lblStatus.ForeColor = Color.Green; }
            };

            AddLabel("🔴 Red: Cảnh báo chậm tiến độ (< 40%)", new Font("Segoe UI", 10), Color.Red, 0);
            AddLabel("🟠 Orange: Công việc đang triển khai (40% - 99%)", new Font("Segoe UI", 10), Color.DarkOrange, 0);
            AddLabel("🟢 Green: Hoàn thành mục tiêu (100%)", new Font("Segoe UI", 10), Color.Green, 30);

            // Box ghi chú quan trọng
            Panel warn = new Panel { Dock = DockStyle.Top, BackColor = Color.FromArgb(255, 252, 225), Padding = new Padding(20), BorderStyle = BorderStyle.FixedSingle, Height = 100 };
            pnlContent.Controls.Add(warn);

            Label warnTitle = new Label { Text = "⭐ QUY ĐỊNH HỆ THỐNG:", Font = new Font("Segoe UI", 10, FontStyle.Bold), ForeColor = Color.DarkRed, Dock = DockStyle.Top, AutoSize = true };
            warn.Controls.Add(warnTitle);

            Label warnText = new Label { Text = "Khi thanh tiến độ đạt 100%, hệ thống sẽ tự động khóa nội dung và gửi thông báo hoàn tất tới người quản lý. Hãy chắc chắn bạn đã đính kèm đủ tài liệu báo cáo tại mục 'Chi tiết Task' trước khi lưu.", Font = new Font("Segoe UI", 10), Dock = DockStyle.Fill, ForeColor = Color.FromArgb(64, 64, 64) };
            warn.Controls.Add(warnText);
        }

        private void AddLabel(string text, Font font, Color color, int bottomMargin)
        {
            Label lbl = new Label
            {
                Text = text,
                Font = font,
                ForeColor = color,
                Dock = DockStyle.Top,
                AutoSize = true,
                MaximumSize = new Size(700, 0),
                Padding = new Padding(0, 5, 0, bottomMargin)
            };
            pnlContent.Controls.Add(lbl);
        }
    }
}