using System;
using System.Collections.Generic;

namespace PlantNestBackEnd.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual Account? Account { get; set; }
}
