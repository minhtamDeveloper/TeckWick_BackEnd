using System;
using System.Collections.Generic;

namespace PlanetNest.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int? Quantity { get; set; }

    public int? CommentId { get; set; }

    public decimal? TotalPrice { get; set; }

    public DateTime? Created { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
