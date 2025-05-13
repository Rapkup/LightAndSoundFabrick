using Microsoft.EntityFrameworkCore;
using SemataryFabrick.Domain.Contracts.Repositories;
using SemataryFabrick.Domain.Entities.Enums;
using SemataryFabrick.Domain.Entities.Models.OrderModels;
using SemataryFabrick.Infrastructure.Implementations.Contexts;

namespace SemataryFabrick.Infrastructure.Implementations.Repositories;
public class OrderBaseRepository(ApplicationContext context) : RepositoryBase<OrderBase>(context), IOrderBaseRepository
{
    public Task AddOrderBaseAsync(OrderBase orderBase) => CreateAsync(orderBase);

    public void DeleteOrderBase(OrderBase orderBase) => Delete(orderBase);

    public void UpdateOrderBase(OrderBase orderBase) => Update(orderBase);

    public async Task<IEnumerable<OrderBase>> GetAllOrderBasesAsync()
        => await Find().ToListAsync();

    public async Task<OrderBase?> GetOrderBaseAsync(Guid id)
        => await Find(ci => ci.Id == id).FirstOrDefaultAsync();

    public async Task<IEnumerable<OrderBase>?> GetOrdersBaseWithRelatedItemsByUserId(Guid userId)
        => await Find(o => o.CustomerId == userId)
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
        .ToListAsync();

    public async Task<OrderBase?> GetOrderBaseWithItemsAsync(Guid id)
        => await Find(o => o.Id == id)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync();

    public async Task<IEnumerable<OrderBase>> GetOrdersByManagerWithItemsAsync(Guid managerId)
    {
        return await context.Orders
            .Where(o => o.OrderManagerId == managerId)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.OrderCrews)
            .AsNoTracking()
            .ToListAsync();
    }

      public async Task<OrderBase?> GetOrderWithCrewsAndTasksAsync(Guid id)
    {
        return await context.Orders
            .Where(o => o.Id == id)
            .Include(o => o.OrderCrews)
                .ThenInclude(oc => oc.WorkTaskAssignments)
                    .ThenInclude(wta => wta.WorkTask)
            .Include(o => o.OrderCrews)
                .ThenInclude(oc => oc.Workers)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task UpdateOrderWithItemsAsync(OrderBase order)
    {
        var existingOrder = await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == order.Id);

        if (existingOrder != null)
        {
            // Обновление основных полей
            context.Entry(existingOrder).CurrentValues.SetValues(order);
            
            // Обработка элементов заказа
            foreach (var item in order.OrderItems)
            {
                var existingItem = existingOrder.OrderItems
                    .FirstOrDefault(i => i.Id == item.Id);
                
                if (existingItem != null)
                {
                    // Обновление существующего элемента
                    context.Entry(existingItem).CurrentValues.SetValues(item);
                }
                else
                {
                    // Добавление нового элемента
                    existingOrder.OrderItems.Add(item);
                }
            }

            // Удаление отсутствующих элементов
            foreach (var existingItem in existingOrder.OrderItems.ToList())
            {
                if (!order.OrderItems.Any(i => i.Id == existingItem.Id))
                {
                    context.OrderItems.Remove(existingItem);
                }
            }

            await context.SaveChangesAsync();
        }
    }

    public async Task AddOrderItemAsync(Guid orderId, OrderItem item)
    {
        var order = await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order != null)
        {
            order.OrderItems.Add(item);
            await context.SaveChangesAsync();
        }
    }

    public async Task RemoveOrderItemAsync(Guid orderId, Guid itemId)
    {
        var order = await context.Orders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == orderId);

        var item = order?.OrderItems.FirstOrDefault(i => i.Id == itemId);
        if (item != null)
        {
            context.OrderItems.Remove(item);
            await context.SaveChangesAsync();
        }
    }

    public async Task UpdateOrderStatusAsync(Guid orderId, OrderState state)
    {
        var order = await context.Orders.FindAsync(orderId);
        if (order != null)
        {
            order.OrderState = state;
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<OrderBase>> GetOrdersByStatusAsync(OrderState state)
    {
        return await context.Orders
            .Where(o => o.OrderState == state)
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<int> GetCompletedTasksCountAsync(Guid orderId)
    {
        return await context.WorkTaskAssignments
            .CountAsync(wta => wta.OrderCrew.OrderBaseId == orderId && wta.IsCompleted);
    }

    public async Task<int> GetTotalTasksCountAsync(Guid orderId)
    {
        return await context.WorkTaskAssignments
            .CountAsync(wta => wta.OrderCrew.OrderBaseId == orderId);
    }
}