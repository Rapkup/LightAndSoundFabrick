using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Entities.Models.OrderModels;
public class OrderCrew
{
    public Guid Id { get; set; }
    public DateOnly WorkDate { get; set; }
    public Guid TechLeadId { get; set; }
    public Guid OrderBaseId { get; set; }

    public TechOrderLead TechOrderLead { get; set; }
    public OrderBase OrderBase { get; set; }
    public ICollection<Worker> Workers { get; set; }
    public ICollection<WorkTaskAssignment> WorkTaskAssignments { get; set; }
}
