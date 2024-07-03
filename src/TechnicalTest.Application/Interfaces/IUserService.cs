using TechnicalTest.Application.DTOs;

namespace TechnicalTest.Application.Interfaces;

public interface IUserService
{
    Task<string> AuthenticateAsync(LoginRequest request);
    Task<long> RegisterUserAsync(string username, string password, string email, int organizationId);
}