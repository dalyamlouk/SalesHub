namespace SalesHub.Application.Common.Interfaces;

public interface ICustomerRepository {
    Task<Domain.Entities.Customer?> GetCustomerByEmailAsync(string email);
    Task<Domain.Entities.Customer> CreateAsync(Domain.Entities.Customer customer);
    Task<Domain.Entities.Customer?> UpdateCustomerAsync(Domain.Entities.Customer customer);
}