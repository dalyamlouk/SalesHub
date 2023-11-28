using Microsoft.EntityFrameworkCore;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure;

public class SalesHubDbContext : DbContext 
{
    public SalesHubDbContext(DbContextOptions<SalesHubDbContext> options) : base(options)
    {  
    }

    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
}