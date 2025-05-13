using SemataryFabrick.Domain.Entities.Models.ProduIctItemModels;

namespace SemataryFabrick.Domain.Entities.Models;
public class ProductCategory
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public IEnumerable<SubCategory> SubCategories { get; set; }
}
