# 📦 Project Quản Lý Công Việc - Sẵn Sàng Chia Sẻ

## 🎯 Thông tin Project

- **Tên**: Quản Lý Công Việc
- **Framework**: .NET Framework 4.7.2
- **Database**: SQL Server
- **Kiến trúc**: 3 lớp (UI, Service, Business, Data Access)

## 📋 Nội dung trong thư mục này

Thư mục này chứa **TẤT CẢ** files cần thiết để:
- ✅ Chia sẻ project cho người khác
- ✅ Setup và chạy trên máy mới
- ✅ Không cần file nào khác

## 🚀 Cách sử dụng

### Bước 1: Giải nén/Mở project
1. Copy toàn bộ thư mục `Project` vào máy mới
2. Mở Visual Studio 2022
3. Mở file `QuanLyCongViec.sln`

### Bước 2: Tạo Database
1. Mở **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**
2. Kết nối với SQL Server của bạn
3. Mở file `Database/CreateDatabase.sql`
4. Chạy script (F5)
5. Kiểm tra: Database **QuanLyCongViec** đã được tạo
6. (Tùy chọn) Chạy `Database/AdditionalProcedures.sql`

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
   - `localhost` (SQL Server mặc định)
   - `localhost\SQLEXPRESS` (SQL Server Express)
   - `(localdb)\MSSQLLocalDB` (LocalDB)
   - `TÊN_MÁY\SQLEXPRESS` (Tên máy tính của bạn)

**💡 Cách tìm server name:**
- Mở SSMS hoặc Azure Data Studio
- Xem server name trong dropdown khi kết nối
- Copy server name đó vào App.config

### Bước 4: Build và Chạy
1. Build project (Ctrl + Shift + B)
2. Chạy project (F5)
3. Nếu thấy "Kết nối database thành công!" = ✅ OK

## 📁 Cấu trúc thư mục

```
Project/
├── QuanLyCongViec.sln              ← Solution file
├── README_SHARE.md                 ← File này
├── SETUP_INSTRUCTIONS.txt          ← Hướng dẫn nhanh
├── Database/                       ← SQL Scripts
│   ├── CreateDatabase.sql          ← Script chính
│   ├── AdditionalProcedures.sql    ← Script bổ sung
│   └── SeedData.sql                ← Dữ liệu mẫu (tùy chọn)
└── QuanLyCongViec/                 ← Project code
    ├── QuanLyCongViec.csproj
    ├── App.config                  ← Connection string
    ├── DataAccess/
    │   └── DatabaseHelper.cs
    ├── Form1.cs
    └── ...
```

## ✅ Checklist Setup

- [ ] Visual Studio 2022 đã cài đặt
- [ ] SQL Server đã cài đặt và đang chạy
- [ ] Database `QuanLyCongViec` đã được tạo
- [ ] Connection string đã được cấu hình
- [ ] Project build thành công
- [ ] Test connection thành công

## 🔧 Xử lý lỗi

### Lỗi: "Cannot open database"
→ Chạy `Database/CreateDatabase.sql`

### Lỗi: "Login failed"
→ Kiểm tra connection string trong `App.config`

### Lỗi: "ConfigurationManager not found"
→ Rebuild project

Xem `SETUP_INSTRUCTIONS.txt` để biết thêm chi tiết.

## 📞 Hỗ trợ

Nếu gặp vấn đề:
1. Đọc `SETUP_INSTRUCTIONS.txt`
2. Kiểm tra SQL Server đang chạy
3. Kiểm tra connection string
4. Kiểm tra database đã được tạo

---

**Chúc bạn setup thành công! 🎉**

