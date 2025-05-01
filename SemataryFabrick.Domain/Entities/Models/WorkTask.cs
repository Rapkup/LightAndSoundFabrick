using SemataryFabrick.Domain.Entities.Enums;

namespace SemataryFabrick.Domain.Entities.Models;
public class WorkTask
{
    public Guid Id { get; set; }
    public string Description { get; set; }
    public WorkTaskState WorkTaskState { get; set; }
    public IEnumerable<WorkTaskAssignment> WorkTaskAssignments { get; set; }
}