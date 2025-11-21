-- =============================================
-- Script chèn dữ liệu mẫu cho Database
-- Chạy sau khi tạo database để test
-- =============================================

USE QuanLyCongViec;
GO

-- Xóa dữ liệu cũ (nếu có)
DELETE FROM TaskHistory;
DELETE FROM Tasks;
DELETE FROM Users;
GO

-- Reset identity
DBCC CHECKIDENT ('Users', RESEED, 0);
DBCC CHECKIDENT ('Tasks', RESEED, 0);
DBCC CHECKIDENT ('TaskHistory', RESEED, 0);
GO

-- =============================================
-- 1. Chèn dữ liệu Users
-- =============================================
-- Lưu ý: Mật khẩu mẫu là "123456" đã được hash
-- Trong thực tế, bạn cần hash mật khẩu bằng BCrypt hoặc tương tự
INSERT INTO Users (Username, PasswordHash, FullName, Email, CreatedDate, IsActive)
VALUES 
    ('admin', '$2a$11$KIXx...', N'Quản trị viên', 'admin@example.com', DATEADD(DAY, -30, GETDATE()), 1),
    ('nguyenvana', '$2a$11$KIXx...', N'Nguyễn Văn A', 'nguyenvana@example.com', DATEADD(DAY, -20, GETDATE()), 1),
    ('tranthib', '$2a$11$KIXx...', N'Trần Thị B', 'tranthib@example.com', DATEADD(DAY, -15, GETDATE()), 1),
    ('levanc', '$2a$11$KIXx...', N'Lê Văn C', 'levanc@example.com', DATEADD(DAY, -10, GETDATE()), 1);
GO

-- =============================================
-- 2. Chèn dữ liệu Tasks
-- =============================================
DECLARE @UserId1 INT = (SELECT Id FROM Users WHERE Username = 'nguyenvana');
DECLARE @UserId2 INT = (SELECT Id FROM Users WHERE Username = 'tranthib');
DECLARE @UserId3 INT = (SELECT Id FROM Users WHERE Username = 'levanc');

-- Công việc của User 1
INSERT INTO Tasks (Title, Description, UserId, Priority, Status, Category, DueDate, CreatedDate, CompletedDate, IsDeleted)
VALUES 
    (N'Hoàn thành báo cáo tuần', N'Viết báo cáo tổng kết công việc tuần này', @UserId1, 'High', 'Doing', 'Work', DATEADD(DAY, 2, GETDATE()), DATEADD(DAY, -5, GETDATE()), NULL, 0),
    (N'Review code cho dự án mới', N'Kiểm tra và review code của team', @UserId1, 'High', 'Todo', 'Work', DATEADD(DAY, 5, GETDATE()), DATEADD(DAY, -3, GETDATE()), NULL, 0),
    (N'Học React Native', N'Hoàn thành khóa học React Native cơ bản', @UserId1, 'Medium', 'Doing', 'Study', DATEADD(DAY, 10, GETDATE()), DATEADD(DAY, -7, GETDATE()), NULL, 0),
    (N'Mua sắm cuối tuần', N'Đi siêu thị mua đồ dùng cá nhân', @UserId1, 'Low', 'Todo', 'Personal', DATEADD(DAY, 1, GETDATE()), DATEADD(DAY, -1, GETDATE()), NULL, 0),
    (N'Hoàn thành task đã xong', N'Task này đã hoàn thành', @UserId1, 'Medium', 'Done', 'Work', DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -3, GETDATE()), 0),
    (N'Task quá hạn', N'Task này đã quá hạn', @UserId1, 'High', 'Todo', 'Work', DATEADD(DAY, -2, GETDATE()), DATEADD(DAY, -10, GETDATE()), NULL, 0);

-- Công việc của User 2
INSERT INTO Tasks (Title, Description, UserId, Priority, Status, Category, DueDate, CreatedDate, CompletedDate, IsDeleted)
VALUES 
    (N'Thiết kế giao diện mới', N'Thiết kế UI/UX cho ứng dụng mobile', @UserId2, 'High', 'Doing', 'Work', DATEADD(DAY, 3, GETDATE()), DATEADD(DAY, -4, GETDATE()), NULL, 0),
    (N'Đọc sách về quản lý thời gian', N'Đọc cuốn "Getting Things Done"', @UserId2, 'Low', 'Todo', 'Personal', DATEADD(DAY, 15, GETDATE()), DATEADD(DAY, -2, GETDATE()), NULL, 0),
    (N'Làm bài tập toán', N'Hoàn thành bài tập chương 5', @UserId2, 'Medium', 'Done', 'Study', DATEADD(DAY, -1, GETDATE()), DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, -1, GETDATE()), 0),
    (N'Chuẩn bị presentation', N'Chuẩn bị slide thuyết trình cho meeting', @UserId2, 'High', 'Todo', 'Work', DATEADD(DAY, 1, GETDATE()), DATEADD(DAY, -6, GETDATE()), NULL, 0);

-- Công việc của User 3
INSERT INTO Tasks (Title, Description, UserId, Priority, Status, Category, DueDate, CreatedDate, CompletedDate, IsDeleted)
VALUES 
    (N'Fix bug đăng nhập', N'Sửa lỗi không đăng nhập được trên iOS', @UserId3, 'High', 'Doing', 'Work', DATEADD(DAY, 1, GETDATE()), DATEADD(DAY, -3, GETDATE()), NULL, 0),
    (N'Học tiếng Anh', N'Luyện nghe tiếng Anh 30 phút mỗi ngày', @UserId3, 'Medium', 'Doing', 'Study', DATEADD(DAY, 7, GETDATE()), DATEADD(DAY, -5, GETDATE()), NULL, 0),
    (N'Đi khám sức khỏe', N'Đặt lịch khám sức khỏe định kỳ', @UserId3, 'Medium', 'Todo', 'Personal', DATEADD(DAY, 14, GETDATE()), DATEADD(DAY, -2, GETDATE()), NULL, 0);
GO

-- Hiển thị thông tin sau khi chèn dữ liệu
DECLARE @UserCount INT;
DECLARE @TaskCount INT;

SELECT @UserCount = COUNT(*) FROM Users;
SELECT @TaskCount = COUNT(*) FROM Tasks;

PRINT 'Dữ liệu mẫu đã được chèn thành công!';
PRINT 'Số lượng Users: ' + CAST(@UserCount AS VARCHAR);
PRINT 'Số lượng Tasks: ' + CAST(@TaskCount AS VARCHAR);
GO

