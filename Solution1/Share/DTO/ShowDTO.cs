using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DTO
{
    public class ShowDTO
    {
        public string ShowId { get; set; } = null!;

        public string? RoomId { get; set; }

        public string? FilmId { get; set; }

        public decimal? Price { get; set; }

        public string? Status { get; set; }
    }
}
