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
    internal class CanBoDAO
    {
        private ConnectionDB conn = new ConnectionDB();

        public List<CanBo> GetAll()
        {
            List<CanBo> list = new List<CanBo>();
            string query = "SELECT ID, HoTen, CCCD, NgaySinh, Email FROM tblCanBo";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new CanBo
                            {
                                ID = (int)reader["ID"],
                                HoTen = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : "",
                                CCCD = reader["CCCD"] != DBNull.Value ? reader["CCCD"].ToString() : "",
                                NgaySinh = reader["NgaySinh"] != DBNull.Value ? (DateTime?)reader["NgaySinh"] : null,
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách cán bộ: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(CanBo canBo)
        {
            string query = @"INSERT INTO tblCanBo (HoTen, CCCD, NgaySinh, Email) 
                           VALUES (@HoTen, @CCCD, @NgaySinh, @Email)";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@HoTen", (object)canBo.HoTen ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CCCD", (object)canBo.CCCD ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgaySinh", (object)canBo.NgaySinh ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Email", (object)canBo.Email ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm cán bộ: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(CanBo canBo)
        {
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
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật cán bộ: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM tblCanBo WHERE ID = @ID";
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
                Console.WriteLine("Lỗi khi xóa cán bộ: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
