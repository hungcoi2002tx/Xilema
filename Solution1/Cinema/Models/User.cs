using System;
using System.Collections.Generic;

namespace Cinema.Models;

public partial class User
{
    public string Id { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Account { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;
}
