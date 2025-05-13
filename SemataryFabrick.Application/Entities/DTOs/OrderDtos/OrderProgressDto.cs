namespace SemataryFabrick.Application.Entities.DTOs.OrderDtos;
public record OrderProgressDto
{
    public int TotalTasks { get; init; }
    public int CompletedTasks { get; init; }
    public double CompletedPercentage =>
        TotalTasks == 0 ? 0 : (CompletedTasks * 100.0) / TotalTasks;
}
