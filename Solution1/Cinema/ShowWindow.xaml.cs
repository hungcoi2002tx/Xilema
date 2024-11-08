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
using Microsoft.EntityFrameworkCore;
namespace Cinema
{
    /// <summary>
    /// Interaction logic for ShowWindow.xaml
    /// </summary>
    public partial class ShowWindow : Window
    {
        private List<ShowDTO> ShowsList;
        public ObservableCollection<Room> RoomFilters { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<Room> RoomModels { get; set; } = new ObservableCollection<Room>();
        public ObservableCollection<Film> FilmFilters { get; set; } = new ObservableCollection<Film>();
        public ObservableCollection<Film> FilmModels { get; set; } = new ObservableCollection<Film>();
        public ObservableCollection<string> ShowStatusFilters { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> ShowStatusModels { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> TimeModels { get; set; } = new ObservableCollection<string>();

        public ShowWindow()
        {
            InitializeComponent();
            LoadData();
            LoadComboBoxes();
            DataContext = this;
        }

        private void RoomComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void LoadComboBoxes()
        {
            using (var context = new CinemaContext())
            {// Tạo danh sách phòng
                RoomFilters = new ObservableCollection<Room>();
                RoomModels = new ObservableCollection<Room>();
                var rooms = context.Rooms.ToList();
                foreach (var room in rooms)
                {
                    RoomFilters.Add(room);
                    RoomModels.Add(room);
                }
                FilmFilters  = new ObservableCollection<Film>();
                FilmModels  = new ObservableCollection<Film>();
                // Tạo danh sách phim
                List<Film> films = context.Films.ToList();
                foreach (var film in films)
                {
                    FilmFilters.Add(film);
                    FilmModels.Add(film);
                }
                ShowStatusFilters = new();
                ShowStatusModels = new();

                foreach (Share.ShowStaus status in Enum.GetValues(typeof(Share.ShowStaus)))
                {
                    ShowStatusFilters.Add(status.ToString()); // Thêm tên enum dưới dạng chuỗi
                    ShowStatusModels.Add(status.ToString());
                }
                TimeModels = new();
                foreach (Share.TimeStart status in Enum.GetValues(typeof(Share.TimeStart)))
                {
                    TimeModels.Add(status.ToString()); // Thêm tên enum dưới dạng chuỗi
                }
            }
        }

        private void ShowsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn không
            if (ShowsDataGrid.SelectedItem is ShowDTO selectedShow) // Thay ShowDTO bằng kiểu dữ liệu của bạn
            {
                // Điền dữ liệu vào các điều khiển thông tin
                RoomIDComboBoxForAdd.SelectedValue = selectedShow.RoomId; // Gán RoomId vào ComboBox
                FilmIDComboBoxForAdd.SelectedValue = selectedShow.FilmId; // Gán FilmId vào ComboBox
                PriceTextBoxForAdd.Text = selectedShow.Price?.ToString("F2"); // Gán giá vào TextBox, định dạng số
                ShowStatusForAdd.SelectedItem = selectedShow.Status; // Gán Status vào ComboBox
                TimeComboBoxForAdd.SelectedItem = selectedShow.Time; // Gán Time vào ComboBox
            }
            else
            {
                // Nếu không có hàng nào được chọn, xóa các giá trị trong các điều khiển
                RoomIDComboBoxForAdd.SelectedValue = null;
                FilmIDComboBoxForAdd.SelectedValue = null;
                PriceTextBoxForAdd.Text = string.Empty;
                ShowStatusForAdd.SelectedItem = null;
                TimeComboBoxForAdd.SelectedItem = null;
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
                IQueryable<Show> query = context.Shows.Include(x => x.Room).Include(x => x.Film);

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
                var list = context.Shows.Include(x => x.Room).Include(x => x.Film).ToList();
                ShowsList = App.Mapper.Map<List<ShowDTO>>(list);
                ShowsDataGrid.ItemsSource = ShowsList;
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy nút đã được nhấn
            var button = sender as Button;
            if (button != null)
            {
                // Lấy hàng của nút
                var showToDelete = button.DataContext as ShowDTO; // Thay ShowDTO bằng kiểu dữ liệu của bạn
                if (showToDelete != null)
                {
                    if (!IsExistedBooking(showToDelete))
                    {
                        return;
                    }
                    using (var context = new CinemaContext())
                    {
                        var entity = context.Shows.Where(x => x.ShowId == showToDelete.ShowId).FirstOrDefault();
                        if (entity == null)
                        {
                            MessageBox.Show("Not found id");
                            return;
                        }
                        context.Shows.Remove(entity);
                        context.SaveChanges(); 
                    }
                }
            }
            CLearFilter();
        }

        private bool IsExistedBooking(ShowDTO showDTO)
        {
            try
            {
                using (var context = new CinemaContext())
                {
                    var model = context.Bookings.Where(x => x.ShowId == showDTO.ShowId).FirstOrDefault();
                    if (model != null)
                    {
                        MessageBox.Show("Show is existed a booking.");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra tính hợp lệ trước khi thêm
            if (!ValidateInput())
            {
                return; // Nếu không hợp lệ, thoát khỏi phương thức
            }
            // Lấy dữ liệu từ các điều khiển
            var newShow = BindInputDataToShowDTO();
            newShow.ShowId = StringExtentions.GetIdByGuid();

            var show = App.Mapper.Map<Show>(newShow);
            if (!ValidShowData(show))
            {
                return;
            }
            using (var context = new CinemaContext())
            {
                context.Shows.Add(show); // Thêm đối tượng Show vào DbSet
                context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
            }

            // Thêm bản ghi mới vào danh sách và cập nhật DataGrid
            // Giả sử bạn có một danh sách ShowsList để chứa các bản ghi
            CLearFilter();
        }

        // Phương thức để bind dữ liệu từ các điều khiển vào ShowDTO
        private ShowDTO BindInputDataToShowDTO()
        {
            return new ShowDTO
            {
                RoomId = RoomIDComboBoxForAdd.SelectedValue?.ToString(),
                FilmId = FilmIDComboBoxForAdd.SelectedValue?.ToString(),
                Price = decimal.TryParse(PriceTextBoxForAdd.Text, out var price) ? price : (decimal?)null,
                Status = ShowStatusForAdd.SelectedItem?.ToString(),
                Time = TimeComboBoxForAdd.SelectedItem?.ToString()
            };
        }

        // Phương thức kiểm tra tính hợp lệ
        private bool ValidateInput()
        {
            // Kiểm tra Room
            if (RoomIDComboBoxForAdd.SelectedValue == null)
            {
                MessageBox.Show("Please select a room.");
                return false;
            }

            // Kiểm tra Film
            if (FilmIDComboBoxForAdd.SelectedValue == null)
            {
                MessageBox.Show("Please select a film.");
                return false;
            }

            // Kiểm tra Price
            if (!decimal.TryParse(PriceTextBoxForAdd.Text, out _))
            {
                MessageBox.Show("Please enter a valid price.");
                return false;
            }

            // Kiểm tra Status
            if (ShowStatusForAdd.SelectedItem == null)
            {
                MessageBox.Show("Please select a status.");
                return false;
            }

            // Kiểm tra Time
            if (TimeComboBoxForAdd.SelectedItem == null)
            {
                MessageBox.Show("Please select a time.");
                return false;
            }

            return true; // Nếu tất cả hợp lệ
        }

        private bool ValidShowData(Show show)
        {
            try
            {
                using (var context = new CinemaContext())
                {
                    var model = context.Shows.Where(x => x.RoomId == show.RoomId)
                        .Where(x => x.Time == show.Time).FirstOrDefault();
                    if(model != null)
                    {
                        MessageBox.Show("Room is existed a film schedule.");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có hàng nào được chọn để cập nhật không
            if (ShowsDataGrid.SelectedItem is ShowDTO selectedShow)
            {
                if (!IsExistedBooking(selectedShow))
                {
                    return;
                }

                List<ShowDTO> cloneList = ShowsList.Where(x => x.ShowId != selectedShow.ShowId).ToList();  
                var validRoom = cloneList.Where(x => x.RoomId == RoomIDComboBoxForAdd.SelectedValue?.ToString())
                    .Where(x => x.Time == TimeComboBoxForAdd.SelectedItem?.ToString())
                    .ToList();
                if (validRoom?.Any() == true)
                {
                    MessageBox.Show("Room is existed a film schedule.");
                    return;
                }
                // Cập nhật dữ liệu từ các điều khiển
                selectedShow.RoomId = RoomIDComboBoxForAdd.SelectedValue?.ToString();
                selectedShow.FilmId = FilmIDComboBoxForAdd.SelectedValue?.ToString();
                selectedShow.Price = decimal.TryParse(PriceTextBoxForAdd.Text, out var price) ? price : (decimal?)null;
                selectedShow.Status = ShowStatusForAdd.SelectedItem?.ToString();
                selectedShow.Time = TimeComboBoxForAdd.SelectedItem?.ToString();
                using (var context = new CinemaContext())
                {
                    var show = App.Mapper.Map<Show>(selectedShow);
                    context.Shows.Update(show); // Thêm đối tượng Show vào DbSet
                    context.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu
                }
                CLearFilter();
            }
            else
            {
                MessageBox.Show("Please select a show to update.");
            }
        }

        private async void AddOrUpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //// Lấy RoomID
            //var roomIdItem = RoomIDComboBox.SelectedItem as ComboBoxItem;
            //var roomId = roomIdItem?.Tag?.ToString();

            //// Kiểm tra nếu RoomIDComboBox không có giá trị được chọn
            //if (string.IsNullOrEmpty(roomId))
            //{
            //    MessageBox.Show("Vui lòng chọn RoomID.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// Lấy FilmID
            //var filmIdItem = FilmIDComboBox.SelectedItem as ComboBoxItem;
            //var filmId = filmIdItem?.Tag?.ToString();

            //// Kiểm tra nếu FilmIDComboBox không có giá trị được chọn
            //if (string.IsNullOrEmpty(filmId))
            //{
            //    MessageBox.Show("Vui lòng chọn FilmID.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// Lấy Price và kiểm tra nếu Price < 0
            //if (!decimal.TryParse(PriceTextBox.Text, out var price) || price < 0)
            //{
            //    MessageBox.Show("Vui lòng nhập giá trị hợp lệ cho Price (>= 0).", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// Lấy Status
            //var statusItem = StatusComboBoxForAdd.SelectedItem as ComboBoxItem;
            //var status = statusItem?.Content?.ToString();

            //// Kiểm tra nếu StatusComboBox không có giá trị được chọn
            //if (string.IsNullOrEmpty(status))
            //{
            //    MessageBox.Show("Vui lòng chọn Status.", "Lỗi nhập liệu", MessageBoxButton.OK, MessageBoxImage.Error);
            //    return;
            //}

            //// Tạo đối tượng ShowDTO từ các giá trị đã nhập
            //var showDTO = new ShowDTO
            //{
            //    ShowId = StringExtentions.GetIdByGuid(),
            //    RoomId = roomId,
            //    FilmId = filmId,
            //    Price = price,
            //    Status = status
            //};

            //using (var context = new CinemaContext())
            //{
            //    // Thêm showDTO vào database qua DbContext
            //    context.Shows.Add(new Show
            //    {
            //        ShowId = showDTO.ShowId,
            //        RoomId = showDTO.RoomId,
            //        FilmId = showDTO.FilmId,
            //        Price = showDTO.Price ?? 0,
            //        Status = showDTO.Status ?? "Còn vé"
            //    });

            //    await context.SaveChangesAsync();
            //}

            //MessageBox.Show("Đã thêm hoặc cập nhật thành công lịch chiếu!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void ClearFilter_Click(object sender, RoutedEventArgs e)
        {
            CLearFilter();
        }

        private void CLearFilter()
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

        private void EditStatus_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // Lấy hàng của nút
                var showToDelete = button.DataContext as ShowDTO; // Thay ShowDTO bằng kiểu dữ liệu của bạn
                if (showToDelete != null)
                {
                    ToggleShowStatus(showToDelete);
                }
            }
            CLearFilter();
        }

        private void ToggleShowStatus(ShowDTO show)
        {
            using (var context = new CinemaContext())
            {
                var existingShow = context.Shows.SingleOrDefault(s => s.ShowId == show.ShowId);
                if (existingShow != null)
                {
                    if (existingShow.Status == Share.ShowStaus.Available.ToString())
                    {
                        existingShow.Status = Share.ShowStaus.SoldOut.ToString();
                    }
                    else
                    {
                        existingShow.Status = Share.ShowStaus.Available.ToString();
                    }
                    context.SaveChanges();
                }
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
