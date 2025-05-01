using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Models;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class WorkTaskRepository(ApplicationContext context) : RepositoryBase<WorkTask>(context), IWorkTaskRepository
{
    public Task AddWorkTaskAsync(WorkTask workTask) => CreateAsync(workTask);

    public void DeleteWorkTask(WorkTask workTask) => Delete(workTask);

    public void UpdateWorkTask(WorkTask workTask) => Update(workTask);

    public async Task<IEnumerable<WorkTask>> GetAllWorkTasksAsync()
        => await Find().ToListAsync();

    public async Task<WorkTask?> GetWorkTaskAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}