using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;

namespace SemataryFabrick.Application.Contracts.Services;
public interface IOrderBaseService
{
    Task<IEnumerable<OrderBase>?> GetUserOrdersByStateOrPayStatusAsync(Guid userId, OrderState? state, PaymentStatus? paymentStatus);
}