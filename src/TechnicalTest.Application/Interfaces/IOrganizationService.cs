namespace TechnicalTest.Application.Interfaces;

public interface IOrganizationService
{
    Task<string> GetConnectionByOrganization();
}
