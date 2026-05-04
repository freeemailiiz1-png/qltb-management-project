using QuanLyThietBi.DTO;
using QuanLyThietBi.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DAO
{
    internal class ThietBiDAO
    {
        private ConnectionDB conn = new ConnectionDB();
        private LichSuThietBiDAO lichSuThietBiDAO = new LichSuThietBiDAO();
        private LichSuHeThongDAO lichSuHeThongDAO = new LichSuHeThongDAO();
        private HanhDongDAO hanhDongDAO = new HanhDongDAO();

        /// <summary>
        /// Lấy ID của hành động dựa trên tên hành động
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

        public List<ThietBi> GetAll()
        {
            List<ThietBi> list = new List<ThietBi>();
            string query = @"SELECT 
                                tb.ID, 
                                tb.LoaiID, 
                                tb.SerialNumber, 
                                tb.CanBoID, 
                                tb.DonViID, 
                                tb.NgayCap, 
                                tb.NgayHetHan, 
                                tb.NgayHetHanVongDoi, 
                                tb.TrangThai, 
                                tb.GhiChu,
                                ltb.TenLoai,
                                cb.HoTen AS TenCanBo,
                                dv.TenDV AS TenDonVi,
                                tt.TrangThai AS TenTrangThai
                           FROM tblThietBi tb 
                           LEFT JOIN tblLoaiThietBi ltb ON tb.LoaiID = ltb.ID
                           LEFT JOIN tblCanBo cb ON tb.CanBoID = cb.ID
                           LEFT JOIN tblDonVi dv ON tb.DonViID = dv.ID
                           LEFT JOIN tblTrangThai tt ON tb.TrangThai = tt.ID
                           WHERE tb.TrangThai = (SELECT ID FROM tblTrangThai WHERE TrangThai = N'Hoạt động')";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThietBi
                            {
                                ID = (int)reader["ID"],
                                LoaiID = reader["LoaiID"] != DBNull.Value ? (int?)reader["LoaiID"] : null,
                                SerialNumber = reader["SerialNumber"] != DBNull.Value ? reader["SerialNumber"].ToString() : "",
                                CanBoID = reader["CanBoID"] != DBNull.Value ? (int?)reader["CanBoID"] : null,
                                DonViID = reader["DonViID"] != DBNull.Value ? (int?)reader["DonViID"] : null,
                                NgayCap = reader["NgayCap"] != DBNull.Value ? (DateTime?)reader["NgayCap"] : null,
                                NgayHetHan = reader["NgayHetHan"] != DBNull.Value ? (DateTime?)reader["NgayHetHan"] : null,
                                NgayHetHanVongDoi = reader["NgayHetHanVongDoi"] != DBNull.Value ? (DateTime?)reader["NgayHetHanVongDoi"] : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? (int?)reader["TrangThai"] : null,
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : "",
                                TenLoai = reader["TenLoai"] != DBNull.Value ? reader["TenLoai"].ToString() : "",
                                TenCanBo = reader["TenCanBo"] != DBNull.Value ? reader["TenCanBo"].ToString() : "",
                                TenDonVi = reader["TenDonVi"] != DBNull.Value ? reader["TenDonVi"].ToString() : "",
                                TenTrangThai = reader["TenTrangThai"] != DBNull.Value ? reader["TenTrangThai"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thiết bị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(ThietBi thietBi)
        {
            string query = @"INSERT INTO tblThietBi (LoaiID, SerialNumber, CanBoID, DonViID, NgayCap, NgayHetHan, NgayHetHanVongDoi, TrangThai, GhiChu) 
                           VALUES (@LoaiID, @SerialNumber, @CanBoID, @DonViID, @NgayCap, @NgayHetHan, @NgayHetHanVongDoi, @TrangThai, @GhiChu);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                conn.KetNoi();
                int newID = 0;
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@LoaiID", (object)thietBi.LoaiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SerialNumber", (object)thietBi.SerialNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanBoID", (object)thietBi.CanBoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViID", (object)thietBi.DonViID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayCap", (object)thietBi.NgayCap ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHan", (object)thietBi.NgayHetHan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHanVongDoi", (object)thietBi.NgayHetHanVongDoi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)thietBi.TrangThai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)thietBi.GhiChu ?? DBNull.Value);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        newID = Convert.ToInt32(result);

                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = thietBi.UserID,
                            HanhDongID = GetHanhDongID("Thêm"),
                            BangTacDong = "tblThietBi",
                            BanGhiID = newID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = null,
                            NoiDungMoi = $"Serial: {thietBi.SerialNumber}, CanBoID: {thietBi.CanBoID}, DonViID: {thietBi.DonViID}"
                        });

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        // Phương thức helper để lấy thông tin thiết bị theo ID
        private ThietBi GetByID(int id)
        {
            ThietBi thietBi = null;
            string query = "SELECT * FROM tblThietBi WHERE ID = @ID";
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
                            thietBi = new ThietBi
                            {
                                ID = (int)reader["ID"],
                                LoaiID = reader["LoaiID"] != DBNull.Value ? (int?)reader["LoaiID"] : null,
                                SerialNumber = reader["SerialNumber"] != DBNull.Value ? reader["SerialNumber"].ToString() : "",
                                CanBoID = reader["CanBoID"] != DBNull.Value ? (int?)reader["CanBoID"] : null,
                                DonViID = reader["DonViID"] != DBNull.Value ? (int?)reader["DonViID"] : null,
                                NgayCap = reader["NgayCap"] != DBNull.Value ? (DateTime?)reader["NgayCap"] : null,
                                NgayHetHan = reader["NgayHetHan"] != DBNull.Value ? (DateTime?)reader["NgayHetHan"] : null,
                                NgayHetHanVongDoi = reader["NgayHetHanVongDoi"] != DBNull.Value ? (DateTime?)reader["NgayHetHanVongDoi"] : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? (int?)reader["TrangThai"] : null,
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : ""
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin thiết bị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return thietBi;
        }

        public bool Update(ThietBi thietBi)
        {
            // Lấy thông tin thiết bị cũ trước khi cập nhật
            ThietBi thietBiCu = GetByID(thietBi.ID);

            string query = @"UPDATE tblThietBi 
                           SET LoaiID = @LoaiID, 
                               SerialNumber = @SerialNumber, 
                               CanBoID = @CanBoID, 
                               DonViID = @DonViID, 
                               NgayCap = @NgayCap, 
                               NgayHetHan = @NgayHetHan, 
                               NgayHetHanVongDoi = @NgayHetHanVongDoi, 
                               TrangThai = @TrangThai, 
                               GhiChu = @GhiChu
                           WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@LoaiID", (object)thietBi.LoaiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SerialNumber", (object)thietBi.SerialNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanBoID", (object)thietBi.CanBoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViID", (object)thietBi.DonViID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayCap", (object)thietBi.NgayCap ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHan", (object)thietBi.NgayHetHan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHanVongDoi", (object)thietBi.NgayHetHanVongDoi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)thietBi.TrangThai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)thietBi.GhiChu ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID", thietBi.ID);

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0 && thietBiCu != null)
                    {
                        // Ghi lịch sử thiết bị nếu có thay đổi CanBo hoặc DonVi
                        if (thietBiCu.CanBoID != thietBi.CanBoID || thietBiCu.DonViID != thietBi.DonViID)
                        {
                            lichSuThietBiDAO.Insert(new LichSuThietBi
                            {
                                ThietBiID = thietBi.ID,
                                HanhDongID = null, // Có thể lấy ID của hành động "Cập nhật" từ tblHanhDong
                                CanBoCuID = thietBiCu.CanBoID,
                                CanBoMoiID = thietBi.CanBoID,
                                DonViCuID = thietBiCu.DonViID,
                                DonViMoiID = thietBi.DonViID,
                                ThoiDiem = DateTime.Now,
                                GhiChu = "Cập nhật thông tin thiết bị"
                            });
                        }

                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = thietBi.UserID,
                            HanhDongID = GetHanhDongID("Sửa"),
                            BangTacDong = "tblThietBi",
                            BanGhiID = thietBi.ID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"Serial: {thietBiCu.SerialNumber}, CanBoID: {thietBiCu.CanBoID}, DonViID: {thietBiCu.DonViID}",
                            NoiDungMoi = $"Serial: {thietBi.SerialNumber}, CanBoID: {thietBi.CanBoID}, DonViID: {thietBi.DonViID}"
                        });

                        return true;
                    }
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int id, int? userID = null)
        {
            // Lấy thông tin thiết bị trước khi xóa
            ThietBi thietBiCu = GetByID(id);

            string query = "UPDATE tblThietBi SET TrangThai = (SELECT ID FROM tblTrangThai WHERE TrangThai = N'Xóa') WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && thietBiCu != null)
                    {
                        // Ghi lịch sử thiết bị
                        lichSuThietBiDAO.Insert(new LichSuThietBi
                        {
                            ThietBiID = id,
                            HanhDongID = null, // Có thể lấy ID của hành động "Xóa" từ tblHanhDong
                            CanBoCuID = thietBiCu.CanBoID,
                            CanBoMoiID = null,
                            DonViCuID = thietBiCu.DonViID,
                            DonViMoiID = null,
                            ThoiDiem = DateTime.Now,
                            GhiChu = "Xóa thiết bị"
                        });

                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = userID,
                            HanhDongID = GetHanhDongID("Xóa"),
                            BangTacDong = "tblThietBi",
                            BanGhiID = id,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"Serial: {thietBiCu.SerialNumber}, CanBoID: {thietBiCu.CanBoID}, DonViID: {thietBiCu.DonViID}",
                            NoiDungMoi = "Đã xóa"
                        });
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
