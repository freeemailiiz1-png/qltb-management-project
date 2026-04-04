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
    internal class LichSuThietBiDAO
    {
        private ConnectionDB conn = new ConnectionDB();

        public List<LichSuThietBi> GetAll()
        {
            List<LichSuThietBi> list = new List<LichSuThietBi>();
            string query = @"SELECT 
                                ls.ID, 
                                ls.ThietBiID, 
                                ls.HanhDongID, 
                                ls.CanBoCuID, 
                                ls.CanBoMoiID, 
                                ls.DonViCuID, 
                                ls.DonViMoiID, 
                                ls.ThoiDiem, 
                                ls.GhiChu,
                                tb.SerialNumber,
                                hd.TenHanhDong,
                                cbcu.HoTen AS TenCanBoCu,
                                cbmoi.HoTen AS TenCanBoMoi,
                                dvcu.TenDV AS TenDonViCu,
                                dvmoi.TenDV AS TenDonViMoi
                           FROM tblLichSuThietBi ls
                           LEFT JOIN tblThietBi tb ON ls.ThietBiID = tb.ID
                           LEFT JOIN tblHanhDong hd ON ls.HanhDongID = hd.ID
                           LEFT JOIN tblCanBo cbcu ON ls.CanBoCuID = cbcu.ID
                           LEFT JOIN tblCanBo cbmoi ON ls.CanBoMoiID = cbmoi.ID
                           LEFT JOIN tblDonVi dvcu ON ls.DonViCuID = dvcu.ID
                           LEFT JOIN tblDonVi dvmoi ON ls.DonViMoiID = dvmoi.ID
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
                            list.Add(new LichSuThietBi
                            {
                                ID = (int)reader["ID"],
                                ThietBiID = reader["ThietBiID"] != DBNull.Value ? (int?)reader["ThietBiID"] : null,
                                HanhDongID = reader["HanhDongID"] != DBNull.Value ? (int?)reader["HanhDongID"] : null,
                                CanBoCuID = reader["CanBoCuID"] != DBNull.Value ? (int?)reader["CanBoCuID"] : null,
                                CanBoMoiID = reader["CanBoMoiID"] != DBNull.Value ? (int?)reader["CanBoMoiID"] : null,
                                DonViCuID = reader["DonViCuID"] != DBNull.Value ? (int?)reader["DonViCuID"] : null,
                                DonViMoiID = reader["DonViMoiID"] != DBNull.Value ? (int?)reader["DonViMoiID"] : null,
                                ThoiDiem = reader["ThoiDiem"] != DBNull.Value ? (DateTime?)reader["ThoiDiem"] : null,
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : "",
                                SerialNumber = reader["SerialNumber"] != DBNull.Value ? reader["SerialNumber"].ToString() : "",
                                TenHanhDong = reader["TenHanhDong"] != DBNull.Value ? reader["TenHanhDong"].ToString() : "",
                                TenCanBoCu = reader["TenCanBoCu"] != DBNull.Value ? reader["TenCanBoCu"].ToString() : "",
                                TenCanBoMoi = reader["TenCanBoMoi"] != DBNull.Value ? reader["TenCanBoMoi"].ToString() : "",
                                TenDonViCu = reader["TenDonViCu"] != DBNull.Value ? reader["TenDonViCu"].ToString() : "",
                                TenDonViMoi = reader["TenDonViMoi"] != DBNull.Value ? reader["TenDonViMoi"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách lịch sử thiết bị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }

        public bool Insert(LichSuThietBi lichSu)
        {
            string query = @"INSERT INTO tblLichSuThietBi 
                           (ThietBiID, HanhDongID, CanBoCuID, CanBoMoiID, DonViCuID, DonViMoiID, ThoiDiem, GhiChu) 
                           VALUES (@ThietBiID, @HanhDongID, @CanBoCuID, @CanBoMoiID, @DonViCuID, @DonViMoiID, @ThoiDiem, @GhiChu)";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ThietBiID", (object)lichSu.ThietBiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@HanhDongID", (object)lichSu.HanhDongID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanBoCuID", (object)lichSu.CanBoCuID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@CanBoMoiID", (object)lichSu.CanBoMoiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViCuID", (object)lichSu.DonViCuID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@DonViMoiID", (object)lichSu.DonViMoiID ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ThoiDiem", (object)lichSu.ThoiDiem ?? DateTime.Now);
                    cmd.Parameters.AddWithValue("@GhiChu", (object)lichSu.GhiChu ?? DBNull.Value);
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm lịch sử thiết bị: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        public List<LichSuThietBi> GetByThietBiID(int thietBiID)
        {
            List<LichSuThietBi> list = new List<LichSuThietBi>();
            string query = @"SELECT 
                                ls.ID, 
                                ls.ThietBiID, 
                                ls.HanhDongID, 
                                ls.CanBoCuID, 
                                ls.CanBoMoiID, 
                                ls.DonViCuID, 
                                ls.DonViMoiID, 
                                ls.ThoiDiem, 
                                ls.GhiChu,
                                tb.SerialNumber,
                                hd.TenHanhDong,
                                cbcu.HoTen AS TenCanBoCu,
                                cbmoi.HoTen AS TenCanBoMoi,
                                dvcu.TenDV AS TenDonViCu,
                                dvmoi.TenDV AS TenDonViMoi
                           FROM tblLichSuThietBi ls
                           LEFT JOIN tblThietBi tb ON ls.ThietBiID = tb.ID
                           LEFT JOIN tblHanhDong hd ON ls.HanhDongID = hd.ID
                           LEFT JOIN tblCanBo cbcu ON ls.CanBoCuID = cbcu.ID
                           LEFT JOIN tblCanBo cbmoi ON ls.CanBoMoiID = cbmoi.ID
                           LEFT JOIN tblDonVi dvcu ON ls.DonViCuID = dvcu.ID
                           LEFT JOIN tblDonVi dvmoi ON ls.DonViMoiID = dvmoi.ID
                           WHERE ls.ThietBiID = @ThietBiID
                           ORDER BY ls.ThoiDiem DESC";
            try
            {
                conn.KetNoi();
                using (var cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ThietBiID", thietBiID);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            list.Add(new LichSuThietBi
                            {
                                ID = (int)reader["ID"],
                                ThietBiID = reader["ThietBiID"] != DBNull.Value ? (int?)reader["ThietBiID"] : null,
                                HanhDongID = reader["HanhDongID"] != DBNull.Value ? (int?)reader["HanhDongID"] : null,
                                CanBoCuID = reader["CanBoCuID"] != DBNull.Value ? (int?)reader["CanBoCuID"] : null,
                                CanBoMoiID = reader["CanBoMoiID"] != DBNull.Value ? (int?)reader["CanBoMoiID"] : null,
                                DonViCuID = reader["DonViCuID"] != DBNull.Value ? (int?)reader["DonViCuID"] : null,
                                DonViMoiID = reader["DonViMoiID"] != DBNull.Value ? (int?)reader["DonViMoiID"] : null,
                                ThoiDiem = reader["ThoiDiem"] != DBNull.Value ? (DateTime?)reader["ThoiDiem"] : null,
                                GhiChu = reader["GhiChu"] != DBNull.Value ? reader["GhiChu"].ToString() : "",
                                SerialNumber = reader["SerialNumber"] != DBNull.Value ? reader["SerialNumber"].ToString() : "",
                                TenHanhDong = reader["TenHanhDong"] != DBNull.Value ? reader["TenHanhDong"].ToString() : "",
                                TenCanBoCu = reader["TenCanBoCu"] != DBNull.Value ? reader["TenCanBoCu"].ToString() : "",
                                TenCanBoMoi = reader["TenCanBoMoi"] != DBNull.Value ? reader["TenCanBoMoi"].ToString() : "",
                                TenDonViCu = reader["TenDonViCu"] != DBNull.Value ? reader["TenDonViCu"].ToString() : "",
                                TenDonViMoi = reader["TenDonViMoi"] != DBNull.Value ? reader["TenDonViMoi"].ToString() : ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy lịch sử thiết bị: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return list;
        }
    }
}
