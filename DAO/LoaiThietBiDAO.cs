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
    internal class LoaiThietBiDAO
    {
        private ConnectionDB conn = new ConnectionDB();

        public List<LoaiThietBi> GetAll()
        {
            List<LoaiThietBi> list = new List<LoaiThietBi>();
            string query = @"SELECT ID, TenLoai, MoTa, VongDoiNam FROM tblLoaiThietBi";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LoaiThietBi
                            {
                                ID = (int)reader["ID"],
                                TenLoai = reader["TenLoai"] != DBNull.Value ? reader["TenLoai"].ToString() : "",
                                MoTa = reader["MoTa"] != DBNull.Value ? reader["MoTa"].ToString() : "",
                                VongDoiNam = reader["VongDoiNam"] != DBNull.Value ? (int?)reader["VongDoiNam"] : null
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách loại thiết bị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(LoaiThietBi loaiThietBi)
        {
            string query = @"INSERT INTO tblLoaiThietBi (TenLoai, MoTa, VongDoiNam) 
                           VALUES (@TenLoai, @MoTa, @VongDoiNam)";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenLoai", (object)loaiThietBi.TenLoai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MoTa", (object)loaiThietBi.MoTa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VongDoiNam", (object)loaiThietBi.VongDoiNam ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm loại thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(LoaiThietBi loaiThietBi)
        {
            string query = @"UPDATE tblLoaiThietBi 
                           SET TenLoai = @TenLoai, 
                               MoTa = @MoTa, 
                               VongDoiNam = @VongDoiNam
                           WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenLoai", (object)loaiThietBi.TenLoai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MoTa", (object)loaiThietBi.MoTa ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@VongDoiNam", (object)loaiThietBi.VongDoiNam ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID", loaiThietBi.ID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật loại thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM tblLoaiThietBi WHERE ID = @ID";
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
                Console.WriteLine("Lỗi khi xóa loại thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
