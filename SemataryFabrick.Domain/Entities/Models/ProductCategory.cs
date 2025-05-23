﻿namespace SemataryFabrick.Domain.Entities.Models;
public class ProductCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    IEnumerable<SubCategory> SubCategories { get; set; }
}
