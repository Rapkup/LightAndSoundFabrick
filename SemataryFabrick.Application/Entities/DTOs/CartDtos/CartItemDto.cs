using SemataryFabrick.Domain.Entities.Models.CartModels;

namespace SemataryFabrick.Application.Entities.DTOs.CartDtos;
public record CartItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public Guid? DiscountId { get; set; }

    public static CartItemDto FromEntity(CartItem entity) => new()
    {
        Id = entity.Id,
        Quantity = entity.Quantity,
        CartId = entity.CartId,
        ProductId = entity.ProductId,
        DiscountId = entity.DiscountId
    };

    public CartItem ToEntity() => new()
    {
        Id = Id,
        Quantity = Quantity,
        CartId = CartId,
        ProductId = ProductId,
        DiscountId = DiscountId
    };
}