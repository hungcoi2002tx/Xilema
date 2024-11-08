using Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Models
{
    public class BookingViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Film> FilmList { get; set; }
        public ObservableCollection<TimeStart> TimeList { get; set; }
        public ObservableCollection<Room> RoomList { get; set; }
        public ObservableCollection<Show> ShowList { get; set; }
        public ObservableCollection<Booking> BookingList { get; set; }
        public ObservableCollection<string> SeatList { get; set; }

        private Film _selectedFilm;
        private TimeStart _selectedTime;
        private Room _selectedRoom;
        private Show _selectedShow;
        private Booking _selectedBooking;
        private string _selectedSeat;
        public TimeStart SelectedTime
        {
            get => _selectedTime;
            set
            {
                _selectedTime = value;
                OnPropertyChanged(nameof(SelectedTime));
            }
        }
        public Film SelectedFilm
        {
            get => _selectedFilm;
            set
            {
                _selectedFilm = value;
                OnPropertyChanged(nameof(SelectedFilm));
            }
        }
        public Room SelectedRoom
        {
            get => _selectedRoom;
            set
            {
                _selectedRoom = value;
                OnPropertyChanged(nameof(SelectedRoom));
            }
        }
        public Show SelectedShow
        {
            get => _selectedShow;
            set
            {
                _selectedShow = value;
                OnPropertyChanged(nameof(SelectedShow));
            }
        }
        public Booking SelectedBooking
        {
            get => _selectedBooking;
            set
            {
                _selectedBooking = value;
                OnPropertyChanged(nameof(SelectedBooking));
            }
        }
        public string SelectedSeat
        {
            get => _selectedSeat;
            set
            {
                _selectedSeat = value;
                OnPropertyChanged(nameof(SelectedSeat));
            }
        }

        // Khởi tạo ViewModel với các giá trị ban đầu
        public BookingViewModel()
        {
            TimeList = new ObservableCollection<TimeStart>(Enum.GetValues(typeof(TimeStart)).Cast<TimeStart>());
            RoomList = new ObservableCollection<Room>();
            ShowList = new ObservableCollection<Show>();
            BookingList = new ObservableCollection<Booking>();
            SeatList = new ObservableCollection<string>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
