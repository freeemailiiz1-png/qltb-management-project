# ? CHECKLIST GHI LOG VÀO tblLichSuHeThong

## ?? KI?M TRA NGUYÊN NHÂN LOG KHÔNG ???C GHI

### ? B??c 1: Ki?m tra c?u trúc b?ng trong Database

Ch?y SQL query sau ?? ki?m tra b?ng `tblLichSuHeThong`:

```sql
-- Ki?m tra xem b?ng có t?n t?i không
SELECT * FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_NAME = 'tblLichSuHeThong'

-- Ki?m tra c?u trúc b?ng
SELECT COLUMN_NAME, DATA_TYPE, IS_NULLABLE 
FROM INFORMATION_SCHEMA.COLUMNS 
WHERE TABLE_NAME = 'tblLichSuHeThong'
ORDER BY ORDINAL_POSITION

-- Ki?m tra d? li?u hi?n có
SELECT TOP 10 * FROM tblLichSuHeThong ORDER BY ThoiDiem DESC
```

**C?u trúc b?ng c?n có:**
- `ID` INT PRIMARY KEY IDENTITY
- `UserID` INT (nullable)
- `HanhDong` NVARCHAR(50)
- `BangTacDong` NVARCHAR(50)
- `BanGhiID` INT (nullable)
- `ThoiDiem` DATETIME
- `NoiDungCu` NVARCHAR(MAX) (nullable)
- `NoiDungMoi` NVARCHAR(MAX) (nullable)

---

### ? B??c 2: Ki?m tra User ?ã ??ng nh?p ch?a

Thêm ?o?n code sau vào form ?? ki?m tra:

```csharp
private void TestSession()
{
    if (!SessionManager.IsLoggedIn())
    {
        MessageBox.Show("Ch?a ??ng nh?p! UserID s? là NULL.");
    }
    else
    {
        var userID = SessionManager.GetCurrentUserID();
        var username = SessionManager.GetCurrentUsername();
        MessageBox.Show($"?ã ??ng nh?p:\nUserID: {userID}\nUsername: {username}");
    }
}
```

G?i `TestSession()` ngay sau khi ??ng nh?p thành công ?? ??m b?o Session ?ã ???c l?u.

---

### ? B??c 3: Ki?m tra xem LichSuHeThongDAO.Insert() có ???c g?i không

Thêm logging vào `LichSuHeThongDAO.Insert()`:

```csharp
public bool Insert(LichSuHeThong lichSu)
{
    // ? THÊM LOGGING ?? DEBUG
    System.Diagnostics.Debug.WriteLine("=== GHI LOG ===");
    System.Diagnostics.Debug.WriteLine($"UserID: {lichSu.UserID}");
    System.Diagnostics.Debug.WriteLine($"HanhDong: {lichSu.HanhDong}");
    System.Diagnostics.Debug.WriteLine($"BangTacDong: {lichSu.BangTacDong}");
    System.Diagnostics.Debug.WriteLine($"BanGhiID: {lichSu.BanGhiID}");
    System.Diagnostics.Debug.WriteLine($"NoiDungMoi: {lichSu.NoiDungMoi}");

    string query = @"INSERT INTO tblLichSuHeThong 
                   (UserID, HanhDong, BangTacDong, BanGhiID, ThoiDiem, NoiDungCu, NoiDungMoi) 
                   VALUES (@UserID, @HanhDong, @BangTacDong, @BanGhiID, @ThoiDiem, @NoiDungCu, @NoiDungMoi)";
    try
    {
        conn.KetNoi();
        using (var cmd = new SqlCommand(query, conn.sqlCon))
        {
            cmd.Parameters.AddWithValue("@UserID", (object)lichSu.UserID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@HanhDong", (object)lichSu.HanhDong ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BangTacDong", (object)lichSu.BangTacDong ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@BanGhiID", (object)lichSu.BanGhiID ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@ThoiDiem", (object)lichSu.ThoiDiem ?? DateTime.Now);
            cmd.Parameters.AddWithValue("@NoiDungCu", (object)lichSu.NoiDungCu ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@NoiDungMoi", (object)lichSu.NoiDungMoi ?? DBNull.Value);

            int result = cmd.ExecuteNonQuery();

            // ? LOGGING K?T QU?
            System.Diagnostics.Debug.WriteLine($"K?t qu? ExecuteNonQuery: {result}");
            System.Diagnostics.Debug.WriteLine("=== K?T THÚC GHI LOG ===\n");

            return result > 0;
        }
    }
    catch (Exception ex)
    {
        // ? HI?N TH? L?I CHI TI?T
        System.Diagnostics.Debug.WriteLine($"L?I GHI LOG: {ex.Message}");
        System.Diagnostics.Debug.WriteLine($"Stack Trace: {ex.StackTrace}");

        MessageBox.Show($"L?i khi thêm l?ch s? h? th?ng:\n{ex.Message}", "L?i", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return false;
    }
    finally
    {
        conn.NgatKetNoi();
    }
}
```

---

### ? B??c 4: Test t?ng b??c

#### Test 1: Thêm Thi?t B?
1. ??ng nh?p vào h? th?ng
2. M? form QLThietBi
3. Click nút "Thêm"
4. Nh?p thông tin thi?t b?
5. Click "L?u"
6. Ki?m tra Output Window (View ? Output) xem có log "=== GHI LOG ===" không
7. Ki?m tra database:
```sql
SELECT TOP 1 * FROM tblLichSuHeThong 
WHERE BangTacDong = 'tblThietBi' AND HanhDong = N'Thêm'
ORDER BY ThoiDiem DESC
```

#### Test 2: S?a Thi?t B?
1. Ch?n 1 thi?t b? trong danh sách
2. Click "S?a"
3. Thay ??i thông tin (ví d?: Cán b?, ??n v?)
4. Click "L?u"
5. Ki?m tra log trong Output Window
6. Ki?m tra database:
```sql
SELECT TOP 1 * FROM tblLichSuHeThong 
WHERE BangTacDong = 'tblThietBi' AND HanhDong = N'S?a'
ORDER BY ThoiDiem DESC
```

#### Test 3: Xóa Thi?t B?
1. Ch?n 1 thi?t b?
2. Click "Xóa"
3. Xác nh?n xóa
4. Ki?m tra log
5. Ki?m tra database:
```sql
SELECT TOP 1 * FROM tblLichSuHeThong 
WHERE BangTacDong = 'tblThietBi' AND HanhDong = N'Xóa'
ORDER BY ThoiDiem DESC
```

---

### ? B??c 5: Các l?i th??ng g?p và cách kh?c ph?c

#### L?i 1: UserID = NULL
**Nguyên nhân:** Ch?a ??ng nh?p ho?c Session b? m?t  
**Kh?c ph?c:** 
- ??m b?o g?i `SessionManager.SetCurrentUser(user)` sau khi ??ng nh?p thành công
- Ki?m tra xem có g?i `SessionManager.Logout()` ? ?âu không

#### L?i 2: Không có dòng nào ???c insert vào database
**Nguyên nhân:** L?i SQL ho?c connection string sai  
**Kh?c ph?c:**
- Ki?m tra connection string trong `App.config`
- Ki?m tra quy?n INSERT trên b?ng `tblLichSuHeThong`
- Xem chi ti?t l?i trong Output Window

#### L?i 3: ExecuteNonQuery() tr? v? 0
**Nguyên nhân:** Câu SQL không th?c thi ???c  
**Kh?c ph?c:**
- Copy câu SQL ra SQL Server Management Studio và ch?y th?
- Ki?m tra xem có trigger/constraint nào block không

#### L?i 4: Không th?y log trong Output Window
**Nguyên nhân:** Ph??ng th?c Insert() không ???c g?i  
**Kh?c ph?c:**
- ??t breakpoint t?i `lichSuHeThongDAO.Insert()`
- Debug ?? xem có vào hàm này không
- Ki?m tra xem có exception nào b? "nu?t" (catch mà không log) không

---

### ? B??c 6: Ki?m tra t?t c? form khác

Các form c?n ki?m tra:
- ? **QLThietBi** / PopupThietBiInfo
- ? **QLUser** / PopupUserInfor
- ? **QLDonVi** / PopupDonViInfo  
- ? **QLQuyen** / PopupQuyenInfo
- ? **QLCanBo** / PopupCanBoInfo

??m b?o t?t c? ??u:
1. Import `using QuanLyThietBi.Common;`
2. Truy?n `UserID` t? `SessionManager` vào DTO
3. G?i ph??ng th?c DAO v?i ??y ?? tham s?

---

### ? B??c 7: Script ki?m tra t?ng quan

Ch?y script SQL sau ?? xem t?ng quan log:

```sql
-- T?ng s? log trong h? th?ng
SELECT COUNT(*) AS TongSoLog FROM tblLichSuHeThong

-- Log theo b?ng
SELECT BangTacDong, COUNT(*) AS SoLuong
FROM tblLichSuHeThong
GROUP BY BangTacDong

-- Log theo hành ??ng
SELECT HanhDong, COUNT(*) AS SoLuong
FROM tblLichSuHeThong
GROUP BY HanhDong

-- Log theo user
SELECT u.TenDangNhap, COUNT(*) AS SoLuong
FROM tblLichSuHeThong ls
LEFT JOIN tblUser u ON ls.UserID = u.ID
GROUP BY u.TenDangNhap

-- 10 log g?n nh?t
SELECT TOP 10 
    ls.ID,
    u.TenDangNhap,
    ls.HanhDong,
    ls.BangTacDong,
    ls.BanGhiID,
    ls.ThoiDiem,
    ls.NoiDungMoi
FROM tblLichSuHeThong ls
LEFT JOIN tblUser u ON ls.UserID = u.ID
ORDER BY ls.ThoiDiem DESC
```

---

## ?? DANH SÁCH KI?M TRA NHANH

- [ ] B?ng `tblLichSuHeThong` ?ã t?n t?i trong database
- [ ] C?u trúc b?ng ?úng (8 c?t nh? mô t? ? trên)
- [ ] User ?ã ??ng nh?p thành công
- [ ] `SessionManager.GetCurrentUserID()` tr? v? giá tr? h?p l?
- [ ] `PopupThietBiInfo` ?ã import `using QuanLyThietBi.Common;`
- [ ] DTO `ThietBi` có thu?c tính `UserID`
- [ ] Khi t?o ??i t??ng `ThietBi`, ?ã gán `UserID = SessionManager.GetCurrentUserID()`
- [ ] `ThietBiDAO.Insert/Update/Delete` ?ã g?i `lichSuHeThongDAO.Insert()`
- [ ] Không có exception nào b? "nu?t" mà không log ra
- [ ] Connection string ?úng và có quy?n INSERT vào b?ng

---

## ?? L?I KHUYÊN

1. **Luôn ki?m tra Output Window** khi ch?y debug ?? xem log
2. **Dùng SQL Profiler** ?? xem câu SQL th?c t? ???c g?i ??n database
3. **Thêm try-catch v?i MessageBox** ?? hi?n th? l?i rõ ràng thay vì ch? ghi Console.WriteLine
4. **Test t?ng ch?c n?ng m?t** thay vì test t?t c? cùng lúc

---

**N?u sau khi làm theo checklist này mà v?n không th?y log, hãy cho tôi bi?t:**
1. B?n ?ang b? ? b??c nào?
2. Output Window hi?n th? gì?
3. Database có d? li?u gì trong `tblLichSuHeThong`?
