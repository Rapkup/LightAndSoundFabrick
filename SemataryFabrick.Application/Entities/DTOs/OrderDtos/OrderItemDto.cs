namespace SemataryFabrick.Application.Entities.DTOs.OrderDtos;
public record OrderItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid OrderBaseId { get; set; }
    public Guid ProductId { get; set; }
}
