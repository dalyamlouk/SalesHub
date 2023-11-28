using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Common.Interfaces.Product;
using SalesHub.Infrastructure.Persistence.Repositories;

namespace SalesHub.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services){
        services.AddDbContext<SalesHubDbContext>(options => 
            options.UseSqlServer("SalesHubDb"));
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}