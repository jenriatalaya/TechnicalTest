using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Application.Interfaces;

namespace TechnicalTest.Infrastructure.Contexts;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync() > 0;
    }

    public bool SaveChanges()
    {
        return dbContext.SaveChanges() > 0;
    }
}
