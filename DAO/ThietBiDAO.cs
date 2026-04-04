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
    internal class ThietBiDAO
    {
        private ConnectionDB conn = new ConnectionDB();

        public List<ThietBi> GetAll()
        {
            List<ThietBi> list = new List<ThietBi>();
            string query = @"SELECT 
                                tb.ID, 
                                tb.LoaiID, 
                                tb.SerialNumber, 
                                tb.CanBoID, 
                                tb.DonViID, 
                                tb.NgayCap, 
                                tb.NgayHetHan, 
                                tb.NgayHetHanVongDoi, 
                                tb.TrangThai, 
                                tb.GhiChu,
                                ltb.TenLoai,
                                cb.HoTen AS TenCanBo,
                                dv.TenDV AS TenDonVi,
                                tt.TrangThai AS TenTrangThai
                           FROM tblThietBi tb 
                           LEFT JOIN tblLoaiThietBi ltb ON tb.LoaiID = ltb.ID
                           LEFT JOIN tblCanBo cb ON tb.CanBoID = cb.ID
                           LEFT JOIN tblDonVi dv ON tb.DonViID = dv.ID
                           LEFT JOIN tblTrangThai tt ON tb.TrangThai = tt.ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new ThietBi
                            {
                                ID = (int)reader["ID"],
                                LoaiID = reader["LoaiID"] != DBNull.Value ? (int?)reader["LoaiID"] : null,
                                SerialNumber = reader["SerialNumber"] != DBNull.Value ? reader["SerialNumber"].ToString() : "",
                                CanBoID = reader["CanBoID"] != DBNull.Value ? (int?)reader["CanBoID"] : null,
                                DonViID = reader["DonViID"] != DBNull.Value ? (int?)reader["DonViID"] : null,
                                NgayCap = reader["NgayCap"] != DBNull.Value ? (DateTime?)reader["NgayCap"] : null,
                                NgayHetHan = reader["NgayHetHan"] != DBNull.Value ? (DateTime?)reader["NgayHetHan"] : null,
                                NgayHetHanVongDoi = reader["NgayHetHanVongDoi"] != DBNull.Value ? (DateTime?)reader["NgayHetHanVongDoi"] : null,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? (int?)reader["TrangThai"] : null,
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : "",
                                TenLoai = reader["TenLoai"] != DBNull.Value ? reader["TenLoai"].ToString() : "",
                                TenCanBo = reader["TenCanBo"] != DBNull.Value ? reader["TenCanBo"].ToString() : "",
                                TenDonVi = reader["TenDonVi"] != DBNull.Value ? reader["TenDonVi"].ToString() : "",
                                TenTrangThai = reader["TenTrangThai"] != DBNull.Value ? reader["TenTrangThai"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách thiết bị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(ThietBi thietBi)
        {
            string query = @"INSERT INTO tblThietBi (LoaiID, SerialNumber, CanBoID, DonViID, NgayCap, NgayHetHan, NgayHetHanVongDoi, TrangThai, GhiChu) 
                           VALUES (@LoaiID, @SerialNumber, @CanBoID, @DonViID, @NgayCap, @NgayHetHan, @NgayHetHanVongDoi, @TrangThai, @GhiChu)";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@LoaiID", (object)thietBi.LoaiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SerialNumber", (object)thietBi.SerialNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanBoID", (object)thietBi.CanBoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViID", (object)thietBi.DonViID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayCap", (object)thietBi.NgayCap ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHan", (object)thietBi.NgayHetHan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHanVongDoi", (object)thietBi.NgayHetHanVongDoi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)thietBi.TrangThai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)thietBi.GhiChu ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Update(ThietBi thietBi)
        {
            string query = @"UPDATE tblThietBi 
                           SET LoaiID = @LoaiID, 
                               SerialNumber = @SerialNumber, 
                               CanBoID = @CanBoID, 
                               DonViID = @DonViID, 
                               NgayCap = @NgayCap, 
                               NgayHetHan = @NgayHetHan, 
                               NgayHetHanVongDoi = @NgayHetHanVongDoi, 
                               TrangThai = @TrangThai, 
                               GhiChu = @GhiChu
                           WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@LoaiID", (object)thietBi.LoaiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@SerialNumber", (object)thietBi.SerialNumber ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanBoID", (object)thietBi.CanBoID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViID", (object)thietBi.DonViID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayCap", (object)thietBi.NgayCap ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHan", (object)thietBi.NgayHetHan ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@NgayHetHanVongDoi", (object)thietBi.NgayHetHanVongDoi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@TrangThai", (object)thietBi.TrangThai ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)thietBi.GhiChu ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ID", thietBi.ID);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public bool Delete(int id)
        {
            string query = "DELETE FROM tblThietBi WHERE ID = @ID";
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
                Console.WriteLine("Lỗi khi xóa thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }
    }
}
