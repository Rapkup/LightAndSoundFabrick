using SemataryFabrick.Domain.Entities.Models.UserModels;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IWorkerRepository
{
    void DeleteWorker(Worker Wwrkers);
    void UpdateWorker(Worker workers);
    Task AddWorkerAsync(Worker worker);
    Task<Worker?> GetWorkerAsync(Guid id);
    Task<IEnumerable<Worker>> GetAllWorkersAsync();
}