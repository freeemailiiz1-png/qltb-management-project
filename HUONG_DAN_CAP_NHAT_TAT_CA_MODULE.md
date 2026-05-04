# ?? H??NG D?N C?P NH?T GHI LOG CHO T?T C? MODULE CÒN L?I

## ? TR?NG THÁI C?P NH?T

| Module | DTO có UserID | DAO có Log | UI truy?n UserID | Hoàn thành |
|--------|---------------|------------|------------------|------------|
| QLThietBi | ? | ? | ? | ? 100% |
| QLUser | ? | ? | ? | ? 100% |
| QLDonVi | ? | ? | ? | ? 100% |
| QLCapDonVi | ? | ? | ? | ? 100% |
| **QLQuyen** | ? | ? | ? | **? 100%** |
| QLCanBo | ? | ? | ? | ?? ?ang c?p nh?t |
| QLLoaiThietBi | ? | ? | ? | ?? ?ang c?p nh?t |
| QLHanhDong | ? | ? | ? | ?? ?ang c?p nh?t |
| QLTrangThai | ? | ? | ? | ?? ?ang c?p nh?t |

---

## ?? ?Ã HOÀN THÀNH: QLQuyen

### **Các file ?ã c?p nh?t:**

1. **DTO/Quyen.cs** ?
   - ?ã có thu?c tính `UserID`

2. **DAO/QuyenDAO.cs** ?
   - Thêm `HanhDongDAO` instance
   - Thêm method `GetHanhDongID(string tenHanhDong)`
   - Thêm method `GetByID(int id)`
   - **Insert()**: Ghi log v?i `HanhDongID`
   - **Update()**: Ghi log v?i thông tin c? và m?i
   - **Delete()**: Ghi log v?i thông tin ?ã xóa

3. **PopupQuyenInfo.cs** ?
   - Import `using QuanLyThietBi.Common;`
   - Truy?n `UserID = SessionManager.GetCurrentUserID()`

4. **QLQuyen.cs** ?
   - Truy?n `SessionManager.GetCurrentUserID()` khi Delete

---

## ?? C?U TRÚC CHU?N CHO CÁC MODULE CÒN L?I

### **B??c 1: C?p nh?t DTO**

```csharp
// Ví d?: DTO/CanBo.cs
public class CanBo
{
    public int ID { get; set; }
    public string HoTen { get; set; }
    public string CCCD { get; set; }
    public DateTime? NgaySinh { get; set; }
    public string Email { get; set; }

    // ? THÊM: Thu?c tính ?? ghi log
    public int? UserID { get; set; }
}
```

### **B??c 2: C?p nh?t DAO**

```csharp
// Ví d?: DAO/CanBoDAO.cs
internal class CanBoDAO
{
    private ConnectionDB conn = new ConnectionDB();
    private LichSuHeThongDAO lichSuHeThongDAO = new LichSuHeThongDAO();
    private HanhDongDAO hanhDongDAO = new HanhDongDAO();

    /// <summary>
    /// L?y ID c?a hành ??ng d?a trên tên hành ??ng
    /// </summary>
    private int? GetHanhDongID(string tenHanhDong)
    {
        try
        {
            var hanhDongs = hanhDongDAO.GetAll();
            var hanhDong = hanhDongs.Find(hd => 
                hd.name != null && 
                hd.name.Equals(tenHanhDong, StringComparison.OrdinalIgnoreCase)
            );
            return hanhDong?.ID;
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// L?y thông tin cán b? theo ID
    /// </summary>
    private CanBo GetByID(int id)
    {
        CanBo canBo = null;
        string query = "SELECT * FROM tblCanBo WHERE ID = @ID";
        try
        {
            conn.KetNoi();
            using (var cmd = new SqlCommand(query, conn.sqlCon))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        canBo = new CanBo
                        {
                            ID = (int)reader["ID"],
                            HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : "",
                            CCCD = reader["CCCD"] != DBNull.Value ? reader["CCCD"].ToString() : "",
                            NgaySinh = reader["NgaySinh"] != DBNull.Value ? (DateTime?)reader["NgaySinh"] : null,
                            Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : ""
                        };
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("L?i khi l?y thông tin cán b?: " + ex.Message);
        }
        finally
        {
            conn.NgatKetNoi();
        }
        return canBo;
    }

    public bool Insert(CanBo canBo)
    {
        string query = @"INSERT INTO tblCanBo (HoTen, CCCD, NgaySinh, Email) 
                       VALUES (@HoTen, @CCCD, @NgaySinh, @Email); 
                       SELECT CAST(SCOPE_IDENTITY() AS INT);";
        try
        {
            conn.KetNoi();
            int newID = 0;
            using (var cmd = new SqlCommand(query, conn.sqlCon))
            {
                cmd.Parameters.AddWithValue("@HoTen", (object)canBo.HoTen ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CCCD", (object)canBo.CCCD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgaySinh", (object)canBo.NgaySinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)canBo.Email ?? DBNull.Value);

                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    newID = Convert.ToInt32(result);

                    // Ghi l?ch s? h? th?ng
                    lichSuHeThongDAO.Insert(new LichSuHeThong
                    {
                        UserID = canBo.UserID,
                        HanhDongID = GetHanhDongID("Thêm"),
                        BangTacDong = "tblCanBo",
                        BanGhiID = newID,
                        ThoiDiem = DateTime.Now,
                        NoiDungCu = null,
                        NoiDungMoi = $"HoTen: {canBo.HoTen}, CCCD: {canBo.CCCD}"
                    });

                    return true;
                }
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("L?i khi thêm cán b?: " + ex.Message);
            return false;
        }
        finally
        {
            conn.NgatKetNoi();
        }
    }

    public bool Update(CanBo canBo)
    {
        // L?y thông tin c?
        CanBo canBoCu = GetByID(canBo.ID);

        string query = @"UPDATE tblCanBo 
                       SET HoTen = @HoTen, 
                           CCCD = @CCCD, 
                           NgaySinh = @NgaySinh, 
                           Email = @Email
                       WHERE ID = @ID";
        try
        {
            conn.KetNoi();
            using (var cmd = new SqlCommand(query, conn.sqlCon))
            {
                cmd.Parameters.AddWithValue("@HoTen", (object)canBo.HoTen ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@CCCD", (object)canBo.CCCD ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgaySinh", (object)canBo.NgaySinh ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)canBo.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@ID", canBo.ID);

                int result = cmd.ExecuteNonQuery();

                if (result > 0 && canBoCu != null)
                {
                    // Ghi l?ch s? h? th?ng
                    lichSuHeThongDAO.Insert(new LichSuHeThong
                    {
                        UserID = canBo.UserID,
                        HanhDongID = GetHanhDongID("S?a"),
                        BangTacDong = "tblCanBo",
                        BanGhiID = canBo.ID,
                        ThoiDiem = DateTime.Now,
                        NoiDungCu = $"HoTen: {canBoCu.HoTen}, CCCD: {canBoCu.CCCD}",
                        NoiDungMoi = $"HoTen: {canBo.HoTen}, CCCD: {canBo.CCCD}"
                    });
                }

                return result > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("L?i khi c?p nh?t cán b?: " + ex.Message);
            return false;
        }
        finally
        {
            conn.NgatKetNoi();
        }
    }

    public bool Delete(int id, int? userID = null)
    {
        // L?y thông tin tr??c khi xóa
        CanBo canBoCu = GetByID(id);

        string query = "DELETE FROM tblCanBo WHERE ID = @ID";
        try
        {
            conn.KetNoi();
            using (var cmd = new SqlCommand(query, conn.sqlCon))
            {
                cmd.Parameters.AddWithValue("@ID", id);
                int result = cmd.ExecuteNonQuery();

                if (result > 0 && canBoCu != null)
                {
                    // Ghi l?ch s? h? th?ng
                    lichSuHeThongDAO.Insert(new LichSuHeThong
                    {
                        UserID = userID,
                        HanhDongID = GetHanhDongID("Xóa"),
                        BangTacDong = "tblCanBo",
                        BanGhiID = id,
                        ThoiDiem = DateTime.Now,
                        NoiDungCu = $"HoTen: {canBoCu.HoTen}, CCCD: {canBoCu.CCCD}",
                        NoiDungMoi = "?ã xóa"
                    });
                }

                return result > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("L?i khi xóa cán b?: " + ex.Message);
            return false;
        }
        finally
        {
            conn.NgatKetNoi();
        }
    }
}
```

### **B??c 3: C?p nh?t Popup**

```csharp
// Ví d?: PopupCanBoInfo.cs

// Thêm using
using QuanLyThietBi.Common;

// Trong btnSave_Click
var canBo = new CanBo
{
    HoTen = txtHoTen.Text.Trim(),
    CCCD = txtCCCD.Text.Trim(),
    NgaySinh = dtpNgaySinh.Value,
    Email = txtEmail.Text.Trim(),

    // ? QUAN TR?NG: Truy?n UserID
    UserID = SessionManager.GetCurrentUserID()
};
```

### **B??c 4: C?p nh?t Form qu?n lý**

```csharp
// Ví d?: QLCanBo.cs

// Trong DeleteClicked
if (result == DialogResult.Yes)
{
    // ? QUAN TR?NG: Truy?n UserID
    bool success = canBoDAO.Delete(selectedID, SessionManager.GetCurrentUserID());
    // ...
}
```

---

## ?? DANH SÁCH FILE C?N C?P NH?T CHO T?NG MODULE

### **1. QLCanBo**
- [ ] DTO/CanBo.cs - Thêm `UserID`
- [ ] DAO/CanBoDAO.cs - Thêm log
- [ ] PopupCanBoInfo.cs - Truy?n `UserID`
- [ ] QLCanBo.cs - Truy?n `UserID` khi Delete

### **2. QLLoaiThietBi**
- [ ] DTO/LoaiThietBi.cs - Thêm `UserID`
- [ ] DAO/LoaiThietBiDAO.cs - Thêm log
- [ ] PopupLoaiThietBiInfo.cs - Truy?n `UserID`
- [ ] QLLoaiThietBi.cs - Truy?n `UserID` khi Delete

### **3. QLHanhDong**
- [ ] DTO/HanhDong.cs - Thêm `UserID`
- [ ] DAO/HanhDongDAO.cs - Thêm log
- [ ] PopupHanhDongInfo.cs - Truy?n `UserID`
- [ ] QLHanhDong.cs - Truy?n `UserID` khi Delete

### **4. QLTrangThai**
- [ ] DTO/TrangThai.cs - Thêm `UserID`
- [ ] DAO/TrangThaiDAO.cs - Thêm log
- [ ] PopupTrangThaiInfo.cs - Truy?n `UserID`
- [ ] QLTrangThai.cs - Truy?n `UserID` khi Delete

---

## ?? CHECKLIST KI?M TRA SAU KHI C?P NH?T

### **??i v?i m?i module:**

- [ ] Build thành công, không có l?i
- [ ] Test thêm m?i: Ki?m tra log trong `tblLichSuHeThong`
- [ ] Test s?a: Ki?m tra `NoiDungCu` và `NoiDungMoi`
- [ ] Test xóa: Ki?m tra log xóa
- [ ] Ki?m tra `HanhDongID` có ?úng không
- [ ] Ki?m tra `UserID` có ?úng không
- [ ] Ki?m tra `BangTacDong` ?úng tên b?ng

### **SQL ?? ki?m tra:**

```sql
-- Ki?m tra log c?a module CanBo
SELECT TOP 10 
    ls.ID,
    u.TenDangNhap,
    hd.name AS TenHanhDong,
    ls.BangTacDong,
    ls.BanGhiID,
    ls.ThoiDiem,
    ls.NoiDungCu,
    ls.NoiDungMoi
FROM tblLichSuHeThong ls
LEFT JOIN tblUser u ON ls.UserID = u.ID
LEFT JOIN tblHanhDong hd ON ls.HanhDongID = hd.ID
WHERE ls.BangTacDong = 'tblCanBo'
ORDER BY ls.ThoiDiem DESC
```

---

## ?? L?U Ý QUAN TR?NG

### **??i v?i các b?ng KHÔNG CÓ TrangThai:**

Các b?ng nh? `tblCanBo`, `tblLoaiThietBi`, `tblHanhDong`, `tblTrangThai` **KHÔNG CÓ** c?t `TrangThai`, nên:

1. ? **Delete s? XÓA C?NG** (DELETE thay vì UPDATE TrangThai)
2. ? C?n **l?u thông tin c? tr??c khi xóa** ?? ghi log
3. ?? **C?n th?n khi xóa** vì không th? khôi ph?c

### **Mapping tên b?ng trong BangTacDong:**

| Module | Tên b?ng trong DB | Giá tr? BangTacDong |
|--------|-------------------|---------------------|
| QLCanBo | tblCanBo | "tblCanBo" |
| QLLoaiThietBi | tblLoaiThietBi | "tblLoaiThietBi" |
| QLHanhDong | tblHanhDong | "tblHanhDong" |
| QLTrangThai | tblTrangThai | "tblTrangThai" |

---

## ?? HÀNH ??NG TI?P THEO

**B?n mu?n tôi:**

1. ? Ti?p t?c c?p nh?t t?ng module (CanBo, LoaiThietBi, HanhDong, TrangThai)?
2. ? T?o script SQL ?? ki?m tra log cho t?t c? module?
3. ? T?o báo cáo t?ng h?p v? tình tr?ng ghi log?

**Vui lòng xác nh?n ?? tôi ti?p t?c!**
