using System;
using System.Collections.Generic;

namespace PlanetNest.Models;

public partial class Comment
{
    public int Id { get; set; }

    public string? Content { get; set; }

    public int? Rating { get; set; }

    public DateTime? Created { get; set; }

    public int? AccountId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }
}
