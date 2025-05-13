using SemataryFabrick.Application.Entities.DTOs.ProductItemDtos;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Entities.DTOs.OrderDtos;
public record OrderItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid OrderBaseId { get; set; }
    public Guid ProductId { get; set; }
    public ItemDto Product { get; set; }
    public Guid? DiscountId { get; set; }

    public static OrderItemDto FromEntity(OrderItem entity)
    {
        return new OrderItemDto
        {
            Id = entity.Id,
            Quantity = entity.Quantity,
            OrderBaseId = entity.OrderBaseId,
            ProductId = entity.ProductId,
            Product = entity.Product != null ? ItemDto.FromEntity(entity.Product) : null,
            DiscountId = entity.DiscountId
        };
    }

    public OrderItem ToEntity()
    {
        return new OrderItem
        {
            Id = this.Id,
            Quantity = this.Quantity,
            OrderBaseId = this.OrderBaseId,
            ProductId = this.ProductId,
            DiscountId = this.DiscountId
        };
    }
}