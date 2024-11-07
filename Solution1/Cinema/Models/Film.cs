using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class Film
{
    public string FilmId { get; set; } = null!;

    public string? GenreId { get; set; }

    public string Title { get; set; } = null!;

    public int? Year { get; set; }

    public string? CountryCode { get; set; }

    public virtual Country? CountryCodeNavigation { get; set; }

    public virtual Genre? Genre { get; set; }

    public virtual ICollection<Show> Shows { get; set; } = new List<Show>();
}
