using QuanLyThietBi.Common;
using QuanLyThietBi.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace QuanLyThietBi.DAO
{
    internal class DonViDAO
    {
        private ConnectionDB conn = new ConnectionDB();
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

        public List<DonVi> GetAll()
        {
            List<DonVi> list = new List<DonVi>();
            string query = @"SELECT 
                                dv.ID, 
                                dv.MaDV, 
                                dv.TenDV, 
                                dv.CapDV, 
                                dv.DonViChaID, 
                                dv.TrangThai, 
                                dv.NgayTao,
                                cdv.TenCapDV,
                                dvcha.TenDV AS TenDonViCha,
                                tt.TrangThai AS TenTrangThai
                           FROM tblDonVi dv 
                           LEFT JOIN tblCapDV cdv ON dv.CapDV = cdv.ID
                           LEFT JOIN tblDonVi dvcha ON dv.DonViChaID = dvcha.ID
                           LEFT JOIN tblTrangThai tt ON dv.TrangThai = tt.ID
                           WHERE dv.TrangThai = (SELECT ID FROM tblTrangThai WHERE TrangThai = N'Hoạt động')";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DonVi
                            {
                                ID = (int)reader["ID"],
                                MaDV = reader["MaDV"] != DBNull.Value ? reader["MaDV"].ToString() : "",
                                TenDV = reader["TenDV"] != DBNull.Value ? reader["TenDV"].ToString() : "",
                                CapDV = reader["CapDV"] != DBNull.Value ? (int?)reader["CapDV"] : null,
                                DonViChaID = reader["DonViChaID"] != DBNull.Value ? (int?)reader["DonViChaID"] : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? (int?)reader["TrangThai"] : null,
                                NgayTao = reader["NgayTao"] != DBNull.Value ? (DateTime?)reader["NgayTao"] : null,
                                TenCapDV = reader["TenCapDV"] != DBNull.Value ? reader["TenCapDV"].ToString() : "",
                                TenDonViCha = reader["TenDonViCha"] != DBNull.Value ? reader["TenDonViCha"].ToString() : "",
                                TenTrangThai = reader["TenTrangThai"] != DBNull.Value ? reader["TenTrangThai"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách đơn vị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        // Phương thức helper để lấy thông tin đơn vị theo ID
        private DonVi GetByID(int id)
        {
            DonVi donVi = null;
            string query = "SELECT * FROM tblDonVi WHERE ID = @ID";
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
                            donVi = new DonVi
                            {
                                ID = (int)reader["ID"],
                                MaDV = reader["MaDV"] != DBNull.Value ? reader["MaDV"].ToString() : "",
                                TenDV = reader["TenDV"] != DBNull.Value ? reader["TenDV"].ToString() : "",
                                CapDV = reader["CapDV"] != DBNull.Value ? (int?)reader["CapDV"] : null,
                                DonViChaID = reader["DonViChaID"] != DBNull.Value ? (int?)reader["DonViChaID"] : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? (int?)reader["TrangThai"] : null
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin đơn vị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return donVi;
        }

        public bool Insert(DonVi donVi)
        {
            string query = @"INSERT INTO tblDonVi (MaDV, TenDV, CapDV, DonViChaID, TrangThai, NgayTao) 
                           VALUES (@MaDV, @TenDV, @CapDV, @DonViChaID, @TrangThai, @NgayTao); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                conn.KetNoi();
                int newID = 0;
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@MaDV", (object)donVi.MaDV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenDV", (object)donVi.TenDV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CapDV", (object)donVi.CapDV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViChaID", (object)donVi.DonViChaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)donVi.TrangThai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayTao", (object)donVi.NgayTao ?? DateTime.Now);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        newID = Convert.ToInt32(result);

                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = donVi.UserID,
                            HanhDongID = GetHanhDongID("Thêm"),
                            BangTacDong = "tblDonVi",
                            BanGhiID = newID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = null,
                            NoiDungMoi = $"MaDV: {donVi.MaDV}, TenDV: {donVi.TenDV}, CapDV: {donVi.CapDV}"
                        });

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm đơn vị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(DonVi donVi)
        {
            // Lấy thông tin đơn vị cũ trước khi cập nhật
            DonVi donViCu = GetByID(donVi.ID);

            string query = @"UPDATE tblDonVi 
                           SET MaDV = @MaDV, 
                               TenDV = @TenDV, 
                               CapDV = @CapDV, 
                               DonViChaID = @DonViChaID, 
                               TrangThai = @TrangThai
                           WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@MaDV", (object)donVi.MaDV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TenDV", (object)donVi.TenDV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CapDV", (object)donVi.CapDV ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViChaID", (object)donVi.DonViChaID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)donVi.TrangThai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID", donVi.ID);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && donViCu != null)
                    {
                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = donVi.UserID,
                            HanhDongID = GetHanhDongID("Sửa"),
                            BangTacDong = "tblDonVi",
                            BanGhiID = donVi.ID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"MaDV: {donViCu.MaDV}, TenDV: {donViCu.TenDV}, CapDV: {donViCu.CapDV}",
                            NoiDungMoi = $"MaDV: {donVi.MaDV}, TenDV: {donVi.TenDV}, CapDV: {donVi.CapDV}"
                        });
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật đơn vị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int id, int? userID = null)
        {
            // Lấy thông tin đơn vị trước khi xóa
            DonVi donViCu = GetByID(id);

            string query = "UPDATE tblDonVi SET TrangThai = (SELECT ID FROM tblTrangThai WHERE TrangThai = N'Xóa') WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && donViCu != null)
                    {
                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = userID,
                            HanhDongID = GetHanhDongID("Xóa"),
                            BangTacDong = "tblDonVi",
                            BanGhiID = id,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"MaDV: {donViCu.MaDV}, TenDV: {donViCu.TenDV}, CapDV: {donViCu.CapDV}",
                            NoiDungMoi = "Đã xóa"
                        });
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa đơn vị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
