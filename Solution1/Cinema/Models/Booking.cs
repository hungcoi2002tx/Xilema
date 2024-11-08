using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class Booking
{
    public string BookingId { get; set; } = null!;

    public string? ShowId { get; set; }

    public string? SeatNumber { get; set; }

    public string? UserId { get; set; }

    public virtual Show? Show { get; set; }
}
