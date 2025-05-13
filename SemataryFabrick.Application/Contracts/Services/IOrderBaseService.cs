using SemataryFabrick.Application.Entities.DTOs.OrderDtos;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Contracts.Services;
public interface IOrderBaseService
{
    Task<IEnumerable<OrderBase>?> GetUserOrdersByStateOrPayStatusAsync(Guid userId, OrderState? state, PaymentStatus? paymentStatus);

    Task<OrderBase> GetOrderByIdAsync(Guid id);
    Task UpdateOrderAsync(OrderBase order);
    Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync(Guid orderId);
    Task<IEnumerable<OrderBase>> GetOrdersByManagerWithItemsAsync(Guid managerId);

    Task UpdateOrderItemAsync(Guid itemId, int quantity);
    Task RemoveOrderItemAsync(Guid itemId);
    Task AddOrderItemAsync(Guid orderId, Guid productId, int quantity);
    Task<OrderBaseDto> GetOrderWithItemsAsync(Guid orderId);
    Task<OrderProgressDto> GetOrderProgressAsync(Guid orderId);

}