using Cinema.Models;
using Share;
using Share.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xml.Linq;

namespace Cinema
{
    /// <summary>
    /// Interaction logic for BookingWindow.xaml
    /// </summary>
    public partial class BookingWindow : Window
    {
        public Show Show {  get; set; } = new Show();
        public BookingWindow()
        {
            InitializeComponent();
            DataContext = new BookingViewModel();
            LoadFilm();
        }
        public void LoadFilm()
        {
            try
            {
                using (var _context = new CinemaContext())
                {
                    cboFilm.ItemsSource = _context.Films.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Booking GetInfor()
        {
            return new Booking
            {
                ShowId = Show.ShowId,
                UserId = Session.UserId.Replace("-", ""),
                SeatNumber = cboSeatNumber.SelectedValue.ToString(),
            };
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Booking p = GetInfor();
                if (p != null)
                {
                    p.BookingId = Guid.NewGuid().ToString("N");
                    using (var _context = new CinemaContext())
                    {
                        _context.Add(p);
                        _context.SaveChanges();
                    }
                    MessageBox.Show("Booking success!", "Booking");
                    LoadSeat();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Booking");
            }
        }

        private void cboTime_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var viewModel = (BookingViewModel)DataContext;
                using (var _context = new CinemaContext())
                {
                    Film x = (Film)cboFilm.SelectedValue;
                    var y = cboTime.SelectedValue.ToString();
                    viewModel.ShowList = new ObservableCollection<Show>(_context.Shows.Where(c => c.FilmId == x.FilmId && c.Time == cboTime.SelectedValue.ToString() && c.Status == ShowStaus.Available.ToString()).ToList());
                    var roomIds = viewModel.ShowList.Select(c => c.RoomId).Distinct().ToList();
                    viewModel.RoomList = new ObservableCollection<Room>(_context.Rooms.Where(c => roomIds.Contains(c.RoomId)).ToList());
                    cboRoom.ItemsSource = viewModel.RoomList;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void cboRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                LoadSeat();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LoadSeat()
        {
            try
            {
                var viewModel = (BookingViewModel)DataContext;
                Room x = (Room)cboRoom.SelectedValue;
                Film y = (Film)cboFilm.SelectedValue;
                var room = viewModel.RoomList.FirstOrDefault(c => c.RoomId == x.RoomId);
                if (room != null)
                {
                    var totalSeat = room.NumberRows * room.NumberCols;
                    for (int i = 1; i <= totalSeat; i++)
                    {
                        viewModel.SeatList.Add(i.ToString());
                    }
                    using (var _context = new CinemaContext())
                    {
                        Show = viewModel.ShowList.First(c => c.RoomId == room.RoomId && c.FilmId == y.FilmId && c.Time == cboTime.SelectedValue.ToString());
                        viewModel.BookingList = new ObservableCollection<Booking>(_context.Bookings.Where(c => c.ShowId == Show.ShowId).ToList());
                        var seatFull = viewModel.BookingList.Select(c => c.SeatNumber).ToList();
                        var seatsToRemove = new List<string>(); // Danh sách tạm thời lưu các phần tử cần xóa

                        foreach (var seat in viewModel.SeatList)
                        {
                            if (seatFull.Contains(seat))
                            {
                                seatsToRemove.Add(seat);
                            }
                        }

                        // Sau khi duyệt qua tất cả phần tử, xoá các phần tử trong danh sách tạm thời
                        foreach (var seat in seatsToRemove)
                        {
                            viewModel.SeatList.Remove(seat);
                        }

                        cboSeatNumber.ItemsSource = viewModel.SeatList;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
