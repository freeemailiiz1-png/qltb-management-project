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

        public List<Quyen> GetAll()
        {
            List<Quyen> list = new List<Quyen>();
            string query = @"SELECT 
                                q.ID, 
                                q.TenQuyen, 
                                q.TrangThai,
                                tt.TrangThai AS TenTrangThai
                           FROM tblQuyen q 
                           LEFT JOIN tblTrangThai tt ON q.TrangThai = tt.ID";
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
                           VALUES (@TenQuyen, @TrangThai)";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenQuyen", (object)quyen.TenQuyen ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)quyen.TrangThai ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
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
                    return cmd.ExecuteNonQuery() > 0;
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

        public bool Delete(int id)
        {
            string query = "DELETE FROM tblQuyen WHERE ID = @ID";
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
