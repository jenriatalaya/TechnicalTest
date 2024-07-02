using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Services;

public class OrganizationService(
    IHttpContextAccessor httpContextAccessor,
    ApplicationDbContext dbContext,
    Func<MultiTenantDbContext> multiTenantDbContextFactory
) : IOrganizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly Func<MultiTenantDbContext> _multiTenantDbContextFactory =
        multiTenantDbContextFactory;

    public string GetCurrentTenant()
    {
        return _httpContextAccessor.HttpContext?.Items["slugTenant"] as string;
    }

    public async Task<string> GetConnectionByOrganization()
    {
        var slugTenant = GetCurrentTenant();

        if (string.IsNullOrEmpty(slugTenant))
        {
            throw new ArgumentException("slugTenant cannot be null or empty.");
        }

        var organization = await _dbContext.Organizations.FirstOrDefaultAsync(n =>
            n.SlugTenant == slugTenant
        );

        if (organization == null)
        {
            throw new ArgumentException($"Organization with slugTenant '{slugTenant}' not found.");
        }

        return organization.ConnectionString;
    }

    public async Task GenerateDatabaseAsync(string connectionString)
    {
        var builder = new NpgsqlConnectionStringBuilder(connectionString);
        var databaseName = builder.Database;
        builder.Database = "postgres";
        var masterConnectionString = builder.ToString();

        using (var connection = new NpgsqlConnection(masterConnectionString))
        {
            await connection.OpenAsync();

            if (!await DatabaseExistsAsync(connection))
            {
                return;
            }

            var createDbCommand = new NpgsqlCommand(
                $"CREATE DATABASE \"{databaseName}\"",
                connection
            );
            await createDbCommand.ExecuteNonQueryAsync();
        }

        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            var createTableCommand = new NpgsqlCommand(
                @"CREATE TABLE IF NOT EXISTS Products (
                Id SERIAL PRIMARY KEY,
                Name VARCHAR(100) NOT NULL,
                Price DECIMAL(18, 2) NOT NULL,
                Quantity INT NOT NULL
            )",
                connection
            );
            await createTableCommand.ExecuteNonQueryAsync();

            await SeedProductsAsync(connectionString);
        }
    }

    private async Task<bool> DatabaseExistsAsync(NpgsqlConnection connection)
    {
        string databaseName = connection.Database;
        var command = new NpgsqlCommand(
            "SELECT 1 FROM pg_database WHERE datname = @databaseName",
            connection
        );
        command.Parameters.AddWithValue("@databaseName", databaseName);

        var result = await command.ExecuteScalarAsync();
        return result != null;
    }

    private async Task SeedProductsAsync(string connectionString)
    {
        var products = new List<Product>
        {
            new("Product 1", 10.99m, 100),
            new("Product 2", 20.99m, 200),
            new("Product 3", 30.99m, 300),
        };

        var insertProductSql =
            @"
        INSERT INTO Products (Name, Price, Quantity)
        VALUES (@Name, @Price, @Quantity)";

        using (var connection = new NpgsqlConnection(connectionString))
        {
            await connection.OpenAsync();

            foreach (var product in products)
            {
                using (var command = new NpgsqlCommand(insertProductSql, connection))
                {
                    command.Parameters.AddWithValue("@Name", product.Name);
                    command.Parameters.AddWithValue("@Price", product.Price);
                    command.Parameters.AddWithValue("@Quantity", product.Quantity);

                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
