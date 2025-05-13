using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class WorkTaskAssignmentRepository(ApplicationContext context) : RepositoryBase<WorkTaskAssignment>(context), IWorkTaskAssignmentRepository
{
    public Task AddWorkTaskAssignmentAsync(WorkTaskAssignment workTaskAssignment) => CreateAsync(workTaskAssignment);

    public void DeleteWorkTaskAssignment(WorkTaskAssignment workTaskAssignment) => Delete(workTaskAssignment);

    public void UpdateWorkTaskAssignment(WorkTaskAssignment workTaskAssignment) => Update(workTaskAssignment);

    public async Task<IEnumerable<WorkTaskAssignment>> GetAllWorkTaskAssignmentsAsync()
        => await Find().ToListAsync();

    public async Task<WorkTaskAssignment?> GetWorkTaskAssignmentAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
    public async Task<IEnumerable<WorkTaskAssignment>> GetTasksByOrderAsync(Guid orderId)
    {
        return await Find(ta => ta.OrderCrew.OrderBaseId == orderId)
             .Include(wta => wta.WorkTask)
            .Include(wta => wta.OrderCrew)
            .ToListAsync();
    }
}