using System;
using System.Collections.Generic;

namespace GenDb.Models;

public partial class Booking
{
    public string BookingId { get; set; } = null!;

    public string? ShowId { get; set; }

    public string? SeatNumber { get; set; }

    public virtual Show? Show { get; set; }
}
