using QuanLyThietBi.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyThietBi.DAO
{

    internal class CapDonViDAO
    {
        private ConnectionDB conn = new ConnectionDB();
        public List<DTO.CapDonVi> GetAll()
        {
            List<DTO.CapDonVi> list = new List<DTO.CapDonVi>();
            string query = "SELECT ID, TenCapDV FROM tblCapDV";
            try
            {
                conn.KetNoi();
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new DTO.CapDonVi
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

        public bool Insert(DTO.CapDonVi capDonVi)
        {
            string query = "INSERT INTO tblCapDV (TenCapDV) VALUES (@TenCapDonVi)";
            try
            {
                conn.KetNoi();
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenCapDonVi", capDonVi.TenCapDV);
                    return cmd.ExecuteNonQuery() > 0;
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

        public bool Update(DTO.CapDonVi capDonVi)
        {
            string query = "UPDATE tblCapDV SET TenCapDV = @TenCapDonVi WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenCapDonVi", capDonVi.TenCapDV);
                    cmd.Parameters.AddWithValue("@ID", capDonVi.ID);
                    return cmd.ExecuteNonQuery() > 0;
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

        public bool Delete(int id)
        {
            string query = "DELETE FROM tblCapDV WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new System.Data.SqlClient.SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", id);
                    return cmd.ExecuteNonQuery() > 0;
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
