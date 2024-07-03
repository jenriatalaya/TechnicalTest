namespace TechnicalTest.Application.Interfaces;

public interface IMultiTenantUnitOfWork
{
    Task<bool> SaveChangesAsync();
}
