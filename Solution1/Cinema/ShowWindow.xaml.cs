using Cinema.Models;
using Share;
using Share.DTO;
using Share.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Share;
namespace Cinema
{
    /// <summary>
    /// Interaction logic for ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        private List<ShowDTO> ShowsList;
        public ObservableCollection<Room> RoomFilters { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<Film> FilmFilters { get; set; } = new ObservableCollection<Film>();
        public ObservableCollection<string> ShowStatusFilters { get; set; } = new ObservableCollection<string>();

        public ShowWindow()
        {
            InitializeComponent();
            LoadData();
            LoadComboBoxes();
            DataContext = this;
        }

        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (RoomComboBox.SelectedItem is Room selectedRoom)
            {
                // Thực hiện các hành động với selectedRoom
                MessageBox.Show($"Selected Room: {selectedRoom.Name}, ID: {selectedRoom.RoomId}");
            }
        }

        private void LoadComboBoxes()
        {
            using (var context = new CinemaContext())
            {// Tạo danh sách phòng
                var rooms = context.Rooms.ToList();
                foreach (var room in rooms)
                {
                    RoomFilters.Add(room);
                }

                // Tạo danh sách phim
                List<Film> films = context.Films.ToList();
                foreach (var film in films)
                {
                    FilmFilters.Add(film);
                }

                foreach (Share.ShowStaus status in Enum.GetValues(typeof(Share.ShowStaus)))
                {
                    ShowStatusFilters.Add(status.ToString()); // Thêm tên enum dưới dạng chuỗi
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Lọc dữ liệu khi có thay đổi
            FilterShows();
        }

        private void FilterShows()
        {
            // Lấy các giá trị đã chọn từ ComboBox
            var selectedRoomId = RoomComboBox.SelectedValue?.ToString();
            var selectedFilmId = FilmComboBox.SelectedValue?.ToString();
            var selectedStatus = ShowStatusComboBox.SelectedItem?.ToString();

            using (var context = new CinemaContext())
            {
                // Bắt đầu với truy vấn ban đầu
                IQueryable<Show> query = context.Shows;

                // Thêm điều kiện lọc dựa trên lựa chọn của người dùng
                if (selectedRoomId.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.RoomId == selectedRoomId);
                }
                if (selectedFilmId.IsNotNullOrEmpty())
                {
                    query = query.Where(x => x.FilmId == selectedFilmId);
                }
                if (selectedStatus.IsNotNullOrEmpty())
                {
                   query = query.Where(x => x.Status == selectedStatus.ToString());
                }

                // Lấy danh sách kết quả
                var list = query.ToList();

                // Chuyển đổi danh sách kết quả thành DTO
                ShowsList = App.Mapper.Map<List<ShowDTO>>(list);

                // Cập nhật ItemsSource cho DataGrid
                ShowsDataGrid.ItemsSource = ShowsList;
            }
        }

        private void LoadData()
        {
            // Giả sử bạn có một DbContext được gọi là `CinemaContext`
            using (var context = new CinemaContext())
            {
                var list = context.Shows.ToList();
                ShowsList = App.Mapper.Map<List<ShowDTO>>(list);
                ShowsDataGrid.ItemsSource = ShowsList;
            }
        }

        private void FilterChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        // Kiểm tra chỉ nhập số cho PriceTextBox
        private void PriceTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, @"^[0-9]*(?:\.[0-9]*)?$");
        }

        private async void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy RoomID
            var roomIdItem = RoomIDComboBox.SelectedItem as ComboBoxItem;
            var roomId = roomIdItem?.Tag?.ToString();

            // Kiểm tra nếu RoomIDComboBox không có giá trị được chọn
            if (string.IsNullOrEmpty(roomId))
            {
                MessageBox.Show("Vui lòng chọn RoomID.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Lấy FilmID
            var filmIdItem = FilmIDComboBox.SelectedItem as ComboBoxItem;
            var filmId = filmIdItem?.Tag?.ToString();

            // Kiểm tra nếu FilmIDComboBox không có giá trị được chọn
            if (string.IsNullOrEmpty(filmId))
            {
                MessageBox.Show("Vui lòng chọn FilmID.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Lấy Price và kiểm tra nếu Price < 0
            if (!decimal.TryParse(PriceTextBox.Text, out var price) || price < 0)
            {
                MessageBox.Show("Vui lòng nhập giá trị hợp lệ cho Price (>= 0).", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Lấy Status
            var statusItem = StatusComboBoxForAdd.SelectedItem as ComboBoxItem;
            var status = statusItem?.Content?.ToString();

            // Kiểm tra nếu StatusComboBox không có giá trị được chọn
            if (string.IsNullOrEmpty(status))
            {
                MessageBox.Show("Vui lòng chọn Status.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Tạo đối tượng ShowDTO từ các giá trị đã nhập
            var showDTO = new ShowDTO
            {
                ShowId = StringExtentions.GetIdByGuid(),
                RoomId = roomId,
                FilmId = filmId,
                Price = price,
                Status = status
            };

            using (var context = new CinemaContext())
            {
                // Thêm showDTO vào database qua DbContext
                context.Shows.Add(new Show
                {
                    ShowId = showDTO.ShowId,
                    RoomId = showDTO.RoomId,
                    FilmId = showDTO.FilmId,
                    Price = showDTO.Price ?? 0,
                    Status = showDTO.Status ?? "Còn vé"
                });

                await context.SaveChangesAsync();
            }

            MessageBox.Show("Đã thêm hoặc cập nhật thành công lịch chiếu!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            RoomComboBox.SelectedItem = null;
            FilmComboBox.SelectedItem = null;
            ShowStatusComboBox.SelectedItem = null;
            LoadData();
            LoadComboBoxes();
        }

        private void RoomIDTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
