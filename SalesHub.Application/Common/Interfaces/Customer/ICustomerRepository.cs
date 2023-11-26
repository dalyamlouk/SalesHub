namespace SalesHub.Application.Common.Interfaces;

public interface ICustomerRepository {
    Domain.Entities.Customer? GetCustomerById(Guid id);
    Task<Domain.Entities.Customer?> CreateAsync(Domain.Entities.Customer customer);
}