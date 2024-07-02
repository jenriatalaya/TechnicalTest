using Microsoft.EntityFrameworkCore;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Infrastructure.Contexts;

public class MultiTenantDbContext : DbContext
{
    private readonly IOrganizationService _organizationService;

    public MultiTenantDbContext(
        DbContextOptions<MultiTenantDbContext> options,
        IOrganizationService organizationService
    )
        : base(options) =>
        _organizationService =
            organizationService ?? throw new ArgumentNullException(nameof(organizationService));

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //if (!optionsBuilder.IsConfigured)
        //{
            var connectionString = _organizationService.GetConnectionByOrganization().Result;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Organization Connection Not found");
            }

            optionsBuilder.UseNpgsql(
                connectionString,
                b =>
                {
                    b.SetPostgresVersion(new Version(9, 6));
                    b.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    b.CommandTimeout(300);
                }
            );

            optionsBuilder.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        }
    //}
}
