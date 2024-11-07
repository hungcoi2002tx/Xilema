using Cinema.Models;
using Share;
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

namespace Cinema
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private CinemaContext _context;
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            using (var context = new CinemaContext())
            {
                var user = context.Users.FirstOrDefault(u => u.Account == username && u.Password == password);

                if (user != null)
                {
                    Session.UserId = user.Id;
                    Session.Role = user.Role;   
                    MessageBox.Show($"Welcome {user.Name}!");
                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                    this.Close();   
                    // Nếu đăng nhập thành công, bạn có thể mở một cửa sổ khác hoặc chuyển hướng.
                }
                else
                {
                    ErrorText.Text = "Invalid username or password.";
                }
            }
        }

        private void RegisterLink_Click(object sender, MouseButtonEventArgs e)
        {
            Register registerWindow = new Register();
            registerWindow.Show();
            this.Close(); // Đóng cửa sổ đăng nhập
        }
    }
}
