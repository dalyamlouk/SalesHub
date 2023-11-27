using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure;

public class OrderRepository : IOrderRepository
{
    public Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Order?> UpdateAsync(Guid id, int status, DateTime updatedDate, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}