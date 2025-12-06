using System;
using System.Security.Cryptography;
using System.Text;

namespace QuanLyCongViec.Helpers
{
    /// <summary>
    /// Helper class để hash và verify mật khẩu
    /// Sử dụng SHA256 với salt để bảo mật
    /// </summary>
    public static class PasswordHelper
    {
        private const string Salt = "QuanLyCongViec_Salt_2024";

        /// <summary>
        /// Hash mật khẩu sử dụng SHA256
        /// </summary>
        /// <param name="password">Mật khẩu cần hash</param>
        /// <returns>Mật khẩu đã được hash</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống", nameof(password));
            }

            // Kết hợp password với salt
            string saltedPassword = password + Salt;

            // Hash bằng SHA256
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(saltedPassword);
                byte[] hash = sha256.ComputeHash(bytes);
                
                // Chuyển đổi byte array thành string hex
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    stringBuilder.Append(hash[i].ToString("x2"));
                }
                
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// Verify mật khẩu có khớp với hash không
        /// </summary>
        /// <param name="password">Mật khẩu cần kiểm tra</param>
        /// <param name="hash">Hash đã lưu trong database</param>
        /// <returns>True nếu mật khẩu khớp</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(hash))
            {
                return false;
            }

            string hashedPassword = HashPassword(password);
            return hashedPassword.Equals(hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}

