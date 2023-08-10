using System;
using System.Collections.Generic;

namespace PlanetNest.Models;

public partial class Supplier
{
    public int Id { get; set; }

    public string? SupplierName { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
