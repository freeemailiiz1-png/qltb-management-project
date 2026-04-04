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
    internal class HanhDongDAO
    {
        private ConnectionDB conn = new ConnectionDB();

        public List<HanhDong> GetAll()
        {
            List<HanhDong> list = new List<HanhDong>();
            string query = "SELECT ID, name FROM tblHanhDong";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new HanhDong
                            {
                                ID = (int)reader["ID"],
                                name = reader["name"] != DBNull.Value ? reader["name"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách hành động: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(HanhDong hanhDong)
        {
            string query = @"INSERT INTO tblHanhDong (name) VALUES (@name)";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@name", (object)hanhDong.name ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm hành động: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(HanhDong hanhDong)
        {
            string query = @"UPDATE tblHanhDong SET name = @name WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@name", (object)hanhDong.name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID", hanhDong.ID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật hành động: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM tblHanhDong WHERE ID = @ID";
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
                Console.WriteLine("Lỗi khi xóa hành động: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
