using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SalesHub.Application;

public static class DependencyInjection 
{
    public static IServiceCollection AddApplication(this IServiceCollection services){
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(typeof(DependencyInjection).GetTypeInfo().Assembly));

        return services;
    }
}