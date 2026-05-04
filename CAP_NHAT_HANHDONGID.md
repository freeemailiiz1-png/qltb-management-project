# ? C?P NH?T HOÀN THI?N: S? D?NG HANHDONGID THAY VÌ CHU?I

## ?? T?NG QUAN THAY ??I

H? th?ng ?ã ???c c?p nh?t ?? s? d?ng **HanhDongID** (INT - Foreign Key) thay vì chu?i khi ghi log vào `tblLichSuHeThong`.

### **?u ?i?m:**
? **Chu?n hóa d? li?u**: Tránh l?i chính t?, nh?t quán
? **Hi?u n?ng t?t h?n**: JOIN nhanh h?n so v?i so sánh chu?i
? **D? b?o trì**: Thêm/s?a/xóa hành ??ng ? 1 n?i (b?ng tblHanhDong)
? **?a ngôn ng?**: D? dàng chuy?n ??i ngôn ng? hi?n th?

---

## ??? C?U TRÚC DATABASE C?N CÓ

### **1. B?ng tblHanhDong**

```sql
CREATE TABLE tblHanhDong (
    ID INT PRIMARY KEY IDENTITY(1,1),
    name NVARCHAR(50) NOT NULL, -- ho?c TenHanhDong
    MoTa NVARCHAR(200) NULL
)

-- Insert d? li?u m?u
INSERT INTO tblHanhDong (name) VALUES 
(N'Thêm'),
(N'S?a'),
(N'Xóa'),
(N'C?p nh?t'),
(N'C?p m?i'),
(N'Thu h?i'),
(N'?i?u chuy?n'),
(N'??ng nh?p'),
(N'??ng xu?t')
```

### **2. B?ng tblLichSuHeThong (C?P NH?T)**

```sql
-- N?u b?ng ?ã t?n t?i v?i c?t HanhDong là NVARCHAR, c?n s?a l?i:

-- B??c 1: Thêm c?t m?i HanhDongID
ALTER TABLE tblLichSuHeThong
ADD HanhDongID INT NULL

-- B??c 2: T?o Foreign Key
ALTER TABLE tblLichSuHeThong
ADD CONSTRAINT FK_LichSuHeThong_HanhDong 
FOREIGN KEY (HanhDongID) REFERENCES tblHanhDong(ID)

-- B??c 3: Migrate d? li?u c? (n?u có)
UPDATE ls
SET ls.HanhDongID = hd.ID
FROM tblLichSuHeThong ls
INNER JOIN tblHanhDong hd ON ls.HanhDong = hd.name

-- B??c 4: Xóa c?t c? (SAU KHI ?Ã MIGRATE XONG)
ALTER TABLE tblLichSuHeThong
DROP COLUMN HanhDong

-- HO?C t?o b?ng m?i t? ??u:
CREATE TABLE tblLichSuHeThong (
    ID INT PRIMARY KEY IDENTITY(1,1),
    UserID INT NULL,
    HanhDongID INT NULL,  -- ? THAY ??I: T? string thành INT
    BangTacDong NVARCHAR(50) NULL,
    BanGhiID INT NULL,
    ThoiDiem DATETIME DEFAULT GETDATE(),
    NoiDungCu NVARCHAR(MAX) NULL,
    NoiDungMoi NVARCHAR(MAX) NULL,

    CONSTRAINT FK_LichSuHeThong_User FOREIGN KEY (UserID) REFERENCES tblUser(ID),
    CONSTRAINT FK_LichSuHeThong_HanhDong FOREIGN KEY (HanhDongID) REFERENCES tblHanhDong(ID)
)
```

---

## ?? CÁC FILE ?Ã C?P NH?T

### **1. DTO/LichSuHeThong.cs** ?
```csharp
public class LichSuHeThong
{
    public int ID { get; set; }
    public int? UserID { get; set; }
    public int? HanhDongID { get; set; }  // ? ?Ã ??I: T? string thành int
    public string BangTacDong { get; set; }
    public int? BanGhiID { get; set; }
    public DateTime? ThoiDiem { get; set; }
    public string NoiDungCu { get; set; }
    public string NoiDungMoi { get; set; }

    // Thu?c tính b? sung
    public string TenUser { get; set; }
    public string TenHanhDong { get; set; }  // ? THÊM: ?? hi?n th? tên
}
```

### **2. DAO/LichSuHeThongDAO.cs** ?
- ? Thêm method `GetHanhDongID(string tenHanhDong)`
- ? C?p nh?t `GetAll()`: JOIN v?i `tblHanhDong`, SELECT `TenHanhDong`
- ? C?p nh?t `GetByUser()`: JOIN v?i `tblHanhDong`
- ? C?p nh?t `GetByTable()`: JOIN v?i `tblHanhDong`
- ? C?p nh?t `Insert()`: Dùng `HanhDongID` thay vì `HanhDong`

### **3. DAO/ThietBiDAO.cs** ?
- ? Thêm `HanhDongDAO` instance
- ? Thêm method `GetHanhDongID(string tenHanhDong)`
- ? **Insert()**: Dùng `GetHanhDongID("Thêm")`
- ? **Update()**: Dùng `GetHanhDongID("S?a")`
- ? **Delete()**: Dùng `GetHanhDongID("Xóa")`

### **4. DAO/UserDAO.cs** ?
- ? Thêm `HanhDongDAO` instance
- ? Thêm method `GetHanhDongID(string tenHanhDong)`
- ? **AddUser()**: Dùng `GetHanhDongID("Thêm")`
- ? **UpdateUser()**: Dùng `GetHanhDongID("S?a")`
- ? **DeleteUser()**: Dùng `GetHanhDongID("Xóa")`

### **5. DAO/DonViDAO.cs** ?
- ? Thêm `HanhDongDAO` instance
- ? Thêm method `GetHanhDongID(string tenHanhDong)`
- ? **Insert()**: Dùng `GetHanhDongID("Thêm")`
- ? **Update()**: Dùng `GetHanhDongID("S?a")`
- ? **Delete()**: Dùng `GetHanhDongID("Xóa")`

### **6. QLLichSuHeThong.cs** ?
- ? ??i t?t c? `ls.HanhDong` thành `ls.TenHanhDong`
- ? ?n c?t `HanhDongID` trong DataGridView
- ? Hi?n th? c?t `TenHanhDong` v?i header "Hành ??ng"

### **7. PopupThietBiInfo.cs** ?
- ? Import `using QuanLyThietBi.Common;`
- ? Thêm `UserID = SessionManager.GetCurrentUserID()` khi t?o ??i t??ng ThietBi

### **8. QLThietBi.cs** ?
- ? Truy?n `SessionManager.GetCurrentUserID()` vào `Delete()`

---

## ?? QUY TRÌNH GHI LOG

### **Khi Insert (Thêm m?i):**
```csharp
// 1. Thêm d? li?u vào b?ng chính
int newID = ...; // L?y ID v?a insert

// 2. Ghi log
lichSuHeThongDAO.Insert(new LichSuHeThong
{
    UserID = SessionManager.GetCurrentUserID(),
    HanhDongID = GetHanhDongID("Thêm"), // ? L?y ID t? tblHanhDong
    BangTacDong = "tblThietBi",
    BanGhiID = newID,
    ThoiDiem = DateTime.Now,
    NoiDungCu = null,
    NoiDungMoi = "Chi ti?t b?n ghi m?i"
});
```

### **Khi Update (S?a):**
```csharp
// 1. L?y d? li?u c?
var old = GetByID(id);

// 2. Update d? li?u

// 3. Ghi log
lichSuHeThongDAO.Insert(new LichSuHeThong
{
    UserID = SessionManager.GetCurrentUserID(),
    HanhDongID = GetHanhDongID("S?a"), // ? L?y ID
    BangTacDong = "tblThietBi",
    BanGhiID = id,
    ThoiDiem = DateTime.Now,
    NoiDungCu = "Thông tin c?",
    NoiDungMoi = "Thông tin m?i"
});
```

### **Khi Delete (Xóa m?m):**
```csharp
// 1. L?y d? li?u tr??c khi xóa
var old = GetByID(id);

// 2. Update TrangThai = "Xóa"

// 3. Ghi log
lichSuHeThongDAO.Insert(new LichSuHeThong
{
    UserID = SessionManager.GetCurrentUserID(),
    HanhDongID = GetHanhDongID("Xóa"), // ? L?y ID
    BangTacDong = "tblThietBi",
    BanGhiID = id,
    ThoiDiem = DateTime.Now,
    NoiDungCu = "Thông tin ?ã xóa",
    NoiDungMoi = "?ã xóa"
});
```

---

## ?? DANH SÁCH HÀNH ??NG CHU?N

??m b?o b?ng `tblHanhDong` có các b?n ghi sau:

| ID | name | Mô t? |
|----|------|-------|
| 1 | Thêm | Thêm m?i b?n ghi |
| 2 | S?a | C?p nh?t thông tin |
| 3 | Xóa | Xóa b?n ghi (soft delete) |
| 4 | C?p m?i | C?p thi?t b? m?i cho cán b? |
| 5 | Thu h?i | Thu h?i thi?t b? t? cán b? |
| 6 | ?i?u chuy?n | Chuy?n thi?t b? gi?a các ??n v? |
| 7 | ??ng nh?p | User ??ng nh?p h? th?ng |
| 8 | ??ng xu?t | User ??ng xu?t |

---

## ?? KI?M TRA SAU KHI C?P NH?T

### **Test 1: Ki?m tra b?ng tblHanhDong**
```sql
SELECT * FROM tblHanhDong ORDER BY ID
```
K?t qu? ph?i có ít nh?t 3 b?n ghi: Thêm, S?a, Xóa

### **Test 2: Ki?m tra c?u trúc tblLichSuHeThong**
```sql
SELECT COLUMN_NAME, DATA_TYPE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'tblLichSuHeThong' AND COLUMN_NAME IN ('HanhDong', 'HanhDongID')
```
K?t qu? ph?i có c?t `HanhDongID INT`, KHÔNG có c?t `HanhDong NVARCHAR`

### **Test 3: Thêm thi?t b? m?i**
1. ??ng nh?p vào h? th?ng
2. Vào QLThietBi ? Click "Thêm"
3. Nh?p thông tin ? L?u
4. Ki?m tra log:
```sql
SELECT TOP 1 
    ls.ID,
    u.TenDangNhap,
    hd.name AS TenHanhDong,
    ls.BangTacDong,
    ls.BanGhiID,
    ls.NoiDungMoi
FROM tblLichSuHeThong ls
LEFT JOIN tblUser u ON ls.UserID = u.ID
LEFT JOIN tblHanhDong hd ON ls.HanhDongID = hd.ID
WHERE ls.BangTacDong = 'tblThietBi'
ORDER BY ls.ThoiDiem DESC
```

K?t qu? mong ??i:
- `TenHanhDong` = "Thêm"
- `BangTacDong` = "tblThietBi"
- `NoiDungMoi` = "Serial: ..., CanBoID: ..., DonViID: ..."

### **Test 4: S?a thi?t b?**
1. Ch?n 1 thi?t b? ? Click "S?a"
2. Thay ??i Cán b? ho?c ??n v?
3. L?u
4. Ki?m tra log v?i query t??ng t?, `TenHanhDong` = "S?a"

### **Test 5: Xóa thi?t b?**
1. Ch?n 1 thi?t b? ? Click "Xóa"
2. Xác nh?n xóa
3. Ki?m tra log v?i query t??ng t?, `TenHanhDong` = "Xóa"

---

## ?? L?U Ý QUAN TR?NG

### **1. Tên hành ??ng trong tblHanhDong ph?i kh?p v?i code**

Trong các DAO, chúng ta ?ang g?i:
- `GetHanhDongID("Thêm")`
- `GetHanhDongID("S?a")`
- `GetHanhDongID("Xóa")`

??m b?o trong b?ng `tblHanhDong` có **?úng** các giá tr? này (phân bi?t hoa th??ng, d?u).

### **2. N?u không tìm th?y HanhDongID**

Method `GetHanhDongID()` s? tr? v? `NULL` n?u không tìm th?y. ?i?u này không gây l?i nh?ng log s? thi?u thông tin.

**Kh?c ph?c:** ??m b?o có ?? d? li?u trong `tblHanhDong`.

### **3. Case-sensitive**

Code hi?n t?i s? d?ng `StringComparison.OrdinalIgnoreCase`, ngh?a là **KHÔNG phân bi?t** hoa/th??ng:
- "Thêm" = "thêm" = "THÊM" ?

Nh?ng **V?N PHÂN BI?T D?U**:
- "Thêm" ? "Them" ?
- "S?a" ? "Sua" ?
- "Xóa" ? "Xoa" ?

### **4. Tên c?t trong database**

Trong code, chúng ta ?ang dùng:
```csharp
hd.name != null && hd.name.Equals(tenHanhDong, ...)
```

N?u c?t trong database là `TenHanhDong` thay vì `name`, c?n c?p nh?t `HanhDongDAO`:
```sql
SELECT ID, TenHanhDong AS name FROM tblHanhDong
-- ho?c
SELECT ID, name FROM tblHanhDong
```

---

## ?? MAPPING HÀNH ??NG

| Code | Tên trong tblHanhDong | Mô t? |
|------|----------------------|-------|
| `GetHanhDongID("Thêm")` | Thêm | Thêm b?n ghi m?i |
| `GetHanhDongID("S?a")` | S?a | C?p nh?t thông tin |
| `GetHanhDongID("Xóa")` | Xóa | Xóa m?m b?n ghi |
| `GetHanhDongID("C?p m?i")` | C?p m?i | C?p thi?t b? cho cán b? |
| `GetHanhDongID("Thu h?i")` | Thu h?i | Thu h?i thi?t b? |
| `GetHanhDongID("?i?u chuy?n")` | ?i?u chuy?n | Chuy?n thi?t b? gi?a ??n v? |

---

## ?? SQL SCRIPT ??Y ?? ?? SETUP

```sql
-- =============================================
-- SCRIPT SETUP DATABASE CHO L?CH S? H? TH?NG
-- =============================================

-- 1. T?o/C?p nh?t b?ng tblHanhDong
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tblHanhDong')
BEGIN
    CREATE TABLE tblHanhDong (
        ID INT PRIMARY KEY IDENTITY(1,1),
        name NVARCHAR(50) NOT NULL UNIQUE,
        MoTa NVARCHAR(200) NULL
    )

    -- Insert d? li?u m?u
    INSERT INTO tblHanhDong (name, MoTa) VALUES 
    (N'Thêm', N'Thêm m?i b?n ghi'),
    (N'S?a', N'C?p nh?t thông tin'),
    (N'Xóa', N'Xóa m?m b?n ghi'),
    (N'C?p m?i', N'C?p thi?t b? m?i'),
    (N'Thu h?i', N'Thu h?i thi?t b?'),
    (N'?i?u chuy?n', N'Chuy?n thi?t b? gi?a ??n v?'),
    (N'??ng nh?p', N'??ng nh?p h? th?ng'),
    (N'??ng xu?t', N'??ng xu?t kh?i h? th?ng')
END

-- 2. Ki?m tra xem b?ng tblLichSuHeThong có c?t HanhDong (string) không
IF EXISTS (
    SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
    WHERE TABLE_NAME = 'tblLichSuHeThong' 
    AND COLUMN_NAME = 'HanhDong' 
    AND DATA_TYPE LIKE '%char%'
)
BEGIN
    PRINT N'Phát hi?n c?t HanhDong ki?u string, c?n migrate sang HanhDongID...'

    -- Thêm c?t HanhDongID n?u ch?a có
    IF NOT EXISTS (
        SELECT * FROM INFORMATION_SCHEMA.COLUMNS 
        WHERE TABLE_NAME = 'tblLichSuHeThong' AND COLUMN_NAME = 'HanhDongID'
    )
    BEGIN
        ALTER TABLE tblLichSuHeThong ADD HanhDongID INT NULL
    END

    -- Migrate d? li?u
    UPDATE ls
    SET ls.HanhDongID = hd.ID
    FROM tblLichSuHeThong ls
    INNER JOIN tblHanhDong hd ON ls.HanhDong = hd.name

    PRINT N'?ã migrate d? li?u. Vui lòng ki?m tra và xóa c?t HanhDong c? th? công n?u c?n.'
    PRINT N'SQL: ALTER TABLE tblLichSuHeThong DROP COLUMN HanhDong'
END
ELSE IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'tblLichSuHeThong')
BEGIN
    -- T?o b?ng m?i
    CREATE TABLE tblLichSuHeThong (
        ID INT PRIMARY KEY IDENTITY(1,1),
        UserID INT NULL,
        HanhDongID INT NULL,
        BangTacDong NVARCHAR(50) NULL,
        BanGhiID INT NULL,
        ThoiDiem DATETIME DEFAULT GETDATE(),
        NoiDungCu NVARCHAR(MAX) NULL,
        NoiDungMoi NVARCHAR(MAX) NULL,

        CONSTRAINT FK_LichSuHeThong_User FOREIGN KEY (UserID) REFERENCES tblUser(ID),
        CONSTRAINT FK_LichSuHeThong_HanhDong FOREIGN KEY (HanhDongID) REFERENCES tblHanhDong(ID)
    )

    PRINT N'?ã t?o b?ng tblLichSuHeThong m?i'
END
ELSE
BEGIN
    PRINT N'B?ng tblLichSuHeThong ?ã t?n t?i và có c?u trúc ?úng'
END

-- 3. Xem k?t qu?
SELECT 
    ls.ID,
    u.TenDangNhap AS [User],
    hd.name AS [Hành ??ng],
    ls.BangTacDong AS [B?ng],
    ls.BanGhiID AS [ID b?n ghi],
    ls.ThoiDiem AS [Th?i ?i?m],
    ls.NoiDungMoi AS [N?i dung]
FROM tblLichSuHeThong ls
LEFT JOIN tblUser u ON ls.UserID = u.ID
LEFT JOIN tblHanhDong hd ON ls.HanhDongID = hd.ID
ORDER BY ls.ThoiDiem DESC
```

---

## ? HOÀN THÀNH

H? th?ng ?ã ???c c?p nh?t hoàn ch?nh ??:
1. ? S? d?ng `HanhDongID` (INT) thay vì chu?i
2. ? T? ??ng l?y ID t? b?ng `tblHanhDong`
3. ? JOIN ?? hi?n th? tên hành ??ng
4. ? Ghi log ??y ?? cho Insert/Update/Delete

**B??c ti?p theo:**
1. Ch?y SQL script ? trên ?? setup database
2. Restart ?ng d?ng
3. Test các ch?c n?ng Thêm/S?a/Xóa
4. Ki?m tra log trong b?ng `tblLichSuHeThong`

Chúc b?n thành công! ??
