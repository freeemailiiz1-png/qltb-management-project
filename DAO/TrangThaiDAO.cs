using QuanLyThietBi.Common;
using QuanLyThietBi.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DAO
{
    public class TrangThaiDAO
    {
        private ConnectionDB conn = new ConnectionDB();

        public List<TrangThai> GetAll()
        {
            List<TrangThai> list = new List<TrangThai>();
            string query = "SELECT ID, TrangThai, MoTa FROM tblTrangThai";
            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new TrangThai
                            {
                                IDTrangThai = (int)reader["ID"],
                                trangThai = reader["TrangThai"].ToString(),
                                MoTa = reader["MoTa"] != DBNull.Value ? reader["MoTa"].ToString() : null
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách trạng thái: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(TrangThai trangThai)
        {
            string query = "INSERT INTO tblTrangThai (TrangThai, MoTa) VALUES (@TenTrangThai, @MoTa)";
            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenTrangThai", trangThai.trangThai);
                    cmd.Parameters.AddWithValue("@MoTa", trangThai.MoTa ?? (object)DBNull.Value);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm trạng thái: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(TrangThai trangThai)
        {
            string query = "UPDATE tblTrangThai SET TrangThai = @TenTrangThai, MoTa = @MoTa WHERE ID = @IDTrangThai";
            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenTrangThai", trangThai.trangThai);
                    cmd.Parameters.AddWithValue("@MoTa", trangThai.MoTa ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@IDTrangThai", trangThai.IDTrangThai);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật trạng thái: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int idTrangThai)
        {
            string query = "DELETE FROM tblTrangThai WHERE ID = @IDTrangThai";
            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@IDTrangThai", idTrangThai);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa trạng thái: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public TrangThai GetByID(int idTrangThai)
        {
            TrangThai trangThai = null;
            string query = "SELECT IDTrangThai, TenTrangThai, MoTa FROM tblTrangThai WHERE ID = @IDTrangThai";
            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@IDTrangThai", idTrangThai);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            trangThai = new TrangThai
                            {
                                IDTrangThai = (int)reader["IDTrangThai"],
                                trangThai = reader["TrangThai"].ToString(),
                                MoTa = reader["MoTa"] != DBNull.Value ? reader["MoTa"].ToString() : null
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy trạng thái theo ID: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return trangThai;
        }


    }
}
