using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DTO
{
    public class BookingDTO
    {
        public string BookingId { get; set; } = null!;

        public string? ShowId { get; set; }

        public string? SeatNumber { get; set; }

        public string? UserId { get; set; }

        public virtual ShowDTO? Show { get; set; }
        public virtual UserDTO? User { get; set; }

        private string _roomName;

        public string RoomName
        {
            get { return Show.RoomName; }
            set { _roomName = value; }
        }

        private string _filmTitle;

        public string FilmTitle
        {
            get { return Show.FilmTitle; }
            set { _filmTitle = value; }
        }

        private string _userName;

        public string? UserName
        {
            get { return User?.Name; }
            set { _roomName = value; }
        }
    }
}
