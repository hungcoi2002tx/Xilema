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
            try
            {
                using (var _context = new CinemaContext())
                {
                    cboGenre.ItemsSource = _context.Genres.ToList();
                    cboCountry.ItemsSource = _context.Countries.ToList();
                    FilmList.ItemsSource = _context.Films.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Film GetInfor()
        {
            return new Film
            {
                FilmId = txtFilmId.Text,
                Title = txtTitle.Text,
                Year = Int16.Parse(txtYear.Text),
                GenreId = cboGenre.SelectedValue.ToString(),
                CountryCode = cboCountry.SelectedValue.ToString(),
            };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Film p = GetInfor();
                if (p != null)
                {
                    p.FilmId = Guid.NewGuid().ToString("N");
                    using (var _context = new CinemaContext())
                    {
                        _context.Add(p);
                        _context.SaveChanges();
                        LoadData();
                    }
                    MessageBox.Show("Add film success!", "Add Film");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Film");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Film film = GetInfor();
                if (film != null)
                {
                    using (var _context = new CinemaContext())
                    {
                        Film oldInfor = _context.Films.FirstOrDefault(p => p.FilmId == film.FilmId);
                        if (oldInfor != null)
                        {
                            oldInfor.Title = film.Title;
                            oldInfor.Year = film.Year;
                            oldInfor.CountryCode = film.CountryCode;
                            oldInfor.GenreId = film.GenreId;
                            _context.Films.Update(oldInfor);
                            _context.SaveChanges();
                            LoadData();
                            MessageBox.Show($"Update film successful", "Update Film");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Film");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Film p = null;
                using (var _context = new CinemaContext())
                {
                    p = _context.Films.FirstOrDefault(p => p.FilmId == txtFilmId.Text);
                    if (p != null)
                    {
                        _context.Films.Remove(p);
                        _context.SaveChanges();
                        LoadData();
                        MessageBox.Show($"Delete film successful", "Delete Film");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Film");
            }
        }
    }
}
