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

namespace Cinema
{
    /// <summary>
    /// Interaction logic for FilmManagement.xaml
    /// </summary>
    public partial class FilmManagement : Window
    {
        public FilmManagement()
        {
            InitializeComponent();
            LoadData();
        }
        public void LoadData()
        {
            using (var _context = new CinemaContext())
            {
                cboGenre.ItemsSource = _context.Genres.ToList();
                cboCountry.ItemsSource = _context.Countries.ToList();
                FilmList.ItemsSource = _context.Films.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
