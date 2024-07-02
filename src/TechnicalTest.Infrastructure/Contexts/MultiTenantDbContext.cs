using Microsoft.EntityFrameworkCore;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Infrastructure.Contexts;

public class MultiTenantDbContext(DbContextOptions<MultiTenantDbContext> options, IOrganizationService organizationService) : DbContext(options)
{
    private readonly IOrganizationService _organizationService = organizationService;

    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }

    protected override async void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = await _organizationService.GetConnectionByOrganization();
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Organization Connection Not found");
        }

        optionsBuilder.UseNpgsql(connectionString, b =>
        {
            b.SetPostgresVersion(new Version(9, 6));
            b.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
            b.CommandTimeout(300);
        });

        optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
    }
}
