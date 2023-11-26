using SalesHub.Application.Common.Interfaces;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure;

public class CustomerRepository : ICustomerRepository
{
    public Task<Customer> CreateAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> GetCustomerByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<Customer?> UpdateCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}