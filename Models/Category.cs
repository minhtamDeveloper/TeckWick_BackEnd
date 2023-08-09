using System;
using System.Collections.Generic;

namespace PlantNestBackEnd.Models;

public partial class Category
{
    public int Id { get; set; }

    public int? CategoryId { get; set; }

    public string? CategoryImage { get; set; }

    public string? CategoryName { get; set; }

    public DateTime? Created { get; set; }

    public bool? Status { get; set; }

    public virtual Category? CategoryNavigation { get; set; }

    public virtual ICollection<Category> InverseCategoryNavigation { get; set; } = new List<Category>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
