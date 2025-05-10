using SemataryFabrick.Domain.Entities.Enums;

namespace SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;
public record ItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } // JSON
    public decimal Price { get; set; }
    public ProductState Status { get; set; }
    public Guid? DiscountId { get; set; }
    public Guid InventoryId { get; set; }
    public Guid SubCategoryId { get; set; }

}