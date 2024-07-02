using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Infrastructure.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).HasColumnName("Id").IsRequired().ValueGeneratedOnAdd();

        builder.Property(u => u.Username).HasColumnName("Username").IsRequired();
        builder.Property(u => u.Password).HasColumnName("Password").IsRequired();
        builder.Property(u => u.Email).HasColumnName("Email").IsRequired();
        
        builder.HasOne(u => u.Organization)
               .WithMany()
               .HasForeignKey(u => u.OrganizationId)
               .IsRequired();
    }
}
