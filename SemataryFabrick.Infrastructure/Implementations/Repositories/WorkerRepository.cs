using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.UserModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class WorkerRepository(ApplicationContext context)
        : RoleRepositoryBase<Worker>(context),
        IWorkerRepository
{
    protected override UserType GetUserType() => UserType.Worker;
    public Task AddWorkerAsync(Worker worker) => CreateAsync(worker);

    public void DeleteWorker(Worker worker) => Delete(worker);
    public void UpdateWorker(Worker worker) => Update(worker);

    public async Task<IEnumerable<Worker>> GetAllWorkersAsync()
        => await Find().ToListAsync();
    public async Task<Worker?> GetWorkerAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();
}