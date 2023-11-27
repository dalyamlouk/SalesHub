using Microsoft.Extensions.DependencyInjection;
using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Common.Interfaces.Product;

namespace SalesHub.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services){
        services.AddScoped<ICustomerRepository, CustomerRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}