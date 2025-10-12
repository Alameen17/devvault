using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace DevVault.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Register MediatR handlers (CQRS)
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }
    }
}
