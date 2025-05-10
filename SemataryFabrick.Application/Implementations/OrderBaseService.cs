using Microsoft.Extensions.Logging;
using SemataryFabrick.Application.Contracts.Services;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Implementations;
public class OrderBaseService(IRepositoryManager repositoryManager, ILogger<CartService> logger) : IOrderBaseService
{
    public async Task<IEnumerable<OrderBase>> GetUserOrdersByStateAsync(Guid userId, OrderState? state, PaymentStatus? paymentStatus)
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
}