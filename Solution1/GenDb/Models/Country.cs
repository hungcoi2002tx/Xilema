using System;
using System.Collections.Generic;

namespace GenDb.Models;

public partial class Country
{
    public string CountryCode { get; set; } = null!;

    public string CountryName { get; set; } = null!;

    public virtual ICollection<Film> Films { get; set; } = new List<Film>();
}
