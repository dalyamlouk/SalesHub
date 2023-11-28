using Microsoft.EntityFrameworkCore;
using SalesHub.Application.Common.Interfaces.Product;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly SalesHubDbContext _dbContext;

    public ProductRepository(SalesHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Product> CreateAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(product, cancellationToken);
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result == 1 ? product : null;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(_dbContext.Products.Single(c => c.Id == id));
        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result != 0;
    }

    public async Task<Product?> GetBySkuAsync(string sku, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Products.FirstOrDefaultAsync(x => x.SKU == sku, cancellationToken);    
    }

    public async Task<Product?> UpdateAsync(Guid id, string name, string description, string sku, CancellationToken cancellationToken = default)
    {
        var dbProduct = await _dbContext.Products.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if(dbProduct is null) return null;

        dbProduct.Name = name;
        dbProduct.Description = description;
        dbProduct.SKU = sku;

        _dbContext.Products.Update(dbProduct);

        var result = await _dbContext.SaveChangesAsync(cancellationToken);

        return result == 1 ? dbProduct : null;
    }
}