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
    /// Interaction logic for RoomManagement.xaml
    /// </summary>
    public partial class RoomManagement : Window
    {
        public RoomManagement()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                using (var _context = new CinemaContext())
                {
                    RoomList.ItemsSource = _context.Rooms.ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private Room GetInfor()
        {
            return new Room
            {
                RoomId = txtRoomId.Text,
                Name = txtName.Text,
                NumberRows = Int16.Parse(txtNumberRows.Text),
                NumberCols = Int16.Parse(txtNumberCols.Text),
            };
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room p = GetInfor();
                if (p != null)
                {
                    p.RoomId = Guid.NewGuid().ToString("N");
                    using (var _context = new CinemaContext())
                    {
                        _context.Add(p);
                        _context.SaveChanges();
                        LoadData();
                    }
                    MessageBox.Show("Add room success!", "Add Room");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add Room");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room room = GetInfor();
                if (room != null)
                {
                    using (var _context = new CinemaContext())
                    {
                        Room oldInfor = _context.Rooms.FirstOrDefault(p => p.RoomId == room.RoomId);
                        if (oldInfor != null)
                        {
                            oldInfor.Name = room.Name;
                            oldInfor.NumberRows = room.NumberRows;
                            oldInfor.NumberCols = room.NumberCols;
                            _context.Rooms.Update(oldInfor);
                            _context.SaveChanges();
                            LoadData();
                            MessageBox.Show($"Update room successful", "Update Room");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Room");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Room p = null;
                using (var _context = new CinemaContext())
                {
                    p = _context.Rooms.FirstOrDefault(p => p.RoomId == txtRoomId.Text);
                    if (p != null)
                    {
                        _context.Rooms.Remove(p);
                        _context.SaveChanges();
                        LoadData();
                        MessageBox.Show($"Delete room successful", "Delete Room");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete Room");
            }
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadData();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
