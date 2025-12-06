-- =============================================
-- Script chèn dữ liệu mẫu (Tasks) vào database
-- Chạy SAU KHI đã tạo tài khoản Users qua form Đăng ký
-- =============================================

USE QuanLyCongViec;
GO

-- =============================================
-- LƯU Ý QUAN TRỌNG:
-- =============================================
-- 1. Bạn CẦN tạo tài khoản Users trước qua form Đăng ký
-- 2. Script này chỉ chèn dữ liệu Tasks (công việc)
-- 3. Nếu chưa có Users, script sẽ không chèn được Tasks

-- =============================================
-- Xóa dữ liệu Tasks cũ (nếu có)
-- =============================================
DELETE FROM TaskHistory;
DELETE FROM Tasks;
GO

-- Reset identity cho Tasks
DBCC CHECKIDENT ('Tasks', RESEED, 0);
DBCC CHECKIDENT ('TaskHistory', RESEED, 0);
GO

-- =============================================
-- Chèn dữ liệu Tasks
-- =============================================

-- Lấy UserId từ các tài khoản đã tạo
-- Lấy 3 user đầu tiên (nếu có)
DECLARE @UserId1 INT;
DECLARE @UserId2 INT;
DECLARE @UserId3 INT;

SELECT @UserId1 = Id FROM (SELECT Id, ROW_NUMBER() OVER (ORDER BY Id) AS RowNum FROM Users) AS T WHERE RowNum = 1;
SELECT @UserId2 = Id FROM (SELECT Id, ROW_NUMBER() OVER (ORDER BY Id) AS RowNum FROM Users) AS T WHERE RowNum = 2;
SELECT @UserId3 = Id FROM (SELECT Id, ROW_NUMBER() OVER (ORDER BY Id) AS RowNum FROM Users) AS T WHERE RowNum = 3;

-- Kiểm tra xem có Users không
IF @UserId1 IS NULL
BEGIN
    PRINT '========================================';
    PRINT 'CẢNH BÁO: Chưa có tài khoản Users nào!';
    PRINT '========================================';
    PRINT 'Vui lòng tạo tài khoản qua form Đăng ký trước!';
    PRINT 'Sau đó chạy lại script này.';
    PRINT '========================================';
    RETURN;
END

PRINT 'Đang chèn dữ liệu Tasks...';
PRINT 'User 1 ID: ' + CAST(ISNULL(@UserId1, 0) AS VARCHAR);
PRINT 'User 2 ID: ' + CAST(ISNULL(@UserId2, 0) AS VARCHAR);
PRINT 'User 3 ID: ' + CAST(ISNULL(@UserId3, 0) AS VARCHAR);
PRINT '';

-- =============================================
-- Công việc của User 1
-- =============================================
INSERT INTO Tasks (Title, Description, UserId, Priority, Status, Category, DueDate, CreatedDate, CompletedDate, IsDeleted)
VALUES 
    (N'Hoàn thành báo cáo tuần', N'Viết báo cáo tổng kết công việc tuần này', @UserId1, 'High', 'Doing', 'Work', DATEADD(DAY, 2, GETDATE()), DATEADD(DAY, -5, GETDATE()), NULL, 0),
    (N'Review code cho dự án mới', N'Kiểm tra và review code của team', @UserId1, 'High', 'Todo', 'Work', DATEADD(DAY, 5, GETDATE()), DATEADD(DAY, -3, GETDATE()), NULL, 0),
    (N'Học React Native', N'Hoàn thành khóa học React Native cơ bản', @UserId1, 'Medium', 'Doing', 'Study', DATEADD(DAY, 10, GETDATE()), DATEADD(DAY, -7, GETDATE()), NULL, 0),
    (N'Mua sắm cuối tuần', N'Đi siêu thị mua đồ dùng cá nhân', @UserId1, 'Low', 'Todo', 'Personal', DATEADD(DAY, 1, GETDATE()), DATEADD(DAY, -1, GETDATE()), NULL, 0),
    (N'Hoàn thành task đã xong', N'Task này đã hoàn thành', @UserId1, 'Medium', 'Done', 'Work', DATEADD(DAY, -5, GETDATE()), DATEADD(DAY, -20, GETDATE()), DATEADD(DAY, -3, GETDATE()), 0),
    (N'Task quá hạn', N'Task này đã quá hạn', @UserId1, 'High', 'Todo', 'Work', DATEADD(DAY, -2, GETDATE()), DATEADD(DAY, -10, GETDATE()), NULL, 0);

PRINT 'Đã chèn 6 tasks cho User 1';

-- =============================================
-- Công việc của User 2 (nếu có)
-- =============================================
IF @UserId2 IS NOT NULL
BEGIN
    INSERT INTO Tasks (Title, Description, UserId, Priority, Status, Category, DueDate, CreatedDate, CompletedDate, IsDeleted)
    VALUES 
        (N'Thiết kế giao diện mới', N'Thiết kế UI/UX cho ứng dụng mobile', @UserId2, 'High', 'Doing', 'Work', DATEADD(DAY, 3, GETDATE()), DATEADD(DAY, -4, GETDATE()), NULL, 0),
        (N'Đọc sách về quản lý thời gian', N'Đọc cuốn "Getting Things Done"', @UserId2, 'Low', 'Todo', 'Personal', DATEADD(DAY, 15, GETDATE()), DATEADD(DAY, -2, GETDATE()), NULL, 0),
        (N'Làm bài tập toán', N'Hoàn thành bài tập chương 5', @UserId2, 'Medium', 'Done', 'Study', DATEADD(DAY, -1, GETDATE()), DATEADD(DAY, -8, GETDATE()), DATEADD(DAY, -1, GETDATE()), 0),
        (N'Chuẩn bị presentation', N'Chuẩn bị slide thuyết trình cho meeting', @UserId2, 'High', 'Todo', 'Work', DATEADD(DAY, 1, GETDATE()), DATEADD(DAY, -6, GETDATE()), NULL, 0);
    
    PRINT 'Đã chèn 4 tasks cho User 2';
END

-- =============================================
-- Công việc của User 3 (nếu có)
-- =============================================
IF @UserId3 IS NOT NULL
BEGIN
    INSERT INTO Tasks (Title, Description, UserId, Priority, Status, Category, DueDate, CreatedDate, CompletedDate, IsDeleted)
    VALUES 
        (N'Fix bug đăng nhập', N'Sửa lỗi không đăng nhập được trên iOS', @UserId3, 'High', 'Doing', 'Work', DATEADD(DAY, 1, GETDATE()), DATEADD(DAY, -3, GETDATE()), NULL, 0),
        (N'Học tiếng Anh', N'Luyện nghe tiếng Anh 30 phút mỗi ngày', @UserId3, 'Medium', 'Doing', 'Study', DATEADD(DAY, 7, GETDATE()), DATEADD(DAY, -5, GETDATE()), NULL, 0),
        (N'Đi khám sức khỏe', N'Đặt lịch khám sức khỏe định kỳ', @UserId3, 'Medium', 'Todo', 'Personal', DATEADD(DAY, 14, GETDATE()), DATEADD(DAY, -2, GETDATE()), NULL, 0);
    
    PRINT 'Đã chèn 3 tasks cho User 3';
END

-- =============================================
-- Hiển thị kết quả
-- =============================================
DECLARE @TaskCount INT;
SELECT @TaskCount = COUNT(*) FROM Tasks;

PRINT '';
PRINT '========================================';
PRINT 'HOÀN THÀNH!';
PRINT '========================================';
PRINT 'Tổng số Tasks đã chèn: ' + CAST(@TaskCount AS VARCHAR);
PRINT '';
PRINT 'Dữ liệu mẫu bao gồm:';
PRINT '- Tasks với các trạng thái: Todo, Doing, Done';
PRINT '- Tasks với các độ ưu tiên: High, Medium, Low';
PRINT '- Tasks với các phân loại: Work, Personal, Study';
PRINT '- Tasks quá hạn và sắp đến hạn';
PRINT '========================================';
GO

-- Hiển thị thống kê
SELECT 
    u.Username,
    u.FullName,
    COUNT(t.Id) AS TaskCount,
    SUM(CASE WHEN t.Status = 'Todo' THEN 1 ELSE 0 END) AS TodoCount,
    SUM(CASE WHEN t.Status = 'Doing' THEN 1 ELSE 0 END) AS DoingCount,
    SUM(CASE WHEN t.Status = 'Done' THEN 1 ELSE 0 END) AS DoneCount
FROM Users u
LEFT JOIN Tasks t ON u.Id = t.UserId AND t.IsDeleted = 0
GROUP BY u.Id, u.Username, u.FullName
ORDER BY u.Id;
GO

