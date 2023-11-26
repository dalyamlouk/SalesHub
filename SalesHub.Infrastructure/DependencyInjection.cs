using Microsoft.Extensions.DependencyInjection;
using SalesHub.Application.Common.Interfaces;

namespace SalesHub.Infrastructure;

public static class DependencyInjection 
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services){
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}