using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    public Task<Customer> CreateAsync(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> UpdateAsync(Customer customer, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}