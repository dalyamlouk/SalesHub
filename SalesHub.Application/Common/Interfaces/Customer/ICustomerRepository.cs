namespace SalesHub.Application.Common.Interfaces.Customer;

public interface ICustomerRepository {
    Task<Domain.Entities.Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Customer> CreateAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken = default);
    Task<Domain.Entities.Customer?> UpdateAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}