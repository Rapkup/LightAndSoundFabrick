using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.Items;

namespace SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;
public record ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ImageName { get; set; }
    public string Description { get; set; } // JSON
    public decimal Price { get; set; }
    public ProductState Status { get; set; }
    public Guid InventoryId { get; set; }
    public Guid SubCategoryId { get; set; }


    public static ItemDto FromEntity(Item entity)
    {
        return new ItemDto
        {
            Id = entity.Id,
            Name = entity.Name,
            ImageName = entity.ImageName,
            Description = entity.Description,
            Price = entity.Price,
            Status = entity.Status,
            InventoryId = entity.InventoryId,
            SubCategoryId = entity.SubCategoryId
        };
    }

    public Item ToEntity()
    {
        return new Item
        {
            Id = this.Id,
            Name = this.Name,
            ImageName = this.ImageName,
            Description = this.Description,
            Price = this.Price,
            Status = this.Status,
            InventoryId = this.InventoryId,
            SubCategoryId = this.SubCategoryId,
        };
    }
}