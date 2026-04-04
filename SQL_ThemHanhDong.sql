-- Script SQL: Thêm các hành ??ng vào b?ng tblHanhDong
-- Ch?y script này trong SQL Server Management Studio ho?c Azure Data Studio

USE [QLTB]
GO

-- Ki?m tra và thêm các hành ??ng n?u ch?a có

-- 1. Thêm m?i thi?t b?
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'Thêm m?i')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'Thêm m?i')
    PRINT N'? ?ã thêm hành ??ng: Thêm m?i'
END
ELSE
    PRINT N'? Hành ??ng "Thêm m?i" ?ã t?n t?i'

-- 2. C?p nh?t thi?t b?
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'C?p nh?t')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'C?p nh?t')
    PRINT N'? ?ã thêm hành ??ng: C?p nh?t'
END
ELSE
    PRINT N'? Hành ??ng "C?p nh?t" ?ã t?n t?i'

-- 3. Xóa thi?t b?
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'Xóa')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'Xóa')
    PRINT N'? ?ã thêm hành ??ng: Xóa'
END
ELSE
    PRINT N'? Hành ??ng "Xóa" ?ã t?n t?i'

-- 4. C?p phát (n?u ch?a có)
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'C?p phát')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'C?p phát')
    PRINT N'? ?ã thêm hành ??ng: C?p phát'
END
ELSE
    PRINT N'? Hành ??ng "C?p phát" ?ã t?n t?i'

-- 5. Thu h?i (n?u ch?a có)
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'Thu h?i')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'Thu h?i')
    PRINT N'? ?ã thêm hành ??ng: Thu h?i'
END
ELSE
    PRINT N'? Hành ??ng "Thu h?i" ?ã t?n t?i'

-- 6. ?i?u chuy?n (n?u ch?a có)
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'?i?u chuy?n')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'?i?u chuy?n')
    PRINT N'? ?ã thêm hành ??ng: ?i?u chuy?n'
END
ELSE
    PRINT N'? Hành ??ng "?i?u chuy?n" ?ã t?n t?i'

-- 7. B?o trì (n?u ch?a có)
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'B?o trì')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'B?o trì')
    PRINT N'? ?ã thêm hành ??ng: B?o trì'
END
ELSE
    PRINT N'? Hành ??ng "B?o trì" ?ã t?n t?i'

-- 8. S?a ch?a (n?u ch?a có)
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'S?a ch?a')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'S?a ch?a')
    PRINT N'? ?ã thêm hành ??ng: S?a ch?a'
END
ELSE
    PRINT N'? Hành ??ng "S?a ch?a" ?ã t?n t?i'

-- 9. Thanh lý (n?u ch?a có)
IF NOT EXISTS (SELECT 1 FROM tblHanhDong WHERE name = N'Thanh lý')
BEGIN
    INSERT INTO tblHanhDong (name) VALUES (N'Thanh lý')
    PRINT N'? ?ã thêm hành ??ng: Thanh lý'
END
ELSE
    PRINT N'? Hành ??ng "Thanh lý" ?ã t?n t?i'

-- Hi?n th? danh sách t?t c? các hành ??ng
PRINT ''
PRINT N'========================================='
PRINT N'DANH SÁCH T?T C? HÀNH ??NG:'
PRINT N'========================================='
SELECT ID, name AS [Tên hành ??ng] 
FROM tblHanhDong 
ORDER BY ID

GO
