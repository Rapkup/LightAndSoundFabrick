namespace SemataryFabrick.Application.Entities.DTOs.OrderDtos;
public record OrderCrewDto
{
    public Guid Id { get; set; }
    public DateOnly WorkDate { get; set; }
    public Guid TechLeadId { get; set; }
    public Guid OrderBaseId { get; set; }
}
