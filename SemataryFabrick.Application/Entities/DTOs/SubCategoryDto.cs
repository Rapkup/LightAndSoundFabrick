namespace SemataryFabrick.Application.Entities.DTOs;
public record SubCategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid ParentCategoryId { get; set; }
}
