using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Application.Entities.DTOs;
public record ProductCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

    public ProductCategory ToEntity()
    {
        return new ProductCategory
        {
            Id = this.Id,
            Name = this.Name,
            Description = this.Description
        };
    }

    public static ProductCategoryDto FromEntity(ProductCategory entity)
    {
        return new ProductCategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description
        };
    }
}