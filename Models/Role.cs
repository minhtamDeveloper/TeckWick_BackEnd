using System;
using System.Collections.Generic;

namespace PlanetNest.Models;

public partial class Role
{
    public int Id { get; set; }

    public string? RoleName { get; set; }

    public string? Description { get; set; }

    public DateTime? Created { get; set; }

    public virtual ICollection<Account> Accounts { get; set; } = new List<Account>();
}
