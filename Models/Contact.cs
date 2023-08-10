using System;
using System.Collections.Generic;

namespace PlanetNest.Models;

public partial class Contact
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Email { get; set; }

    public string? Subject { get; set; }

    public string? Message { get; set; }

    public DateTime? Created { get; set; }
}
