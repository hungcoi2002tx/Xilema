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
    /// Interaction logic for GenreManagement.xaml
    /// </summary>
    public partial class GenreManagement : Window
    {
        public GenreManagement()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            using (var _context = new CinemaContext())
            {
                GenreList.ItemsSource = _context.Genres.ToList();
            }
        }

        private Genre GetInfor()
        {
            return new Genre
            {
                GenreId = txtGenreId.Text,
                GenreName = txtGenreName.Text,
            };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Genre p = GetInfor();
                if (p != null)
                {
                    p.GenreId = Guid.NewGuid().ToString("N");
                    using (var _context = new CinemaContext())
                    {
                        _context.Add(p);
                        _context.SaveChanges();
                        LoadData();
                    }
                    MessageBox.Show("Add genre success!", "Add Genre");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Genre");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Genre genre = GetInfor();
                if (genre != null)
                {
                    using (var _context = new CinemaContext())
                    {
                        Genre oldInfor = _context.Genres.FirstOrDefault(p => p.GenreId == genre.GenreId);
                        if (oldInfor != null)
                        {
                            oldInfor.GenreName = genre.GenreName;
                            _context.Genres.Update(oldInfor);
                            _context.SaveChanges();
                            LoadData();
                            MessageBox.Show($"Update genre successful", "Update Genre");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Genre");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Genre p = null;
                using (var _context = new CinemaContext())
                {
                    p = _context.Genres.FirstOrDefault(p => p.GenreId == txtGenreId.Text);
                    if (p != null)
                    {
                        _context.Genres.Remove(p);
                        _context.SaveChanges();
                        LoadData();
                        MessageBox.Show($"Delete genre successful", "Delete Genre");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Genre");
            }
        }
    }
}
