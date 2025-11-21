using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCongViec.DataAccess;

namespace QuanLyCongViec
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            TestDatabaseConnection();
        }

        /// <summary>
        /// Test kết nối database khi form load
        /// </summary>
        private void TestDatabaseConnection()
        {
            try
            {
                bool isConnected = DatabaseHelper.TestConnection();
                if (isConnected)
                {
                    MessageBox.Show(
                        "✅ Kết nối database thành công!\n\n" +
                        "Database: QuanLyCongViec\n" +
                        "Connection String đã được cấu hình đúng.",
                        "Thành công",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "❌ Lỗi kết nối database!\n\n" +
                    "Chi tiết: " + ex.Message + "\n\n" +
                    "Vui lòng kiểm tra:\n" +
                    "1. SQL Server đang chạy\n" +
                    "2. Database 'QuanLyCongViec' đã được tạo\n" +
                    "3. Connection string trong App.config đúng\n\n" +
                    "Xem file README_SHARE.md trong project để biết cách setup.",
                    "Lỗi kết nối",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}

