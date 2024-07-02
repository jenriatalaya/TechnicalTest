using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Services;

public class OrganizationService(IHttpContextAccessor httpContextAccessor, ApplicationDbContext dbContext) : IOrganizationService
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ApplicationDbContext _dbContext = dbContext;

    public string GetCurrentTenant()
    {
        var slugTenant = _httpContextAccessor.HttpContext?.Items["slugTenant"] as string;
        return slugTenant;
    }
    public async Task<string> GetConnectionByOrganization()
    {
        var slugTenant = GetCurrentTenant();

        if (string.IsNullOrEmpty(slugTenant))
        {
            throw new ArgumentException("slugTenant cannot be null or empty.");
        }

        var organization = await _dbContext.Organizations.FirstOrDefaultAsync(n => n.SlugTenant == slugTenant);

        if (organization == null)
        {
            throw new ArgumentException($"Organization with slugTenant '{slugTenant}' not found.");
        }

        return organization.ConnectionString;

    }
}
