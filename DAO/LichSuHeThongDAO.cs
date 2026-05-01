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
    internal class LichSuHeThongDAO
    {
        private ConnectionDB conn = new ConnectionDB();
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
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Lỗi khi lấy HanhDongID: {ex.Message}");
                return null;
            }
        }

        public List<LichSuHeThong> GetAll()
        {
            List<LichSuHeThong> list = new List<LichSuHeThong>();
            string query = @"SELECT 
                                ls.ID, 
                                ls.UserID, 
                                ls.HanhDong, 
                                ls.BangTacDong, 
                                ls.BanGhiID, 
                                ls.ThoiDiem, 
                                ls.NoiDungCu, 
                                ls.NoiDungMoi,
                                u.TenDangNhap AS TenUser,
                                hd.Name AS TenHanhDong
                           FROM tblLichSuHeThong ls
                           LEFT JOIN tblUser u ON ls.UserID = u.ID
                           LEFT JOIN tblHanhDong hd ON ls.HanhDong = hd.ID
                           ORDER BY ls.ThoiDiem DESC";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LichSuHeThong
                            {
                                ID = (int)reader["ID"],
                                UserID = reader["UserID"] != DBNull.Value ? (int?)reader["UserID"] : null,
                                HanhDongID = reader["HanhDong"] != DBNull.Value ? (int?)reader["HanhDong"] : null,
                                BangTacDong = reader["BangTacDong"] != DBNull.Value ? reader["BangTacDong"].ToString() : "",
                                BanGhiID = reader["BanGhiID"] != DBNull.Value ? (int?)reader["BanGhiID"] : null,
                                ThoiDiem = reader["ThoiDiem"] != DBNull.Value ? (DateTime?)reader["ThoiDiem"] : null,
                                NoiDungCu = reader["NoiDungCu"] != DBNull.Value ? reader["NoiDungCu"].ToString() : "",
                                NoiDungMoi = reader["NoiDungMoi"] != DBNull.Value ? reader["NoiDungMoi"].ToString() : "",
                                TenUser = reader["TenUser"] != DBNull.Value ? reader["TenUser"].ToString() : "",
                                TenHanhDong = reader["TenHanhDong"] != DBNull.Value ? reader["TenHanhDong"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách lịch sử hệ thống: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(LichSuHeThong lichSu)
        {
            string query = @"INSERT INTO tblLichSuHeThong 
                           (UserID, HanhDong, BangTacDong, BanGhiID, ThoiDiem, NoiDungCu, NoiDungMoi) 
                           VALUES (@UserID, @HanhDongID, @BangTacDong, @BanGhiID, @ThoiDiem, @NoiDungCu, @NoiDungMoi)";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@UserID", (object)lichSu.UserID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@HanhDongID", (object)lichSu.HanhDongID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BangTacDong", (object)lichSu.BangTacDong ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@BanGhiID", (object)lichSu.BanGhiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ThoiDiem", (object)lichSu.ThoiDiem ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@NoiDungCu", (object)lichSu.NoiDungCu ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NoiDungMoi", (object)lichSu.NoiDungMoi ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm lịch sử hệ thống: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public List<LichSuHeThong> GetByUser(int userID)
        {
            List<LichSuHeThong> list = new List<LichSuHeThong>();
            string query = @"SELECT 
                                ls.ID, 
                                ls.UserID, 
                                ls.HanhDong, 
                                ls.BangTacDong, 
                                ls.BanGhiID, 
                                ls.ThoiDiem, 
                                ls.NoiDungCu, 
                                ls.NoiDungMoi,
                                u.TenDangNhap AS TenUser,
                                hd.Name AS TenHanhDong
                           FROM tblLichSuHeThong ls
                           LEFT JOIN tblUser u ON ls.UserID = u.ID
                           LEFT JOIN tblHanhDong hd ON ls.HanhDong = hd.ID
                           WHERE ls.UserID = @UserID
                           ORDER BY ls.ThoiDiem DESC";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@UserID", userID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LichSuHeThong
                            {
                                ID = (int)reader["ID"],
                                UserID = reader["UserID"] != DBNull.Value ? (int?)reader["UserID"] : null,
                                HanhDongID = reader["HanhDong"] != DBNull.Value ? (int?)reader["HanhDong"] : null,
                                BangTacDong = reader["BangTacDong"] != DBNull.Value ? reader["BangTacDong"].ToString() : "",
                                BanGhiID = reader["BanGhiID"] != DBNull.Value ? (int?)reader["BanGhiID"] : null,
                                ThoiDiem = reader["ThoiDiem"] != DBNull.Value ? (DateTime?)reader["ThoiDiem"] : null,
                                NoiDungCu = reader["NoiDungCu"] != DBNull.Value ? reader["NoiDungCu"].ToString() : "",
                                NoiDungMoi = reader["NoiDungMoi"] != DBNull.Value ? reader["NoiDungMoi"].ToString() : "",
                                TenUser = reader["TenUser"] != DBNull.Value ? reader["TenUser"].ToString() : "",
                                TenHanhDong = reader["TenHanhDong"] != DBNull.Value ? reader["TenHanhDong"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy lịch sử theo user: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public List<LichSuHeThong> GetByTable(string bangTacDong)
        {
            List<LichSuHeThong> list = new List<LichSuHeThong>();
            string query = @"SELECT 
                                ls.ID, 
                                ls.UserID, 
                                ls.HanhDong, 
                                ls.BangTacDong, 
                                ls.BanGhiID, 
                                ls.ThoiDiem, 
                                ls.NoiDungCu, 
                                ls.NoiDungMoi,
                                u.TenDangNhap AS TenUser,
                                hd.Name AS TenHanhDong
                           FROM tblLichSuHeThong ls
                           LEFT JOIN tblUser u ON ls.UserID = u.ID
                           LEFT JOIN tblHanhDong hd ON ls.HanhDong = hd.ID
                           WHERE ls.BangTacDong = @BangTacDong
                           ORDER BY ls.ThoiDiem DESC";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@BangTacDong", bangTacDong);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LichSuHeThong
                            {
                                ID = (int)reader["ID"],
                                UserID = reader["UserID"] != DBNull.Value ? (int?)reader["UserID"] : null,
                                HanhDongID = reader["HanhDong"] != DBNull.Value ? (int?)reader["HanhDong"] : null,
                                BangTacDong = reader["BangTacDong"] != DBNull.Value ? reader["BangTacDong"].ToString() : "",
                                BanGhiID = reader["BanGhiID"] != DBNull.Value ? (int?)reader["BanGhiID"] : null,
                                ThoiDiem = reader["ThoiDiem"] != DBNull.Value ? (DateTime?)reader["ThoiDiem"] : null,
                                NoiDungCu = reader["NoiDungCu"] != DBNull.Value ? reader["NoiDungCu"].ToString() : "",
                                NoiDungMoi = reader["NoiDungMoi"] != DBNull.Value ? reader["NoiDungMoi"].ToString() : "",
                                TenUser = reader["TenUser"] != DBNull.Value ? reader["TenUser"].ToString() : "",
                                TenHanhDong = reader["TenHanhDong"] != DBNull.Value ? reader["TenHanhDong"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy lịch sử theo bảng: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }
    }
}
