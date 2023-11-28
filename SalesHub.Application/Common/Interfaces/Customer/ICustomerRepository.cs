namespace SalesHub.Application.Common.Interfaces.Customer;

public interface ICustomerRepository {
    Task<Domain.Entities.Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Domain.Entities.Customer?> CreateAsync(Domain.Entities.Customer customer, CancellationToken cancellationToken);
    Task<Domain.Entities.Customer?> UpdateAsync(Guid id, string firstName, string lastName, string phone, string email, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
}