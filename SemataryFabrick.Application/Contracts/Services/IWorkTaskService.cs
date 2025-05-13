using SemataryFabrick.Application.Entities.DTOs.OrderDtos;

namespace SemataryFabrick.Application.Contracts.Services;
public interface IWorkTaskService
{
    Task<OrderProgressDto> GetOrderProgressAsync(Guid orderId);
}