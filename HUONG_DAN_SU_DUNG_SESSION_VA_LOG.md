# H??NG D?N S? D?NG SESSION MANAGER VÀ GHI LOG

## 1. GI?I THI?U

H? th?ng ?ã ???c c?p nh?t ?? t? ??ng ghi l?i l?ch s? (audit log) cho m?i thao tác CRUD (Create, Read, Update, Delete).

- **SessionManager**: L?u tr? thông tin user ?ang ??ng nh?p
- **LichSuThietBiDAO**: Ghi l?ch s? cho thi?t b? (khi thay ??i CanBo, DonVi)
- **LichSuHeThongDAO**: Ghi l?ch s? cho t?t c? các b?ng

---

## 2. CÁC DAO ?Ã ???C C?P NH?T

? **ThietBiDAO** - Ghi log vào c? `tblLichSuThietBi` và `tblLichSuHeThong`
? **UserDAO** - Ghi log vào `tblLichSuHeThong`
? **DonViDAO** - Ghi log vào `tblLichSuHeThong`
? **QuyenDAO** - Ghi log vào `tblLichSuHeThong`

---

## 3. CÁC DTO ?Ã ???C B? SUNG THU?C TÍNH

- **ThietBi.UserID**: ID c?a user th?c hi?n hành ??ng
- **User.ActionByUserID**: ID c?a user th?c hi?n hành ??ng
- **DonVi.UserID**: ID c?a user th?c hi?n hành ??ng
- **Quyen.UserID**: ID c?a user th?c hi?n hành ??ng

---

## 4. H??NG D?N S? D?NG TRONG UI

### 4.1. L?y thông tin user ?ang ??ng nh?p

```csharp
using QuanLyThietBi.Common;

// L?y toàn b? ??i t??ng User
User currentUser = SessionManager.GetCurrentUser();

// Ho?c ch? l?y UserID
int? currentUserID = SessionManager.GetCurrentUserID();

// Ho?c l?y username
string username = SessionManager.GetCurrentUsername();

// Ki?m tra xem ?ã ??ng nh?p ch?a
bool isLoggedIn = SessionManager.IsLoggedIn();
```

### 4.2. Thêm m?i d? li?u (INSERT)

#### Ví d? 1: Thêm Thi?t B?

```csharp
private void btnLuu_Click(object sender, EventArgs e)
{
    ThietBi thietBi = new ThietBi
    {
        SerialNumber = txtSerial.Text,
        LoaiID = (int)cboLoai.SelectedValue,
        CanBoID = (int)cboCanBo.SelectedValue,
        DonViID = (int)cboDonVi.SelectedValue,
        TrangThai = 1, // Ho?t ??ng

        // ? QUAN TR?NG: Truy?n UserID t? SessionManager
        UserID = SessionManager.GetCurrentUserID()
    };

    ThietBiDAO dao = new ThietBiDAO();
    bool success = dao.Insert(thietBi);

    if (success)
    {
        MessageBox.Show("Thêm thi?t b? thành công!");
        // Log s? ???c ghi t? ??ng vào tblLichSuThietBi và tblLichSuHeThong
    }
}
```

#### Ví d? 2: Thêm User

```csharp
private void btnLuu_Click(object sender, EventArgs e)
{
    User user = new User
    {
        TenDangNhap = txtTenDangNhap.Text,
        MatKhau = txtMatKhau.Text,
        DonViID = (int)cboDonVi.SelectedValue,
        CapDonViID = (int)cboCapDonVi.SelectedValue,
        TrangThai = 1,

        // ? QUAN TR?NG: Truy?n ID c?a user ?ang th?c hi?n hành ??ng
        ActionByUserID = SessionManager.GetCurrentUserID()
    };

    UserDAO dao = new UserDAO();
    bool success = dao.AddUser(user);

    if (success)
    {
        MessageBox.Show("Thêm ng??i dùng thành công!");
        // Log s? ???c ghi t? ??ng vào tblLichSuHeThong
    }
}
```

#### Ví d? 3: Thêm ??n V?

```csharp
private void btnLuu_Click(object sender, EventArgs e)
{
    DonVi donVi = new DonVi
    {
        MaDV = txtMaDV.Text,
        TenDV = txtTenDV.Text,
        CapDV = (int)cboCapDV.SelectedValue,
        DonViChaID = (int?)cboDonViCha.SelectedValue,
        TrangThai = 1,
        NgayTao = DateTime.Now,

        // ? QUAN TR?NG: Truy?n UserID
        UserID = SessionManager.GetCurrentUserID()
    };

    DonViDAO dao = new DonViDAO();
    bool success = dao.Insert(donVi);

    if (success)
    {
        MessageBox.Show("Thêm ??n v? thành công!");
    }
}
```

### 4.3. C?p nh?t d? li?u (UPDATE)

#### Ví d? 1: C?p nh?t Thi?t B?

```csharp
private void btnCapNhat_Click(object sender, EventArgs e)
{
    ThietBi thietBi = new ThietBi
    {
        ID = selectedThietBiID, // ID c?a thi?t b? c?n c?p nh?t
        SerialNumber = txtSerial.Text,
        LoaiID = (int)cboLoai.SelectedValue,
        CanBoID = (int)cboCanBo.SelectedValue,
        DonViID = (int)cboDonVi.SelectedValue,
        TrangThai = (int)cboTrangThai.SelectedValue,
        GhiChu = txtGhiChu.Text,

        // ? QUAN TR?NG: Truy?n UserID
        UserID = SessionManager.GetCurrentUserID()
    };

    ThietBiDAO dao = new ThietBiDAO();
    bool success = dao.Update(thietBi);

    if (success)
    {
        MessageBox.Show("C?p nh?t thi?t b? thành công!");
        // Log s? ghi l?i: NoiDungCu và NoiDungMoi
    }
}
```

#### Ví d? 2: C?p nh?t User (Không ??i m?t kh?u)

```csharp
private void btnCapNhat_Click(object sender, EventArgs e)
{
    User user = new User
    {
        ID = selectedUserID,
        TenDangNhap = txtTenDangNhap.Text,
        MatKhau = "", // ? ?? tr?ng n?u không ??i m?t kh?u
        DonViID = (int)cboDonVi.SelectedValue,
        CapDonViID = (int)cboCapDonVi.SelectedValue,
        TrangThai = (int)cboTrangThai.SelectedValue,

        // ? QUAN TR?NG
        ActionByUserID = SessionManager.GetCurrentUserID()
    };

    UserDAO dao = new UserDAO();
    bool success = dao.UpdateUser(user);

    if (success)
    {
        MessageBox.Show("C?p nh?t thành công!");
    }
}
```

### 4.4. Xóa d? li?u (DELETE)

#### Ví d? 1: Xóa Thi?t B?

```csharp
private void btnXoa_Click(object sender, EventArgs e)
{
    if (dgThietBi.SelectedRows.Count == 0)
    {
        MessageBox.Show("Vui lòng ch?n thi?t b? c?n xóa!");
        return;
    }

    int thietBiID = Convert.ToInt32(dgThietBi.SelectedRows[0].Cells["ID"].Value);

    DialogResult result = MessageBox.Show(
        "B?n có ch?c ch?n mu?n xóa thi?t b? này?",
        "Xác nh?n",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    );

    if (result == DialogResult.Yes)
    {
        ThietBiDAO dao = new ThietBiDAO();

        // ? QUAN TR?NG: Truy?n c? ID thi?t b? và UserID
        bool success = dao.Delete(thietBiID, SessionManager.GetCurrentUserID());

        if (success)
        {
            MessageBox.Show("Xóa thi?t b? thành công!");
            LoadDanhSachThietBi(); // Reload danh sách
        }
    }
}
```

#### Ví d? 2: Xóa User

```csharp
private void btnXoa_Click(object sender, EventArgs e)
{
    if (dgUser.SelectedRows.Count == 0)
    {
        MessageBox.Show("Vui lòng ch?n ng??i dùng c?n xóa!");
        return;
    }

    int userID = Convert.ToInt32(dgUser.SelectedRows[0].Cells["ID"].Value);

    // Không cho phép xóa chính mình
    if (userID == SessionManager.GetCurrentUserID())
    {
        MessageBox.Show("B?n không th? xóa tài kho?n c?a chính mình!");
        return;
    }

    DialogResult result = MessageBox.Show(
        "B?n có ch?c ch?n mu?n xóa ng??i dùng này?",
        "Xác nh?n",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    );

    if (result == DialogResult.Yes)
    {
        UserDAO dao = new UserDAO();

        // ? QUAN TR?NG: Truy?n c? userID và actionByUserID
        bool success = dao.DeleteUser(userID, SessionManager.GetCurrentUserID());

        if (success)
        {
            MessageBox.Show("Xóa ng??i dùng thành công!");
            LoadDanhSachUser();
        }
    }
}
```

#### Ví d? 3: Xóa ??n V?

```csharp
private void btnXoa_Click(object sender, EventArgs e)
{
    if (dgDonVi.SelectedRows.Count == 0)
    {
        MessageBox.Show("Vui lòng ch?n ??n v? c?n xóa!");
        return;
    }

    int donViID = Convert.ToInt32(dgDonVi.SelectedRows[0].Cells["ID"].Value);

    DialogResult result = MessageBox.Show(
        "B?n có ch?c ch?n mu?n xóa ??n v? này?",
        "Xác nh?n",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Warning
    );

    if (result == DialogResult.Yes)
    {
        DonViDAO dao = new DonViDAO();

        // ? QUAN TR?NG: Truy?n UserID
        bool success = dao.Delete(donViID, SessionManager.GetCurrentUserID());

        if (success)
        {
            MessageBox.Show("Xóa ??n v? thành công!");
            LoadDanhSachDonVi();
        }
    }
}
```

---

## 5. XEM L?CH S? H? TH?NG

### 5.1. L?y t?t c? l?ch s?

```csharp
LichSuHeThongDAO dao = new LichSuHeThongDAO();
List<LichSuHeThong> list = dao.GetAll();
dgLichSu.DataSource = list;
```

### 5.2. L?y l?ch s? theo User

```csharp
LichSuHeThongDAO dao = new LichSuHeThongDAO();
List<LichSuHeThong> list = dao.GetByUser(SessionManager.GetCurrentUserID().Value);
dgLichSu.DataSource = list;
```

### 5.3. L?y l?ch s? theo B?ng

```csharp
LichSuHeThongDAO dao = new LichSuHeThongDAO();
List<LichSuHeThong> list = dao.GetByTable("tblThietBi");
dgLichSu.DataSource = list;
```

---

## 6. L?U Ý QUAN TR?NG

### ?? B?T BU?C PH?I TRUY?N UserID

Khi g?i Insert/Update/Delete, **B?T BU?C** ph?i truy?n `UserID` t? `SessionManager`, n?u không log s? không ghi ???c thông tin ng??i th?c hi?n.

### ? Ki?m tra ??ng nh?p tr??c khi thao tác

```csharp
if (!SessionManager.IsLoggedIn())
{
    MessageBox.Show("Vui lòng ??ng nh?p ?? th?c hi?n thao tác này!");
    return;
}

// Ti?p t?c th?c hi?n thao tác...
```

### ? ??ng xu?t khi ?óng ?ng d?ng

```csharp
private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
{
    SessionManager.Logout();
}
```

---

## 7. C?U TRÚC D? LI?U LOG

### 7.1. tblLichSuThietBi

```sql
ID              INT             -- ID t? t?ng
ThietBiID       INT             -- ID thi?t b?
HanhDongID      INT             -- ID hành ??ng (t? tblHanhDong)
CanBoCuID       INT             -- Cán b? c?
CanBoMoiID      INT             -- Cán b? m?i
DonViCuID       INT             -- ??n v? c?
DonViMoiID      INT             -- ??n v? m?i
ThoiDiem        DATETIME        -- Th?i ?i?m th?c hi?n
GhiChu          NVARCHAR(500)   -- Ghi chú
```

### 7.2. tblLichSuHeThong

```sql
ID              INT             -- ID t? t?ng
UserID          INT             -- User th?c hi?n hành ??ng
HanhDong        NVARCHAR(50)    -- "Thêm", "S?a", "Xóa"
BangTacDong     NVARCHAR(50)    -- "tblThietBi", "tblUser", ...
BanGhiID        INT             -- ID c?a b?n ghi b? tác ??ng
ThoiDiem        DATETIME        -- Th?i ?i?m th?c hi?n
NoiDungCu       NVARCHAR(MAX)   -- D? li?u tr??c khi thay ??i
NoiDungMoi      NVARCHAR(MAX)   -- D? li?u sau khi thay ??i
```

---

## 8. TR? GIÚP & H? TR?

N?u g?p l?i ho?c c?n h? tr? thêm, vui lòng ki?m tra:
- ?ã import `using QuanLyThietBi.Common;` ch?a?
- ?ã truy?n `UserID` t? `SessionManager` ch?a?
- User ?ã ??ng nh?p ch?a? (`SessionManager.IsLoggedIn()`)

---

**Chúc b?n code vui v?! ??**
