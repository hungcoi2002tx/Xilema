using Share;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadUserInterface();
        }

        private void LoadUserInterface()
        {
            // Kiểm tra vai trò người dùng và hiển thị button tương ứng
            if (Session.Role == RoleEnum.Admin.ToString())
            {
                ShowAdminButtons();
            }
            else if (Session.Role == RoleEnum.Staff.ToString())
            {
                ShowStaffButton();
            }
        }

        private void ShowStaffButton()
        {
            try
            {
                btnTicket.Visibility = Visibility.Visible;
                btnCategory.Visibility = Visibility.Hidden;
                btnFilm.Visibility = Visibility.Hidden;
                btnRoom.Visibility = Visibility.Hidden;
                btnSchedule.Visibility = Visibility.Hidden;
                btnTicketMn.Visibility = Visibility.Hidden;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void ShowAdminButtons()
        {
            try
            {
                btnTicket.Visibility = Visibility.Hidden;
                btnCategory.Visibility = Visibility.Visible;
                btnFilm.Visibility = Visibility.Visible;
                btnRoom.Visibility = Visibility.Visible;
                btnSchedule.Visibility = Visibility.Visible;
                btnTicketMn.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        private void ManageRoomButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ Quản lý phòng chiếu
            var manageRoomWindow = new RoomManagement();
            manageRoomWindow.Show();
            this.Hide();
        }

        private void ManageMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ Quản lý phim
            var manageMoviesWindow = new FilmManagement();
            manageMoviesWindow.Show();
            this.Hide();
        }

        private void ManageGenreCountryButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ Quản lý thể loại và quốc gia
            var manageGenreCountryWindow = new GenreManagement();
            manageGenreCountryWindow.Show();
            this.Hide();
        }

        private void ScheduleMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ Lên lịch chiếu
            var scheduleMoviesWindow = new ShowWindow();
            scheduleMoviesWindow.Show();
            this.Hide();
        }

        private void ManageBookingButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ Quản lý đặt vé
            var manageBookingWindow = new BookingWindow();
            manageBookingWindow.Show();
            this.Hide();
        }

        private void ManageBookingMnButton_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ Quản lý đặt vé
            var manageBookingWindow = new BookingManagement();
            manageBookingWindow.Show();
            this.Hide();
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            Session.Clear();
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}