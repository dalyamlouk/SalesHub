using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure.Tests
{
    public class CustomerRepositoryTests : IDisposable
    {
        private readonly SalesHubDbContext _dbContext;
        private readonly CustomerRepository _customerRepository;

        public CustomerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SalesHubDbContext>()
                .UseInMemoryDatabase("InMemorySalesHubDb")
                .Options;
            _dbContext = new SalesHubDbContext(options);
            _customerRepository = new CustomerRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CreateCustomerAsync_ShouldCreateCustomer()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john@doe.com",
            };

            var createdCustomer = await _customerRepository.CreateAsync(customer);

            createdCustomer.ShouldNotBeNull();
            createdCustomer.Id.ShouldBe(customer.Id);
            createdCustomer.FirstName.ShouldBe(customer.FirstName);
            createdCustomer.LastName.ShouldBe(customer.LastName);
            createdCustomer.Phone.ShouldBe(customer.Phone);
            createdCustomer.Email.ShouldBe(customer.Email);
        }

        [Fact]
        public async Task DeleteCustomerAsync_ShouldDeleteCustomer()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john@doe.com",
            };

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            var deleted = await _customerRepository.DeleteAsync(customer.Id);

            deleted.ShouldBeTrue();
            _dbContext.Customers.ShouldNotContain(customer);
        }

        [Fact]
        public async Task GetByEmailAsync_ShouldReturnCustomer()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john@doe.com",
            };

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            var retrievedCustomer = await _customerRepository.GetByEmailAsync(customer.Email);

            retrievedCustomer.ShouldNotBeNull();
            retrievedCustomer.Id.ShouldBe(customer.Id);
            retrievedCustomer.FirstName.ShouldBe(customer.FirstName);
            retrievedCustomer.LastName.ShouldBe(customer.LastName);
            retrievedCustomer.Phone.ShouldBe(customer.Phone);
            retrievedCustomer.Email.ShouldBe(customer.Email);
        }

        [Fact]
        public async Task UpdateCustomerAsync_ShouldUpdateCustomer()
        {
            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john@doe.com",
            };

            await _dbContext.Customers.AddAsync(customer);
            await _dbContext.SaveChangesAsync();

            var updatedCustomer = new Customer
            {
                Id = customer.Id,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Phone = "1111111",
                Email = "updated@test.com",
            };

            var updated = await _customerRepository.UpdateAsync(customer.Id, updatedCustomer.FirstName, updatedCustomer.LastName, 
                                                                updatedCustomer.Phone, updatedCustomer.Email);

            updated.ShouldNotBeNull();
            updated.Id.ShouldBe(customer.Id);
            updated.FirstName.ShouldBe(updatedCustomer.FirstName);
            updated.LastName.ShouldBe(updatedCustomer.LastName);
            updated.Phone.ShouldBe(updatedCustomer.Phone);
            updated.Email.ShouldBe(updatedCustomer.Email);
        }
    }
}