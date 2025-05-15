using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Application.Entities.DTOs.OrderDtos;
using SemataryFabrick.Application.Entities.Exceptions;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Implementations;
public class OrderBaseService(IRepositoryManager repositoryManager, ILogger<CartService> logger) : IOrderBaseService
{
    public async Task<OrderBase> GetOrderByIdAsync(Guid id)
    {
        var order = await repositoryManager.OrderBase.GetOrderBaseAsync(id);

        if (order == null)
        {
            logger.LogWarning("Order not found with ID: {OrderId}", id);
            throw new KeyNotFoundException($"Order with ID {id} not found");
        }

        return order;
    }

    public async Task<IEnumerable<OrderItemDto>> GetOrderItemsAsync(Guid orderId)
    {
        var order = await repositoryManager.OrderBase.GetOrderBaseWithItemsAsync(orderId);

        if (order?.OrderItems == null)
        {
            return Enumerable.Empty<OrderItemDto>();
        }

        return order.OrderItems.Select(OrderItemDto.FromEntity);

    }
    public async Task<IEnumerable<OrderBase>> GetOrdersByManagerWithItemsAsync(Guid managerId)
    {
        try
        {
            logger.LogInformation("Getting orders with items for manager: {ManagerId}", managerId);
            return await repositoryManager.OrderBase.GetOrdersByManagerWithItemsAsync(managerId);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting orders for manager: {ManagerId}", managerId);
            throw;
        }
    }

    public async Task<IEnumerable<OrderBase>?> GetUserOrdersByStateOrPayStatusAsync(Guid userId, OrderState? state, PaymentStatus? paymentStatus)
    {
        var orderList = await repositoryManager.OrderBase.GetOrdersBaseWithRelatedItemsByUserId(userId);

        if (orderList == null)
        {
            if (state.HasValue)
                orderList = orderList.Where(o => o.OrderState == state.Value);

            if (paymentStatus.HasValue)
                orderList = orderList.Where(o => o.PaymentState == paymentStatus.Value);
        }

        return orderList;
    }

    public async Task UpdateOrderAsync(OrderBase order)
    {
        repositoryManager.OrderBase.UpdateOrderBase(order);
        await repositoryManager.SaveAsync();
    }

    public async Task UpdateOrderItemAsync(Guid itemId, int quantity)
    {
        var item = await repositoryManager.OrderItem.GetOrderItemAsync(itemId);
        if (item != null)
        {
            item.Quantity = quantity;
            repositoryManager.OrderItem.UpdateOrderItem(item);
            await repositoryManager.SaveAsync();
        }
    }

    public async Task RemoveOrderItemAsync(Guid itemId)
    {
        var item = await repositoryManager.OrderItem.GetOrderItemAsync(itemId);
        if (item != null)
        {
            repositoryManager.OrderItem.DeleteOrderItem(item);
            await repositoryManager.SaveAsync();
        }
    }

    public async Task AddOrderItemAsync(Guid orderId, Guid productId, int quantity)
    {
        var order = await repositoryManager.OrderBase.GetOrderBaseWithItemsAsync(orderId);
        if (order != null)
        {
            var newItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderBaseId = orderId,
                ProductId = productId,
                Quantity = quantity
            };

            order.OrderItems.Add(newItem);
            await repositoryManager.SaveAsync();
        }
    }

    public async Task<OrderBaseDto> GetOrderWithItemsAsync(Guid orderId)
    {
        var order = await repositoryManager.OrderBase.GetOrderBaseWithItemsAsync(orderId);
        return OrderBaseDto.FromEntity(order);
    }

    public async Task<OrderProgressDto> GetOrderProgressAsync(Guid orderId)
    {
        return new OrderProgressDto
        {
            CompletedTasks = await repositoryManager.OrderBase.GetCompletedTasksCountAsync(orderId),
            TotalTasks = await repositoryManager.OrderBase.GetTotalTasksCountAsync(orderId)
        };
    }


    public async Task UpdateOrderStatusAsync(Guid orderId, OrderState newState)
    {
        var order = await repositoryManager.OrderBase.GetOrderBaseAsync(orderId);
        if (order == null) throw new EntityNotFoundException(nameof(OrderBase), orderId);

        order.OrderState = newState;
        await repositoryManager.SaveAsync();
    }
}