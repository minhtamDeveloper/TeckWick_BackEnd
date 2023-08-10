using System;
using System.Collections.Generic;

namespace PlanetNest.Models;

public partial class FavoriteCart
{
    public int Id { get; set; }

    public int? AccountId { get; set; }

    public int? ProductId { get; set; }

    public virtual Account? Account { get; set; }
}
