using QuanLyThietBi.Common;
using QuanLyThietBi.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DAO
{
    internal class CapDonViDAO
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
        /// Lấy thông tin cấp đơn vị theo ID
        /// </summary>
        private CapDonVi GetByID(int id)
        {
            CapDonVi capDonVi = null;
            string query = "SELECT ID, TenCapDV FROM tblCapDV WHERE ID = @ID";
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
                            capDonVi = new CapDonVi
                            {
                                ID = (int)reader["ID"],
                                TenCapDV = reader["TenCapDV"].ToString()
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin cấp đơn vị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return capDonVi;
        }

        public List<CapDonVi> GetAll()
        {
            List<CapDonVi> list = new List<CapDonVi>();
            string query = "SELECT ID, TenCapDV FROM tblCapDV";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CapDonVi
                            {
                                ID = (int)reader["ID"],
                                TenCapDV = reader["TenCapDV"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách cấp đơn vị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(CapDonVi capDonVi)
        {
            string query = "INSERT INTO tblCapDV (TenCapDV) VALUES (@TenCapDonVi); SELECT CAST(SCOPE_IDENTITY() AS INT);";
            try
            {
                conn.KetNoi();
                int newID = 0;
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenCapDonVi", capDonVi.TenCapDV);

                    var result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        newID = Convert.ToInt32(result);

                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = capDonVi.UserID,
                            HanhDongID = GetHanhDongID("Thêm"),
                            BangTacDong = "tblCapDV",
                            BanGhiID = newID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = null,
                            NoiDungMoi = $"TenCapDV: {capDonVi.TenCapDV}"
                        });

                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm cấp đơn vị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(CapDonVi capDonVi)
        {
            // Lấy thông tin cũ trước khi cập nhật
            CapDonVi capDonViCu = GetByID(capDonVi.ID);

            string query = "UPDATE tblCapDV SET TenCapDV = @TenCapDonVi WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenCapDonVi", capDonVi.TenCapDV);
                    cmd.Parameters.AddWithValue("@ID", capDonVi.ID);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && capDonViCu != null)
                    {
                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = capDonVi.UserID,
                            HanhDongID = GetHanhDongID("Sửa"),
                            BangTacDong = "tblCapDV",
                            BanGhiID = capDonVi.ID,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"TenCapDV: {capDonViCu.TenCapDV}",
                            NoiDungMoi = $"TenCapDV: {capDonVi.TenCapDV}"
                        });
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật cấp đơn vị: " + ex.Message);
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
            CapDonVi capDonViCu = GetByID(id);

            string query = "DELETE FROM tblCapDV WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && capDonViCu != null)
                    {
                        // Ghi lịch sử hệ thống
                        lichSuHeThongDAO.Insert(new LichSuHeThong
                        {
                            UserID = userID,
                            HanhDongID = GetHanhDongID("Xóa"),
                            BangTacDong = "tblCapDV",
                            BanGhiID = id,
                            ThoiDiem = DateTime.Now,
                            NoiDungCu = $"TenCapDV: {capDonViCu.TenCapDV}",
                            NoiDungMoi = "Đã xóa"
                        });
                    }

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa cấp đơn vị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
