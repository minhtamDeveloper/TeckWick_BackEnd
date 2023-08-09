using System;
using System.Collections.Generic;

namespace PlantNestBackEnd.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string? PaymentMethod { get; set; }

    public decimal? TotalOrder { get; set; }

    public DateTime? OrderDate { get; set; }

    public TimeSpan? OrderTime { get; set; }

    public string? Status { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
