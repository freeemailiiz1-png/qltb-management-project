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
    internal class QuyenDAO
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

        /// <summary>
        /// Lấy thông tin quyền theo ID
        /// </summary>
        private Quyen GetByID(int id)
        {
            Quyen quyen = null;
            string query = "SELECT * FROM tblQuyen WHERE ID = @ID";
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
                            quyen = new Quyen
                            {
                                ID = (int)reader["ID"],
                                TenQuyen = reader["TenQuyen"] != DBNull.Value ? reader["TenQuyen"].ToString() : "",
                                TrangThai = reader["TrangThai"] != DBNull.Value ? (int?)reader["TrangThai"] : null
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin quyền: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return quyen;
        }

        public List<Quyen> GetAll()
        {
            List<Quyen> list = new List<Quyen>();
            string query = @"SELECT 
                                q.ID, 
                                q.TenQuyen, 
                                q.TrangThai,
                                tt.TrangThai AS TenTrangThai
                           FROM tblQuyen q 
                           LEFT JOIN tblTrangThai tt ON q.TrangThai = tt.ID
                           WHERE q.TrangThai = (SELECT ID FROM tblTrangThai WHERE TrangThai = N'Hoạt động')";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new Quyen
                            {
                                ID = (int)reader["ID"],
                                TenQuyen = reader["TenQuyen"] != DBNull.Value ? reader["TenQuyen"].ToString() : "",
                                TrangThai = reader["TrangThai"] != DBNull.Value ? (int?)reader["TrangThai"] : null,
                                TenTrangThai = reader["TenTrangThai"] != DBNull.Value ? reader["TenTrangThai"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách quyền: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(Quyen quyen)
        {
            string query = @"INSERT INTO tblQuyen (TenQuyen, TrangThai) 
                           VALUES (@TenQuyen, @TrangThai); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                conn.KetNoi();
                int newID = 0;
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenQuyen", (object)quyen.TenQuyen ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)quyen.TrangThai ?? DBNull.Value);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        newID = Convert.ToInt32(result);

                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = quyen.UserID,
                            HanhDongID = GetHanhDongID("Thêm"),
                            BangTacDong = "tblQuyen",
                            BanGhiID = newID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = null,
                            NoiDungMoi = $"TenQuyen: {quyen.TenQuyen}"
                        });

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm quyền: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(Quyen quyen)
        {
            // Lấy thông tin cũ trước khi cập nhật
            Quyen quyenCu = GetByID(quyen.ID);

            string query = @"UPDATE tblQuyen 
                           SET TenQuyen = @TenQuyen, 
                               TrangThai = @TrangThai
                           WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenQuyen", (object)quyen.TenQuyen ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)quyen.TrangThai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID", quyen.ID);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && quyenCu != null)
                    {
                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = quyen.UserID,
                            HanhDongID = GetHanhDongID("Sửa"),
                            BangTacDong = "tblQuyen",
                            BanGhiID = quyen.ID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"TenQuyen: {quyenCu.TenQuyen}",
                            NoiDungMoi = $"TenQuyen: {quyen.TenQuyen}"
                        });
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật quyền: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int id, int? userID = null)
        {
            // Lấy thông tin trước khi xóa
            Quyen quyenCu = GetByID(id);

            string query = "UPDATE tblQuyen SET TrangThai = (SELECT ID FROM tblTrangThai WHERE TrangThai = N'Xóa') WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && quyenCu != null)
                    {
                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = userID,
                            HanhDongID = GetHanhDongID("Xóa"),
                            BangTacDong = "tblQuyen",
                            BanGhiID = id,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"TenQuyen: {quyenCu.TenQuyen}",
                            NoiDungMoi = "Đã xóa"
                        });
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa quyền: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
