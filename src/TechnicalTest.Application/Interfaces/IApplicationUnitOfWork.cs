namespace TechnicalTest.Application.Interfaces;

public interface IApplicationUnitOfWork
{
    Task<bool> SaveChangesAsync();
}
