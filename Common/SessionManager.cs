using QuanLyThietBi.DTO;
using System;

namespace QuanLyThietBi.Common
{
    /// <summary>
    /// Qu?n lý thông tin phiên ??ng nh?p c?a ng??i dùng hi?n t?i.
    /// </summary>
    public static class SessionManager
    {
        private static User currentUser = null;

        /// <summary>
        /// L?u thông tin ng??i dùng ?ang ??ng nh?p.
        /// </summary>
        /// <param name="user">??i t??ng User ?ang ??ng nh?p.</param>
        public static void SetCurrentUser(User user)
        {
            currentUser = user;
        }

        /// <summary>
        /// L?y thông tin ng??i dùng ?ang ??ng nh?p.
        /// </summary>
        /// <returns>??i t??ng User ?ang ??ng nh?p, ho?c null n?u ch?a ??ng nh?p.</returns>
        public static User GetCurrentUser()
        {
            return currentUser;
        }

        /// <summary>
        /// L?y ID c?a ng??i dùng ?ang ??ng nh?p.
        /// </summary>
        /// <returns>ID c?a ng??i dùng, ho?c null n?u ch?a ??ng nh?p.</returns>
        public static int? GetCurrentUserID()
        {
            return currentUser?.ID;
        }

        /// <summary>
        /// L?y tên ??ng nh?p c?a ng??i dùng hi?n t?i.
        /// </summary>
        /// <returns>Tên ??ng nh?p, ho?c chu?i r?ng n?u ch?a ??ng nh?p.</returns>
        public static string GetCurrentUsername()
        {
            return currentUser?.TenDangNhap ?? "";
        }

        /// <summary>
        /// Ki?m tra xem có ng??i dùng ?ang ??ng nh?p hay không.
        /// </summary>
        /// <returns>True n?u ?ã ??ng nh?p, False n?u ch?a.</returns>
        public static bool IsLoggedIn()
        {
            return currentUser != null;
        }

        /// <summary>
        /// Xóa thông tin phiên ??ng nh?p (??ng xu?t).
        /// </summary>
        public static void Logout()
        {
            currentUser = null;
        }
    }
}
