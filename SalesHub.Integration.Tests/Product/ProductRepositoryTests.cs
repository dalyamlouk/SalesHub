namespace SalesHub.Infrastructure.Tests.Product
{
    public class ProductRepositoryTests : IDisposable
    {
        private readonly SalesHubDbContext _dbContext;
        private readonly ProductRepository _productRepository;

        public ProductRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<SalesHubDbContext>()
                .UseInMemoryDatabase("InMemorySalesHubDb")
                .Options;
            _dbContext = new SalesHubDbContext(options);
            _productRepository = new ProductRepository(_dbContext);
        }

        public void Dispose()
        {
            _dbContext.Database.EnsureDeleted(); // Ensures the database is deleted after each test
        }

        [Fact]
        public async Task CreateProductAsync_ShouldCreateProduct()
        {
            var product = new Domain.Entities.Product { Id = Guid.NewGuid(), Name = "Product", SKU = "13546546", Description = "Description" };

            var createdProduct = await _productRepository.CreateAsync(product);

            createdProduct.ShouldNotBeNull();
            createdProduct.Id.ShouldBe(product.Id);
            createdProduct.Name.ShouldBe(product.Name);
            createdProduct.SKU.ShouldBe(product.SKU);
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldDeleteProduct()
        {
            var product = new Domain.Entities.Product { Id = Guid.NewGuid(), Name = "Product", SKU = "13546546", Description = "Description" };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var deleted = await _productRepository.DeleteAsync(product.Id);

            deleted.ShouldBeTrue();
            _dbContext.Products.ShouldNotContain(product);
        }

        [Fact]
        public async Task GetBySkuAsync_ShouldReturnProduct()
        {
            var product = new Domain.Entities.Product { Id = Guid.NewGuid(), Name = "Product", SKU = "13546546", Description = "Description" };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var retrievedProduct = await _productRepository.GetBySkuAsync(product.SKU);

            retrievedProduct.ShouldNotBeNull();
            retrievedProduct.Id.ShouldBe(product.Id);
            retrievedProduct.Name.ShouldBe(product.Name);
            retrievedProduct.SKU.ShouldBe(product.SKU);
        }

        [Fact]
        public async Task UpdateProductAsync_ShouldUpdateProduct()
        {
            var product = new Domain.Entities.Product { Id = Guid.NewGuid(), Name = "Product", SKU = "13546546", Description = "Description" };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            var updated = await _productRepository.UpdateAsync(product.Id, "Updated Product Name", "Updated Description", "13546546");

            updated.ShouldNotBeNull();
            updated.Id.ShouldBe(product.Id);
            updated.Name.ShouldBe("Updated Product Name");
            updated.Description.ShouldBe("Updated Description");
            updated.SKU.ShouldBe("13546546");
        }
    }
}