using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Domain.Entities.Models;
public class WorkTaskAssignment
{
    public Guid Id { get; set; }
    public bool IsCompleted { get; set; }
    public Guid WorkTaskId { get; set; }
    public Guid OrderCrewId { get; set; }

    public WorkTask WorkTask { get; set; }
    public OrderCrew OrderCrew { get; set; }
}
