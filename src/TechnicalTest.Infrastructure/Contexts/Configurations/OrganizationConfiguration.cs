using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Infrastructure.Contexts.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("Organizations");

        builder.HasKey(o => o.Id);
        builder.Property(o => o.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();

        builder.Property(o => o.Name).HasColumnName("Name").IsRequired();
        builder.Property(o => o.SlugTenant).HasColumnName("SlugTenant").IsRequired();
        builder.Property(o => o.ConnectionString).HasColumnName("ConnectionString").IsRequired();
    }
}
