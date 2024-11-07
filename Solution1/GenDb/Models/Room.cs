using System;
using System.Collections.Generic;

namespace GenDb.Models;

public partial class Room
{
    public string RoomId { get; set; } = null!;

    public int NumberRows { get; set; }

    public int NumberCols { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Show> Shows { get; set; } = new List<Show>();
}
