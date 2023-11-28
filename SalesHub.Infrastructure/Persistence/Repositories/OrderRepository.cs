using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly SalesHubDbContext _dbContext;

    public OrderRepository(SalesHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Order> CreateAsync(Order order, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(order, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result == 1 ? order : null;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(_dbContext.Orders.Single(c => c.Id == id));
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result != 0;
    }

    public async Task<Order?> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Order?> UpdateAsync(Guid id, int status, DateTime updatedDate, CancellationToken cancellationToken = default)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if(order is null) return null;

        order.Status = status;
        order.UpdatedDate = updatedDate;

        _dbContext.Orders.Update(order);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result == 1 ? order : null;
    }
}