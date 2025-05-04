namespace SemataryFabrick.Application.Entities.DTOs.WorkTaskDtos;
public record WorkTaskAssignmentDto
{
    public Guid Id { get; set; }
    public bool IsCompleted { get; set; }
    public Guid WorkTaskId { get; set; }
    public Guid OrderCrewId { get; set; }
}
