namespace SalesHub.Application.Common.Interfaces.Order;

public interface IOrderRepository {
    Task<Domain.Entities.Order?> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Order> CreateAsync(Domain.Entities.Order order, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Order?> UpdateAsync(Guid id, int status, DateTime updatedDate, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}