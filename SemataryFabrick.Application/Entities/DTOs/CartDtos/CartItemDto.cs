namespace SemataryFabrick.Application.Entities.DTOs.CartDtos;
public record CartItemDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
}
