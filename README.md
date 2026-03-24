# qltb-management-project
# IT43_QLTB – Nền tảng Quản lý Thiết bị (Cơ quan Nhà nước)

Dự án **IT43_QLTB** là ứng dụng **Windows Forms (C#)** phục vụ quản lý thiết bị chuyên dùng trong môi trường cơ quan nhà nước. Hệ thống tập trung vào quản lý danh mục thiết bị, phân cấp đơn vị, cán bộ sử dụng, trạng thái thiết bị và lịch sử thao tác/hành động nhằm đảm bảo **minh bạch – truy vết – kiểm soát trách nhiệm**.

---

## 1. Mục tiêu dự án

- Quản lý tập trung thiết bị chuyên dùng theo **đơn vị hành chính/đơn vị trực thuộc**.
- Theo dõi **vòng đời thiết bị**: ngày cấp, ngày hết hạn, hết hạn vòng đời (khấu hao/niên hạn).
- Gắn trách nhiệm sử dụng thiết bị theo **cán bộ** và **đơn vị quản lý**.
- Ghi nhận **lịch sử thiết bị** và **nhật ký hệ thống** để kiểm tra, truy vết, phục vụ kiểm kê/kiểm toán.

---

## 2. Công nghệ sử dụng

- **Ngôn ngữ/Framework**: C# – Windows Forms, .NET Framework **4.7.2**.  [oai_citation:0‡GitHub](https://github.com/kienndt0808/IT43_QLTB/blob/main/App.config)  
- **CSDL**: Microsoft SQL Server
- **ORM**: Entity Framework (có thư mục package EF trong repo).  [oai_citation:1‡GitHub](https://github.com/kienndt0808/IT43_QLTB)  

---

## 3. Chức năng chính

Dự án tổ chức theo các màn hình quản trị (Form) tương ứng các nghiệp vụ:

- **Quản lý Cán bộ** (`QLCanBo`)
- **Quản lý Cấp đơn vị** (`QLCapDonVi`)
- **Quản lý Đơn vị** (`QLDonVi`)
- **Quản lý Loại thiết bị** (`QLLoaiThietBi`)
- **Quản lý Thiết bị** (`QLThietBi`)
- **Quản lý Trạng thái** (`QLTrangThai`)
- **Quản lý Người dùng** (`QLUser`)
- **Quản lý Hành động** (`QLHanhDong`)
- **Xem Lịch sử hệ thống** (`QLLichSuHeThong`)
- **Xem Lịch sử thiết bị** (`QLLichSuThietBi`)
- Màn hình chính: `Form1`  
(Danh sách file Form có thể xem ngay trong thư mục gốc repo).  [oai_citation:2‡GitHub](https://github.com/kienndt0808/IT43_QLTB)  

---

## 4. Kiến trúc thư mục (tham khảo)

Repo có các thư mục phổ biến theo hướng phân lớp:

- `DTO/`: lớp dữ liệu (Data Transfer Object)
- `DAO/`: lớp truy xuất dữ liệu (Data Access Object)
- `Common/`: hàm tiện ích dùng chung
- Các Form WinForms: `QL*.cs`, `Form1.cs`, `PopupUserInfor.cs` …  [oai_citation:3‡GitHub](https://github.com/kienndt0808/IT43_QLTB)  

---

## 5. Thiết kế cơ sở dữ liệu (từ file `qltb.sql`)

File script CSDL nằm trong repo: `qltb.sql`.  [oai_citation:4‡GitHub](https://github.com/kienndt0808/IT43_QLTB)  

### Nhóm bảng danh mục
- `tblTrangThai`: danh mục trạng thái dùng chung.
- `tblHanhDong`: danh mục hành động (phục vụ lịch sử).
- `tblCapDV`: danh mục cấp đơn vị.
- `tblLoaiThietBi`: danh mục loại thiết bị (có `VongDoiNam`, mặc định 5 năm).

### Nhóm bảng nghiệp vụ
- `tblDonVi`: đơn vị quản lý theo phân cấp (có `DonViChaID`).
- `tblCanBo`: thông tin cán bộ.
- `tblThietBi`: thông tin thiết bị, gán cán bộ/đơn vị, theo dõi hạn và vòng đời.
- `tblUser`: tài khoản người dùng (gắn đơn vị/cấp đơn vị/trạng thái).

### Nhóm bảng lịch sử – truy vết
- `tblLichSuThietBi`: ghi nhận thay đổi liên quan thiết bị (cán bộ cũ/mới, đơn vị cũ/mới, hành động, thời điểm…).
- `tblLichSuHeThong`: nhật ký tác động hệ thống theo user/hành động/bảng/bản ghi.
- *(Ngoài danh sách 10 bảng bạn nêu, trong script còn có `tblQuyen` để quản lý quyền theo trạng thái.)*

---

## 6. Cấu hình & cài đặt

### 6.1 Yêu cầu môi trường
- Windows 10/11
- Visual Studio (khuyến nghị 2019/2022)
- .NET Framework Developer Pack **4.7.2**
- SQL Server (Express/Developer đều được)

### 6.2 Tạo cơ sở dữ liệu
1. Mở SQL Server Management Studio (SSMS).
2. Tạo database tên: `QLTB`
3. Chạy script: `qltb.sql` để tạo bảng và dữ liệu liên quan.  [oai_citation:5‡GitHub](https://github.com/kienndt0808/IT43_QLTB/blob/main/qltb.sql)  

### 6.3 Cấu hình chuỗi kết nối
Trong `App.config`, dự án đang dùng connection string tên **`QLTB`** và kết nối SQL Server theo dạng integrated security.  [oai_citation:6‡GitHub](https://github.com/kienndt0808/IT43_QLTB/blob/main/App.config)  

Ví dụ hiện tại:
```xml
<add name="QLTB" connectionString="data source=DESKTOP708;initial catalog=QLTB;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" providerName="System.Data.SqlClient" />
