using System;
using System.Collections.Generic;

namespace PlantNestBackEnd.Models;

public partial class Delivery
{
    public int Id { get; set; }

    public int? OrderId { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTime? ReceivingDate { get; set; }

    public string? RecipientName { get; set; }

    public string? RecipientAddress { get; set; }

    public string? RecipientPhone { get; set; }

    public string? Message { get; set; }

    public string? Status { get; set; }

    public virtual Order? Order { get; set; }
}
