using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Share.DTO
{
    public class FilmDTO
    {
        public string FilmId { get; set; } = null!;

        public string? GenreId { get; set; }

        public string Title { get; set; } = null!;

        public int? Year { get; set; }

        public string? CountryCode { get; set; }
    }
}
