using SemataryFabrick.Domain.Entities.Models.ProduIctItemModels;

namespace SemataryFabrick.Application.Entities.DTOs;
public record SubCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ParentCategoryId { get; set; }

    public SubCategory ToEntity()
    {
        return new SubCategory
        {
            Id = this.Id,
            Name = this.Name,
            ParentCategoryId = this.ParentCategoryId,   
        };
    }

    public static SubCategoryDto FromEntity(SubCategory entity)
    {
        return new SubCategoryDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ParentCategoryId = entity.ParentCategoryId
        };
    }
}
