using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Infrastructure.Contexts;
using TechnicalTest.Infrastructure.Repositories;
using TechnicalTest.Infrastructure.Services;

namespace TechnicalTest.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureLayer(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<Func<MultiTenantDbContext>>(provider =>
            () =>
            {
                var options = provider.GetService<DbContextOptions<MultiTenantDbContext>>();
                var organizationService = provider.GetService<IOrganizationService>();
                return new MultiTenantDbContext(options, organizationService);
            }
        );

        services.AddDbContext<MultiTenantDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"))
        );

        services.AddScoped<IOrganizationService, OrganizationService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.RegisterRepositories();
        return services;
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        var interfaceType = typeof(IGenericRepository<>);
        var interfaces = Assembly
            .GetAssembly(interfaceType)
            .GetTypes()
            .Where(p => p.GetInterface(interfaceType.Name) != null);

        var implementations = Assembly.GetAssembly(typeof(GenericRepository<>)).GetTypes();

        foreach (var item in interfaces)
        {
            var implementation = implementations.FirstOrDefault(p =>
                p.GetInterface(item.Name) != null
            );
            services.AddTransient(item, implementation);
        }
    }
}
