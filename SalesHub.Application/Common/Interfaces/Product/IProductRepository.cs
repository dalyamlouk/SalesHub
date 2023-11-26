namespace SalesHub.Application.Common.Interfaces;

public interface IProductRepository {
    Task<Domain.Entities.Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Product> CreateAsync(Domain.Entities.Product product, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Product?> UpdateAsync(Domain.Entities.Product product, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}