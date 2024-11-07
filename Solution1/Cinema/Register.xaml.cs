using Cinema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Share.Helper;
using Share;
using System.Data;
using Microsoft.EntityFrameworkCore;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        public Register()
        {
            InitializeComponent();
        }
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy giá trị từ các TextBox
            string name = NameTextBox.Text.Trim();
            string account = AccountTextBox.Text.Trim();
            string password = PasswordBox.Password.Trim();

            // Kiểm tra tính hợp lệ
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                NameTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(account))
            {
                MessageBox.Show("Vui lòng nhập tài khoản.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                AccountTextBox.Focus();
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                PasswordBox.Focus();
                return;
            }
            
            var user = new User()
            {
                Id = StringExtentions.GetIdByGuid(),
                Name = name,
                Account = account,
                Password = password,
                Role = RoleEnum.Staff.ToString()
            };

            using (var context = new CinemaContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }
            // Thực hiện đăng ký (sau khi tất cả các trường đã hợp lệ)
            MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            Login loginWindow = new Login();
            loginWindow.Show();
            this.Close();
        }
    }
}
