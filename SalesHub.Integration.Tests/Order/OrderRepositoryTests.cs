using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure.Tests
{
    public class OrderRepositoryTests : IDisposable
    {
        private readonly SalesHubDbContext _dbContext;
        private readonly OrderRepository _orderRepository;

        public OrderRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SalesHubDbContext>()
                .UseInMemoryDatabase("InMemorySalesHubDb")
                .Options;
            _dbContext = new SalesHubDbContext(options);
            _orderRepository = new OrderRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldCreateOrder()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Status = 1
            };

            var createdOrder = await _orderRepository.CreateAsync(order);

            createdOrder.ShouldNotBeNull();
            createdOrder.Id.ShouldBe(order.Id);
            createdOrder.CustomerId.ShouldBe(order.CustomerId);
            createdOrder.CreatedDate.ShouldBe(order.CreatedDate);
            createdOrder.UpdatedDate.ShouldBe(order.UpdatedDate);
            createdOrder.Status.ShouldBe(order.Status);
        }

        [Fact]
        public async Task DeleteOrderAsync_ShouldDeleteOrder()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Status = 1
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            var deleted = await _orderRepository.DeleteAsync(order.Id);

            deleted.ShouldBeTrue();
            _dbContext.Orders.ShouldNotContain(order);
        }

        [Fact]
        public async Task GetById_ShouldReturnOrder()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Status = 1
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            var retrievedOrder = await _orderRepository.GetById(order.Id);

            retrievedOrder.ShouldNotBeNull();
            retrievedOrder.Id.ShouldBe(order.Id);
            retrievedOrder.CustomerId.ShouldBe(order.CustomerId);
            retrievedOrder.CreatedDate.ShouldBe(order.CreatedDate);
            retrievedOrder.UpdatedDate.ShouldBe(order.UpdatedDate);
            retrievedOrder.Status.ShouldBe(order.Status);
        }

        [Fact]
        public async Task UpdateOrderAsync_ShouldUpdateOrder()
        {
            var order = new Order
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow,
                Status = 1
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            var updatedOrder = new Order
            {
                Id = order.Id,
                Status = 2,
                UpdatedDate = DateTime.UtcNow.AddDays(-1)
            };

            var updated = await _orderRepository.UpdateAsync(order.Id, updatedOrder.Status, updatedOrder.UpdatedDate, default);

            updated.ShouldNotBeNull();
            order.Id.ShouldBe(updated.Id);
            updated.Status.ShouldBe(updatedOrder.Status);
            updated.UpdatedDate.ShouldBe(updatedOrder.UpdatedDate);
        }
    }
}