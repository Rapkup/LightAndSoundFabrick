namespace SemataryFabrick.Application.Entities.DTOs;
public record ProductCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}