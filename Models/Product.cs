using System;
using System.Collections.Generic;

namespace PlanetNest.Models;

public partial class Product
{
    public int Id { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public decimal? CostPrice { get; set; }

    public decimal? SellPrice { get; set; }

    public int? Quantity { get; set; }

    public DateTime? CreatedDate { get; set; }

    public int? SupplierId { get; set; }

    public int? CategoryId { get; set; }

    public bool? Status { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Supplier? Supplier { get; set; }
}
