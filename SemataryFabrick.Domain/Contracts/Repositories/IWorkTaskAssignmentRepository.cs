using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IWorkTaskAssignmentRepository
{
    void DeleteWorkTaskAssignment(WorkTaskAssignment workTaskAssignment);
    void UpdateWorkTaskAssignment(WorkTaskAssignment workTaskAssignment);
    Task AddWorkTaskAssignmentAsync(WorkTaskAssignment workTaskAssignment);
    Task<WorkTaskAssignment?> GetWorkTaskAssignmentAsync(Guid id);
    Task<IEnumerable<WorkTaskAssignment>> GetAllWorkTaskAssignmentsAsync();
}