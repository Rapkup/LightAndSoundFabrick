namespace SemataryFabrick.Application.Entities.DTOs.CartDtos;
public record CartDto
{
    public Guid Id { get; set; }
    public decimal TotalPrice { get; set; }
    public Guid CustomerId { get; set; }
}
