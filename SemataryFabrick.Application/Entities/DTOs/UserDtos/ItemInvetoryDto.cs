namespace SemataryFabrick.Application.Entities.DTOs.UserDtos;
public record ItemInvetoryDto
{
    public Guid Id { get; set; }
    public int Quantity { get; set; }
}