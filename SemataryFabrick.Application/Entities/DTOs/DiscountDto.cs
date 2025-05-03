namespace SemataryFabrick.Application.Entities.DTOs;
public record DiscountDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int DiscountPercent { get; set; }
}