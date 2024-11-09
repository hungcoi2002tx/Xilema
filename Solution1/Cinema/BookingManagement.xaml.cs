using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Share.DTO;
using System;
using System.Collections;
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
    /// Interaction logic for BookingManagement.xaml
    /// </summary>
    public partial class BookingManagement : Window
    {
        List<BookingDTO> BookingLists = new List<BookingDTO>();
        public BookingManagement()
        {
            InitializeComponent();
            LoadData();
        }

        private void ShowsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void LoadData()
        {
            try
            {
                using (var context = new CinemaContext())
                {
                    var bookings = context.Bookings
                         .Include(b => b.Show)
                             .ThenInclude(s => s.Film)
                         .Include(b => b.Show)
                             .ThenInclude(s => s.Room)
                         .ToList();
                    BookingLists = App.Mapper.Map<List<BookingDTO>>(bookings);
                    var users = context.Users.ToList();
                    BookingLists = BookingLists.GroupJoin(users, candidate => candidate.UserId, destination => destination.Id, (candidate, destination) => new { candidate, destination })
                    .SelectMany(x => x.destination.DefaultIfEmpty(),
                    (candidate, destination) =>
                            {
                                candidate.candidate.User = App.Mapper.Map<UserDTO>(destination);
                                return candidate.candidate;
                            }
                            ).ToList();
                    ShowsDataGrid.ItemsSource = BookingLists;
                }
            }
            catch (Exception)
            {

                throw;
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
