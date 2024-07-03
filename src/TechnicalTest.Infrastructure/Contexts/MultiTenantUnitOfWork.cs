using Microsoft.EntityFrameworkCore;
using TechnicalTest.Application.Interfaces;

namespace TechnicalTest.Infrastructure.Contexts;

public class MultiTenantUnitOfWork : IMultiTenantUnitOfWork
{
    private readonly DbContext _dbContext;

    public MultiTenantUnitOfWork(MultiTenantDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveChangesAsync()
    {
        bool result = true;

        result &= await _dbContext.SaveChangesAsync() > 0;

        return result;
    }

    public bool SaveChanges()
    {
        bool result = true;

        result &= _dbContext.SaveChanges() > 0;

        return result;
    }
}
