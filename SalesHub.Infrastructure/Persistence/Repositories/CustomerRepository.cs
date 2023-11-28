using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure.Persistence.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly SalesHubDbContext _dbContext;

    public CustomerRepository(SalesHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Customer?> CreateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(customer, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result == 1 ? customer : null;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(_dbContext.Customers.Single(c => c.Id == id));
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result != 0;
    }

    public async Task<Customer?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Customers.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public async Task<Customer?> UpdateAsync(Guid id, string firstName, string lastName, string phone, string email, CancellationToken cancellationToken = default)
    {
        var dbCustomer = await _dbContext.Customers.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if(dbCustomer is null) return null;

        dbCustomer.FirstName = firstName;
        dbCustomer.LastName = lastName;
        dbCustomer.Phone = phone;
        dbCustomer.Email = email;

        _dbContext.Customers.Update(dbCustomer);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result == 1 ? dbCustomer : null;
    }
}