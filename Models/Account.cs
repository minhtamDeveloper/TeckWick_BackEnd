using System;
using System.Collections.Generic;

namespace PlantNestBackEnd.Models;

public partial class Account
{
    public int Id { get; set; }

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string? AccountImage { get; set; }

    public int? RoleId { get; set; }

    public DateTime? Created { get; set; }

    public DateTime? Dob { get; set; }

    public bool? Status { get; set; }

    public string? Fullname { get; set; }

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<FavoriteCart> FavoriteCarts { get; set; } = new List<FavoriteCart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
