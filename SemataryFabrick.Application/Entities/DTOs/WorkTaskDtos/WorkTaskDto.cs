using SemataryFabrick.Domain.Entities.Enums;

namespace SemataryFabrick.Application.Entities.DTOs.WorkTaskDtos;
public record WorkTaskDto
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public WorkTaskState WorkTaskState { get; set; }
}