using System;
using System.Collections.Generic;

namespace GenDb.Models;

public partial class Show
{
    public string ShowId { get; set; } = null!;

    public string? RoomId { get; set; }

    public string? FilmId { get; set; }

    public decimal? Price { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Film? Film { get; set; }

    public virtual Room? Room { get; set; }
}
