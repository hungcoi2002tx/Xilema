using Cinema.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
        }

        public void LoadGenreData()
        {
            try
            {
                using (var _context = new CinemaContext())
                {
                    GenreList.ItemsSource = _context.Genres.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void LoadCountryData()
        {
            try
            {
                using (var _context = new CinemaContext())
                {
                    CountryList.ItemsSource = _context.Countries.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Genre GetGenreInfor()
        {
            return new Genre
            {
                GenreId = txtGenreId.Text,
                GenreName = txtGenreName.Text,
            };
        }

        private Country GetCountryInfor()
        {
            return new Country
            {
                CountryCode = txtCountryCode.Text,
                CountryName = txtCountryName.Text,
            };
        }

        private void btnAddGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Genre p = GetGenreInfor();
                if (p != null)
                {
                    p.GenreId = Guid.NewGuid().ToString("N");
                    using (var _context = new CinemaContext())
                    {
                        _context.Add(p);
                        _context.SaveChanges();
                        LoadGenreData();
                    }
                    MessageBox.Show("Add genre success!", "Add Genre");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Genre");
            }
        }

        private void btnUpdateGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Genre genre = GetGenreInfor();
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
                            LoadGenreData();
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

        private void btnDeleteGenre_Click(object sender, RoutedEventArgs e)
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
                        LoadGenreData();
                        MessageBox.Show($"Delete genre successful", "Delete Genre");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Genre");
            }
        }

        private void btnAddCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country p = GetCountryInfor();
                if (p != null)
                {
                    if (p.CountryCode.IsNullOrEmpty())
                    {
                        MessageBox.Show($"CountryCode is not null", "Add Country");
                    }
                    else
                    {
                        using (var _context = new CinemaContext())
                        {
                            Country countryExist = _context.Countries.FirstOrDefault(c => c.CountryCode == p.CountryCode);
                            if (countryExist != null)
                            {
                                MessageBox.Show("CountryCode is already exist!", "Add Country");
                            }
                            else
                            {
                                _context.Add(p);
                                _context.SaveChanges();
                                LoadCountryData();
                                MessageBox.Show("Add country success!", "Add Country");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Country");
            }
        }

        private void btnUpdateCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country country = GetCountryInfor();
                if (country != null)
                {
                    if (country.CountryCode.IsNullOrEmpty())
                    {
                        MessageBox.Show($"CountryCode is not null", "Update Country");
                    }
                    else
                    {
                        using (var _context = new CinemaContext())
                        {
                            Country oldInfor = _context.Countries.FirstOrDefault(p => p.CountryCode == country.CountryCode);
                            if (oldInfor != null)
                            {
                                oldInfor.CountryName = country.CountryName;
                                _context.Countries.Update(oldInfor);
                                _context.SaveChanges();
                                LoadCountryData();
                                MessageBox.Show($"Update country successful", "Update Country");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Country");
            }
        }

        private void btnDeleteCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country p = null;
                if (txtCountryCode.Text.IsNullOrEmpty())
                {
                    MessageBox.Show($"CountryCode is not null", "Delete Country");
                }
                else
                {
                    using (var _context = new CinemaContext())
                    {
                        p = _context.Countries.FirstOrDefault(p => p.CountryCode == txtCountryCode.Text);
                        if (p != null)
                        {
                            _context.Countries.Remove(p);
                            _context.SaveChanges();
                            LoadCountryData();
                            MessageBox.Show($"Delete country successful", "Delete Country");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Country");
            }
        }

        private void btnLoadGenre_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadGenreData();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnLoadCountry_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadCountryData();
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
