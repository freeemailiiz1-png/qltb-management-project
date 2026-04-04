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
                           LEFT JOIN tblTrangThai tt ON dv.TrangThai = tt.ID";
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

        public bool Insert(DonVi donVi)
        {
            string query = @"INSERT INTO tblDonVi (MaDV, TenDV, CapDV, DonViChaID, TrangThai, NgayTao) 
                           VALUES (@MaDV, @TenDV, @CapDV, @DonViChaID, @TrangThai, @NgayTao)";
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
                    cmd.Parameters.AddWithValue("@NgayTao", (object)donVi.NgayTao ?? DateTime.Now);
                    return cmd.ExecuteNonQuery() > 0;
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
                    return cmd.ExecuteNonQuery() > 0;
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

        public bool Delete(int id)
        {
            string query = "DELETE FROM tblDonVi WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    return cmd.ExecuteNonQuery() > 0;
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
