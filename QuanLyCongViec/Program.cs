using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyCongViec
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Uncomment dòng dưới để mở form tính hash (dùng để tạo script SQL)
            // Application.Run(new frmHashPassword());
            
            // Form đăng nhập chính
            Application.Run(new frmDangNhap());
        }
    }
}

