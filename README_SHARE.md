# 📦 Project Quản Lý Công Việc - Sẵn Sàng Chia Sẻ

## 🎯 Thông tin Project

- **Tên**: Quản Lý Công Việc
- **Framework**: .NET Framework 4.7.2
- **Database**: SQL Server
- **Kiến trúc**: 3 lớp (UI, Business, Data Access)
- **Ngôn ngữ**: C# (Windows Forms)

## 📋 Nội dung trong thư mục này

Thư mục này chứa **TẤT CẢ** files cần thiết để:
- ✅ Chia sẻ project cho người khác
- ✅ Setup và chạy trên máy mới
- ✅ Không cần file nào khác

## 🚀 Cách sử dụng

### Bước 1: Giải nén/Mở project
1. Copy toàn bộ thư mục `Project` vào máy mới
2. Mở Visual Studio 2022 (hoặc Visual Studio 2019+)
3. Mở file `QuanLyCongViec.sln`

### Bước 2: Tạo Database
1. Mở **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**
2. Kết nối với SQL Server của bạn
3. Mở file `Database/CreateDatabase.sql`
4. Chạy script (F5)
5. Kiểm tra: Database **QuanLyCongViec** đã được tạo
6. Mở file `Database/AdditionalProcedures.sql`
7. Chạy script (F5)
8. Kiểm tra: Đã tạo 11 Stored Procedures

**✅ Lưu ý:** File `CreateDatabase.sql` hoạt động trên mọi máy có SQL Server!

### Bước 3: Cấu hình Connection String (QUAN TRỌNG)
1. Mở file `QuanLyCongViec/App.config`
2. Tìm dòng connection string
3. **Chỉnh sửa Server name** cho phù hợp với máy của bạn:
   ```xml
   <add name="QuanLyCongViecConnection" 
        connectionString="Server=YOUR_SERVER;Database=QuanLyCongViec;Integrated Security=True;TrustServerCertificate=True;" 
        providerName="System.Data.SqlClient" />
   ```
4. Thay `YOUR_SERVER` bằng tên SQL Server của bạn:
   - `.\SQLEXPRESS` (SQL Server Express - mặc định)
   - `localhost\SQLEXPRESS` (SQL Server Express)
   - `localhost` (SQL Server mặc định)
   - `(localdb)\MSSQLLocalDB` (LocalDB)
   - `TÊN_MÁY\SQLEXPRESS` (Tên máy tính của bạn)

**💡 Cách tìm server name:**
- Mở SSMS hoặc Azure Data Studio
- Xem server name trong dropdown khi kết nối
- Copy server name đó vào App.config

### Bước 4: Build và Chạy
1. Build project (Ctrl + Shift + B)
2. Chạy project (F5)
3. Nếu thấy form "Đăng nhập" = ✅ OK

### Bước 5: Tạo tài khoản đầu tiên
1. Trên form Đăng nhập, click "Đăng ký"
2. Điền đầy đủ thông tin:
   - **Tên đăng nhập**: 3-50 ký tự (bắt buộc)
   - **Mật khẩu**: Tối thiểu 6 ký tự (bắt buộc)
   - **Xác nhận mật khẩu**: Phải khớp với mật khẩu
   - **Họ và tên**: Bắt buộc
   - **Email**: Định dạng hợp lệ (bắt buộc)
   - **Đồng ý điều khoản**: Phải tích chọn
3. Click "Xác nhận" để đăng ký
4. Sau khi đăng ký thành công, quay lại form Đăng nhập
5. Đăng nhập bằng tài khoản vừa tạo

### Bước 6: Chèn dữ liệu mẫu (Tùy chọn)
1. Sau khi đã tạo ít nhất 1 tài khoản Users
2. Mở file `Database/InsertSampleData.sql`
3. Chạy script (F5)
4. Script sẽ chèn dữ liệu Tasks (công việc) mẫu
5. **Lưu ý**: Script này chỉ chèn Tasks, không chèn Users

## 📁 Cấu trúc thư mục

```
Project/
├── QuanLyCongViec.sln              ← Solution file
├── README_SHARE.md                 ← File này
├── SETUP_INSTRUCTIONS.txt          ← Hướng dẫn nhanh
├── Database/                       ← SQL Scripts
│   ├── CreateDatabase.sql          ← Script tạo database (BẮT BUỘC)
│   ├── AdditionalProcedures.sql    ← Script tạo stored procedures (BẮT BUỘC)
│   └── InsertSampleData.sql        ← Dữ liệu Tasks mẫu (TÙY CHỌN)
└── QuanLyCongViec/                 ← Project code
    ├── QuanLyCongViec.csproj
    ├── App.config                  ← Connection string
    ├── Program.cs                  ← Entry point
    ├── DataAccess/
    │   └── DatabaseHelper.cs       ← Helper kết nối database
    ├── Helpers/
    │   └── PasswordHelper.cs       ← Helper hash mật khẩu
    ├── frmDangNhap.cs              ← Form Đăng nhập
    ├── frmDangNhap.Designer.cs
    ├── frmDangKy.cs                ← Form Đăng ký
    ├── frmDangKy.Designer.cs
    └── Properties/
        ├── Settings.settings       ← Application settings
        └── ...
```

## 🎨 Các Form hiện có

### 1. Form Đăng nhập (frmDangNhap)
- **Chức năng**: Đăng nhập vào hệ thống
- **Validation**:
  - Username: Bắt buộc
  - Password: Bắt buộc
- **Tính năng**:
  - Ghi nhớ đăng nhập (Remember Me)
  - Giới hạn số lần thử đăng nhập (5 lần)
  - Khóa tài khoản tạm thời sau nhiều lần sai
  - Kiểm tra trạng thái tài khoản (Active/Locked)

### 2. Form Đăng ký (frmDangKy)
- **Chức năng**: Tạo tài khoản mới
- **Validation**:
  - Username: 3-50 ký tự, không trùng
  - Password: 6-100 ký tự
  - Xác nhận mật khẩu: Phải khớp
  - Họ và tên: Bắt buộc
  - Email: Định dạng hợp lệ, không trùng
  - Đồng ý điều khoản: Phải tích chọn
- **Tính năng**:
  - Kiểm tra Username/Email đã tồn tại
  - Hash mật khẩu tự động (SHA256 + Salt)
  - Tự động điền Username vào form Đăng nhập sau khi đăng ký thành công

## 🔧 Các Helper Classes

### PasswordHelper.cs
- **Chức năng**: Hash và verify mật khẩu
- **Thuật toán**: SHA256 với Salt cố định
- **Methods**:
  - `HashPassword(string password)`: Hash mật khẩu
  - `VerifyPassword(string password, string hash)`: Xác thực mật khẩu

### DatabaseHelper.cs
- **Chức năng**: Kết nối và thực thi SQL
- **Methods**:
  - `ExecuteQuery(string query)`: Thực thi query trả về DataTable
  - `ExecuteNonQuery(string query)`: Thực thi query không trả về dữ liệu
  - `ExecuteStoredProcedure(...)`: Thực thi stored procedure

## 📊 Database Schema

### Bảng Users
- `Id` (INT, PRIMARY KEY)
- `Username` (NVARCHAR(50), UNIQUE)
- `PasswordHash` (NVARCHAR(255))
- `FullName` (NVARCHAR(100))
- `Email` (NVARCHAR(100), UNIQUE)
- `CreatedDate` (DATETIME)
- `LastLoginDate` (DATETIME)
- `IsActive` (BIT)

### Bảng Tasks
- `Id` (INT, PRIMARY KEY)
- `Title` (NVARCHAR(200))
- `Description` (NVARCHAR(MAX))
- `UserId` (INT, FOREIGN KEY)
- `Priority` (NVARCHAR(20))
- `Status` (NVARCHAR(20))
- `Category` (NVARCHAR(20))
- `DueDate` (DATETIME)
- `CreatedDate` (DATETIME)
- `CompletedDate` (DATETIME)
- `IsDeleted` (BIT)

### Bảng TaskHistory
- `Id` (INT, PRIMARY KEY)
- `TaskId` (INT, FOREIGN KEY)
- `Action` (NVARCHAR(50))
- `OldStatus` (NVARCHAR(20))
- `NewStatus` (NVARCHAR(20))
- `Notes` (NVARCHAR(500))
- `ActionDate` (DATETIME)
- `UserId` (INT, FOREIGN KEY)

## 🔐 Stored Procedures

### User Management
- `sp_UserLogin`: Đăng nhập
- `sp_UserRegister`: Đăng ký
- `sp_GetUserById`: Lấy thông tin user
- `sp_UpdateUser`: Cập nhật thông tin user
- `sp_ChangePassword`: Đổi mật khẩu

### Task Management
- `sp_GetTasksByFilter`: Lấy danh sách công việc theo bộ lọc
- `sp_GetTaskById`: Lấy chi tiết công việc
- `sp_GetTaskHistory`: Lấy lịch sử công việc
- `sp_DeleteTask`: Xóa công việc
- `sp_SearchTasks`: Tìm kiếm công việc

### Statistics
- `sp_GetDashboardStats`: Thống kê dashboard

## ✅ Checklist Setup

- [ ] Visual Studio 2022 (hoặc 2019+) đã cài đặt
- [ ] SQL Server đã cài đặt và đang chạy
- [ ] Database `QuanLyCongViec` đã được tạo (chạy CreateDatabase.sql)
- [ ] Stored Procedures đã được tạo (chạy AdditionalProcedures.sql)
- [ ] Connection string đã được cấu hình trong App.config
- [ ] Project build thành công
- [ ] Form Đăng nhập hiển thị
- [ ] Đã tạo tài khoản Users qua form Đăng ký
- [ ] Đăng nhập thành công

## 🔧 Xử lý lỗi

### Lỗi: "Cannot open database"
→ Chạy `Database/CreateDatabase.sql`

### Lỗi: "Could not find stored procedure 'sp_UserLogin'"
→ Chạy `Database/AdditionalProcedures.sql`

### Lỗi: "Login failed"
→ Kiểm tra connection string trong `App.config`
→ Đảm bảo SQL Server đang chạy
→ Thử đổi sang Windows Authentication

### Lỗi: "ConfigurationManager not found"
→ Rebuild project (Build → Rebuild Solution)

### Lỗi: "Tên đăng nhập hoặc mật khẩu không đúng"
→ Đảm bảo đã tạo tài khoản qua form Đăng ký
→ Kiểm tra Username và Password đã nhập đúng

### Lỗi: "Tài khoản đã tồn tại" khi đăng ký
→ Username hoặc Email đã được sử dụng
→ Thử Username/Email khác

## 🔒 Bảo mật

- ✅ Mật khẩu được hash bằng SHA256 + Salt trước khi lưu vào database
- ✅ Không lưu mật khẩu dạng plain text
- ✅ Username và Email phải unique (không trùng lặp)
- ✅ Form Đăng nhập có giới hạn số lần thử (5 lần)
- ✅ Tài khoản bị khóa tạm thời sau nhiều lần đăng nhập sai
- ✅ Validation đầy đủ trên cả client và server

## 📞 Hỗ trợ

Nếu gặp vấn đề:
1. Đọc `SETUP_INSTRUCTIONS.txt` để biết hướng dẫn chi tiết
2. Kiểm tra SQL Server đang chạy
3. Kiểm tra connection string trong `App.config`
4. Kiểm tra database đã được tạo
5. Kiểm tra stored procedures đã được tạo
6. Đảm bảo đã tạo tài khoản Users qua form Đăng ký

---

**Chúc bạn setup thành công! 🎉**
