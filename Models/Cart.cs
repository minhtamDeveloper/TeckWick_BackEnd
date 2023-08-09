using System;
using System.Collections.Generic;

namespace PlantNestBackEnd.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public string? RegisId { get; set; }

    public int? Quantity { get; set; }

    public virtual Account? Account { get; set; }
}
