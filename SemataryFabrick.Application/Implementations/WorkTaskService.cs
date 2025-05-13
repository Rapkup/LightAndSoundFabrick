using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.OrderDtos;
using SemataryFabrick.Domain.Contracts.Repositories;

namespace SemataryFabrick.Application.Implementations;
public class WorkTaskService(IRepositoryManager repositoryManager, ILogger<WorkTaskService> logger) : IWorkTaskService
{
    public async Task<OrderProgressDto> GetOrderProgressAsync(Guid orderId)
    {
        var tasks = await repositoryManager.TaskAssignment
            .GetTasksByOrderAsync(orderId);

        return new OrderProgressDto
        {
            TotalTasks = tasks.Count(),
            CompletedTasks = tasks.Count(t => t.IsCompleted)
        };
    }
}