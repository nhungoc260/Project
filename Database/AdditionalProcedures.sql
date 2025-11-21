-- =============================================
-- Script bổ sung các Stored Procedures cần thiết
-- Chạy sau khi chạy CreateDatabase.sql
-- =============================================

USE QuanLyCongViec;
GO

-- =============================================
-- USER MANAGEMENT PROCEDURES
-- =============================================

-- SP: Đăng nhập người dùng
IF OBJECT_ID('sp_UserLogin', 'P') IS NOT NULL
    DROP PROCEDURE sp_UserLogin;
GO

CREATE PROCEDURE sp_UserLogin
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @UserId INT;
    
    SELECT 
        @UserId = Id
    FROM Users
    WHERE Username = @Username 
        AND PasswordHash = @PasswordHash
        AND IsActive = 1;
    
    IF @UserId IS NOT NULL
    BEGIN
        -- Cập nhật LastLoginDate
        UPDATE Users SET LastLoginDate = GETDATE() WHERE Id = @UserId;
        
        SELECT 
            Id,
            Username,
            FullName,
            Email,
            CreatedDate
        FROM Users
        WHERE Id = @UserId;
        
        RETURN 1;
    END
    ELSE
    BEGIN
        RETURN 0;
    END
END;
GO

-- SP: Đăng ký người dùng mới
IF OBJECT_ID('sp_UserRegister', 'P') IS NOT NULL
    DROP PROCEDURE sp_UserRegister;
GO

CREATE PROCEDURE sp_UserRegister
    @Username NVARCHAR(50),
    @PasswordHash NVARCHAR(255),
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100),
    @UserId INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
    BEGIN
        SET @UserId = -1;
        RETURN;
    END
    
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email)
    BEGIN
        SET @UserId = -2;
        RETURN;
    END
    
    INSERT INTO Users (Username, PasswordHash, FullName, Email, CreatedDate)
    VALUES (@Username, @PasswordHash, @FullName, @Email, GETDATE());
    
    SET @UserId = SCOPE_IDENTITY();
END;
GO

-- SP: Lấy thông tin người dùng theo ID
IF OBJECT_ID('sp_GetUserById', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetUserById;
GO

CREATE PROCEDURE sp_GetUserById
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        Id,
        Username,
        FullName,
        Email,
        CreatedDate
    FROM Users
    WHERE Id = @UserId AND IsActive = 1;
END;
GO

-- SP: Cập nhật thông tin người dùng
IF OBJECT_ID('sp_UpdateUser', 'P') IS NOT NULL
    DROP PROCEDURE sp_UpdateUser;
GO

CREATE PROCEDURE sp_UpdateUser
    @UserId INT,
    @FullName NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF EXISTS (SELECT 1 FROM Users WHERE Email = @Email AND Id != @UserId)
    BEGIN
        RAISERROR('Email đã được sử dụng bởi người dùng khác', 16, 1);
        RETURN;
    END
    
    UPDATE Users
    SET FullName = @FullName, Email = @Email
    WHERE Id = @UserId;
END;
GO

-- SP: Đổi mật khẩu
IF OBJECT_ID('sp_ChangePassword', 'P') IS NOT NULL
    DROP PROCEDURE sp_ChangePassword;
GO

CREATE PROCEDURE sp_ChangePassword
    @UserId INT,
    @OldPasswordHash NVARCHAR(255),
    @NewPasswordHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM Users WHERE Id = @UserId AND PasswordHash = @OldPasswordHash)
    BEGIN
        RAISERROR('Mật khẩu cũ không đúng', 16, 1);
        RETURN;
    END
    
    UPDATE Users SET PasswordHash = @NewPasswordHash WHERE Id = @UserId;
END;
GO

-- =============================================
-- TASK MANAGEMENT PROCEDURES
-- =============================================

-- SP: Lấy tasks theo bộ lọc
IF OBJECT_ID('sp_GetTasksByFilter', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTasksByFilter;
GO

CREATE PROCEDURE sp_GetTasksByFilter
    @UserId INT = NULL,
    @Status NVARCHAR(20) = NULL,
    @Priority NVARCHAR(20) = NULL,
    @Category NVARCHAR(20) = NULL,
    @SearchTitle NVARCHAR(200) = NULL,
    @IsOverdue BIT = NULL,
    @IsDueSoon BIT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.*,
        u.FullName AS UserFullName
    FROM Tasks t
    INNER JOIN Users u ON t.UserId = u.Id
    WHERE 
        t.IsDeleted = 0
        AND (@UserId IS NULL OR t.UserId = @UserId)
        AND (@Status IS NULL OR t.Status = @Status)
        AND (@Priority IS NULL OR t.Priority = @Priority)
        AND (@Category IS NULL OR t.Category = @Category)
        AND (@SearchTitle IS NULL OR t.Title LIKE '%' + @SearchTitle + '%')
        AND (@IsOverdue IS NULL OR 
             (@IsOverdue = 1 AND t.Status != 'Done' AND t.DueDate < CAST(GETDATE() AS DATE)) OR
             (@IsOverdue = 0 AND NOT (t.Status != 'Done' AND t.DueDate < CAST(GETDATE() AS DATE))))
        AND (@IsDueSoon IS NULL OR 
             (@IsDueSoon = 1 AND t.Status != 'Done' AND t.DueDate <= DATEADD(DAY, 3, CAST(GETDATE() AS DATE))) OR
             (@IsDueSoon = 0 AND NOT (t.Status != 'Done' AND t.DueDate <= DATEADD(DAY, 3, CAST(GETDATE() AS DATE)))))
    ORDER BY 
        CASE WHEN t.Priority = 'High' THEN 1 
             WHEN t.Priority = 'Medium' THEN 2 
             ELSE 3 END,
        t.DueDate ASC;
END;
GO

-- SP: Lấy task theo ID
IF OBJECT_ID('sp_GetTaskById', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTaskById;
GO

CREATE PROCEDURE sp_GetTaskById
    @TaskId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.*,
        u.FullName AS UserFullName,
        u.Email AS UserEmail
    FROM Tasks t
    INNER JOIN Users u ON t.UserId = u.Id
    WHERE t.Id = @TaskId AND t.IsDeleted = 0;
END;
GO

-- SP: Lấy lịch sử thay đổi của task
IF OBJECT_ID('sp_GetTaskHistory', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetTaskHistory;
GO

CREATE PROCEDURE sp_GetTaskHistory
    @TaskId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        th.*,
        u.FullName AS UserFullName
    FROM TaskHistory th
    INNER JOIN Users u ON th.UserId = u.Id
    WHERE th.TaskId = @TaskId
    ORDER BY th.ActionDate DESC;
END;
GO

-- SP: Xóa task (soft delete)
IF OBJECT_ID('sp_DeleteTask', 'P') IS NOT NULL
    DROP PROCEDURE sp_DeleteTask;
GO

CREATE PROCEDURE sp_DeleteTask
    @TaskId INT,
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    IF NOT EXISTS (SELECT 1 FROM Tasks WHERE Id = @TaskId AND UserId = @UserId)
    BEGIN
        RAISERROR('Bạn không có quyền xóa công việc này', 16, 1);
        RETURN;
    END
    
    UPDATE Tasks
    SET IsDeleted = 1, DeletedDate = GETDATE()
    WHERE Id = @TaskId;
    
    INSERT INTO TaskHistory (TaskId, Action, OldStatus, NewStatus, Notes, UserId)
    SELECT Id, 'Deleted', Status, NULL, 'Công việc đã bị xóa', @UserId
    FROM Tasks WHERE Id = @TaskId;
END;
GO

-- SP: Tìm kiếm tasks
IF OBJECT_ID('sp_SearchTasks', 'P') IS NOT NULL
    DROP PROCEDURE sp_SearchTasks;
GO

CREATE PROCEDURE sp_SearchTasks
    @UserId INT,
    @SearchTerm NVARCHAR(200)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        t.*,
        u.FullName AS UserFullName
    FROM Tasks t
    INNER JOIN Users u ON t.UserId = u.Id
    WHERE t.UserId = @UserId
        AND t.IsDeleted = 0
        AND (t.Title LIKE '%' + @SearchTerm + '%' OR t.Description LIKE '%' + @SearchTerm + '%')
    ORDER BY 
        CASE WHEN t.Priority = 'High' THEN 1 
             WHEN t.Priority = 'Medium' THEN 2 
             ELSE 3 END,
        t.DueDate ASC;
END;
GO

-- =============================================
-- STATISTICS PROCEDURES
-- =============================================

-- SP: Lấy thống kê tổng quan cho dashboard
IF OBJECT_ID('sp_GetDashboardStats', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetDashboardStats;
GO

CREATE PROCEDURE sp_GetDashboardStats
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT 
        COUNT(*) AS TotalTasks,
        SUM(CASE WHEN Status = 'Todo' THEN 1 ELSE 0 END) AS TodoCount,
        SUM(CASE WHEN Status = 'Doing' THEN 1 ELSE 0 END) AS DoingCount,
        SUM(CASE WHEN Status = 'Done' THEN 1 ELSE 0 END) AS DoneCount,
        SUM(CASE WHEN Status != 'Done' AND DueDate < CAST(GETDATE() AS DATE) THEN 1 ELSE 0 END) AS OverdueCount,
        SUM(CASE WHEN Status != 'Done' AND DueDate <= DATEADD(DAY, 3, CAST(GETDATE() AS DATE)) THEN 1 ELSE 0 END) AS DueSoonCount,
        CASE 
            WHEN COUNT(*) > 0 
            THEN CAST(SUM(CASE WHEN Status = 'Done' THEN 1 ELSE 0 END) * 100.0 / COUNT(*) AS DECIMAL(5,2))
            ELSE 0 
        END AS CompletionRate
    FROM Tasks
    WHERE UserId = @UserId AND IsDeleted = 0;
END;
GO

PRINT 'Các Stored Procedures bổ sung đã được tạo thành công!';
GO

