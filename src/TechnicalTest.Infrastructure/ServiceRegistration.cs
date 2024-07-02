using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Infrastructure.Services;
using TechnicalTest.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TechnicalTest.Infrastructure;

public static class ServiceRegistration
{
    public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IOrganizationService, OrganizationService>();

        services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
