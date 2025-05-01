using SemataryFabrick.Domain.Entities.Models;

namespace SemataryFabrick.Domain.Contracts.Repositories;
public interface IWorkTaskRepository
{
    void DeleteWorkTask(WorkTask workTask);
    void UpdateWorkTask(WorkTask workTask);
    Task AddWorkTaskAsync(WorkTask workTask);
    Task<WorkTask?> GetWorkTaskAsync(Guid id);
    Task<IEnumerable<WorkTask>> GetAllWorkTasksAsync();
}