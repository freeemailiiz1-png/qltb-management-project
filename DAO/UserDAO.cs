using QuanLyThietBi.Common;
using QuanLyThietBi.DTO;
using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Data;

namespace QuanLyThietBi.DAO
{
    internal class UserDAO
    {
        private ConnectionDB conn = new ConnectionDB();

        // --- Phương thức mã hóa mật khẩu ---
        private string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return null;
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    builder.Append(hash[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Kiểm tra thông tin đăng nhập của người dùng.
        /// </summary>
        /// <param name="tenDangNhap">Tên đăng nhập</param>
        /// <param name="matKhau">Mật khẩu (chưa mã hóa)</param>
        /// <returns>Trả về đối tượng User nếu đăng nhập thành công, ngược lại trả về null.</returns>
        public User Login(string tenDangNhap, string matKhau)
        {
            string hashedPassword = HashPassword(matKhau);
            User user = null;

            try
            {
                conn.KetNoi();
                string query = "SELECT ID, TenDangNhap, MatKhau, DonViID, CapDonViID, TrangThai FROM tblUser WHERE TenDangNhap = @TenDangNhap AND MatKhau = @MatKhau AND TrangThai = 1";

                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                ID = (int)reader["ID"],
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                MatKhau = reader["MatKhau"].ToString()
                                //DonViID = (int)reader["DonViID"],
                                //CapDonViID = (int)reader["CapDonViID"],
                                //TrangThai = (int)reader["TrangThai"]
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Có thể ghi log lỗi ở đây
                Console.WriteLine("Lỗi khi đăng nhập: " + ex.Message);
                return null;
            }
            finally
            {
                conn.NgatKetNoi();
            }

            return user;
        }

        /// <summary>
        /// Thêm một người dùng mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="user">Đối tượng User chứa thông tin người dùng mới.</param>
        /// <returns>True nếu thêm thành công, False nếu thất bại.</returns>
        public bool AddUser(User user)
        {
            string hashedPassword = HashPassword(user.MatKhau);
            string query = "INSERT INTO tblUser (TenDangNhap, MatKhau, DonViID, CapDonViID, TrangThai) VALUES (@TenDangNhap, @MatKhau, @DonViID, @CapDonViID, @TrangThai)";

            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", user.TenDangNhap);
                    cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
                    cmd.Parameters.AddWithValue("@DonViID", user.DonViID);
                    cmd.Parameters.AddWithValue("@CapDonViID", user.CapDonViID);
                    cmd.Parameters.AddWithValue("@TrangThai", user.TrangThai);

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm người dùng: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        /// <summary>
        /// Cập nhật thông tin người dùng.
        /// </summary>
        /// <param name="user">Đối tượng User chứa thông tin cần cập nhật.</param>
        /// <returns>True nếu cập nhật thành công, False nếu thất bại.</returns>
        public bool UpdateUser(User user)
        {
            // Nếu mật khẩu không rỗng, cập nhật cả mật khẩu đã mã hóa
            bool updatePassword = !string.IsNullOrEmpty(user.MatKhau);
            string query;

            if (updatePassword)
            {
                query = "UPDATE tblUser SET TenDangNhap = @TenDangNhap, MatKhau = @MatKhau, DonViID = @DonViID, CapDonViID = @CapDonViID, TrangThai = @TrangThai WHERE ID = @ID";
            }
            else // Nếu mật khẩu rỗng, chỉ cập nhật các thông tin khác
            {
                query = "UPDATE tblUser SET TenDangNhap = @TenDangNhap, DonViID = @DonViID, CapDonViID = @CapDonViID, TrangThai = @TrangThai WHERE ID = @ID";
            }

            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@TenDangNhap", user.TenDangNhap);
                    cmd.Parameters.AddWithValue("@DonViID", user.DonViID);
                    cmd.Parameters.AddWithValue("@CapDonViID", user.CapDonViID);
                    cmd.Parameters.AddWithValue("@TrangThai", user.TrangThai);
                    cmd.Parameters.AddWithValue("@ID", user.ID);

                    if (updatePassword)
                    {
                        string hashedPassword = HashPassword(user.MatKhau);
                        cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
                    }

                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật người dùng: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        /// <summary>
        /// Xóa một người dùng khỏi cơ sở dữ liệu.
        /// </summary>
        /// <param name="userID">ID của người dùng cần xóa.</param>
        /// <returns>True nếu xóa thành công, False nếu thất bại.</returns>
        public bool DeleteUser(int userID)
        {
            string query = "DELETE FROM tblUser WHERE ID = @ID";
            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", userID);
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi xóa người dùng: " + ex.Message);
                return false;
            }
            finally
            {
                conn.NgatKetNoi();
            }
        }

        /// <summary>
        /// Lấy danh sách tất cả người dùng từ cơ sở dữ liệu.
        /// </summary>
        /// <returns>Một DataTable chứa danh sách người dùng.</returns>
        public DataTable GetAllUsers()
        {
            DataTable dt = new DataTable();
            string query = "SELECT ID, TenDangNhap, DonViID, CapDonViID, TrangThai FROM tblUser"; 
            try
            {
                conn.KetNoi();
                using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn.sqlCon))
                {
                    adapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy danh sách người dùng: " + ex.Message);
            }
            finally
            {
                conn.NgatKetNoi();
            }
            return dt;
        }

        /// <summary>
        /// Lấy thông tin chi tiết của một người dùng bằng ID.
        /// </summary>
        /// <param name="userID">ID của người dùng cần tìm.</param>
        /// <returns>Đối tượng User nếu tìm thấy, ngược lại trả về null.</returns>
        public User GetUserByID(int userID)
        {
            User user = null;
            // Không lấy mật khẩu để bảo mật
            string query = "SELECT ID, TenDangNhap, DonViID, CapDonViID, TrangThai FROM tblUser WHERE ID = @ID";

            try
            {
                conn.KetNoi();
                using (SqlCommand cmd = new SqlCommand(query, conn.sqlCon))
                {
                    cmd.Parameters.AddWithValue("@ID", userID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            user = new User
                            {
                                ID = (int)reader["ID"],
                                TenDangNhap = reader["TenDangNhap"].ToString(),
                                DonViID = reader["DonViID"] != DBNull.Value ? Convert.ToInt32(reader["DonViID"]) : 0,
                                CapDonViID = reader["CapDonViID"] != DBNull.Value ? Convert.ToInt32(reader["CapDonViID"]) : 0,
                                TrangThai = reader["TrangThai"] != DBNull.Value ? Convert.ToInt32(reader["TrangThai"]) : 0
                            };
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy thông tin người dùng bằng ID: " + ex.Message);
                return null;
            }
            finally
            {
                conn.NgatKetNoi();
            }

            return user;
        }
    }
}
