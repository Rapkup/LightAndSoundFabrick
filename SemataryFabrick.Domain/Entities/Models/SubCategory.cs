using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Domain.Entities.Models;

public class SubCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ParentCategoryId {get; set; }

    public ProductCategory ParentCategory { get; set; }
    public IEnumerable<Item> Items { get; set; }
}