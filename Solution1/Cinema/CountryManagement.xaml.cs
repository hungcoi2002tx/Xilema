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
    /// Interaction logic for CountryManagement.xaml
    /// </summary>
    public partial class CountryManagement : Window
    {
        public CountryManagement()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            using (var _context = new CinemaContext())
            {
                CountryList.ItemsSource = _context.Countries.ToList();
            }
        }

        private Country GetInfor()
        {
            return new Country
            {
                CountryCode = txtCountryCode.Text,
                CountryName = txtCountryName.Text,
            };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country p = GetInfor();
                if (p != null)
                {
                    p.CountryCode = Guid.NewGuid().ToString("N");
                    using (var _context = new CinemaContext())
                    {
                        _context.Add(p);
                        _context.SaveChanges();
                        LoadData();
                    }
                    MessageBox.Show("Add country success!", "Add Country");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Country");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country country = GetInfor();
                if (country != null)
                {
                    using (var _context = new CinemaContext())
                    {
                        Country oldInfor = _context.Countries.FirstOrDefault(p => p.CountryCode == country.CountryCode);
                        if (oldInfor != null)
                        {
                            oldInfor.CountryName = country.CountryName;
                            _context.Countries.Update(oldInfor);
                            _context.SaveChanges();
                            LoadData();
                            MessageBox.Show($"Update country successful", "Update Country");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Country");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Country p = null;
                using (var _context = new CinemaContext())
                {
                    p = _context.Countries.FirstOrDefault(p => p.CountryCode == txtCountryCode.Text);
                    if (p != null)
                    {
                        _context.Countries.Remove(p);
                        _context.SaveChanges();
                        LoadData();
                        MessageBox.Show($"Delete country successful", "Delete Country");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Country");
            }
        }
    }
}
