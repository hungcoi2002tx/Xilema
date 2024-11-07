using System;
using System.Collections.Generic;

namespace GenDb.Models;

public partial class Genre
{
    public string GenreId { get; set; } = null!;

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
