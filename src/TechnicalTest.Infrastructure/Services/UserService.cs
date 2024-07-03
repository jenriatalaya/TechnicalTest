using Microsoft.Extensions.Configuration;
using TechnicalTest.Application.DTOs;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Services;

public class UserService(IApplicationUnitOfWork unitOfWork, IUserRepository userRepository, IConfiguration configuration) : IUserService
{
    public async Task<string> AuthenticateAsync(LoginRequest request)
    {
        var user = await userRepository.GetUserByUsernameAsync(request.Username);
        if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            throw new Exception("Invalid username or password.");
        }

        var jwtToken = GenerateJwtToken(user);

        return jwtToken;
    }

    public async Task<long> RegisterUserAsync(string username, string password, string email, int organizationId)
    {
        var existingUser = await userRepository.GetUserByUsernameAsync(username);
        if (existingUser != null)
        {
            throw new Exception("Username already exists.");
        }

        var newUser = new User(username, BCrypt.Net.BCrypt.HashPassword(password), email, organizationId);

        await userRepository.AddAsync(newUser);
        await unitOfWork.SaveChangesAsync();

        return newUser.Id;
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["Jwt:Key"]);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
                [
                    new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim("orgId", user.OrganizationId.ToString())
                ]
            ),

            Expires = DateTime.UtcNow.AddMinutes(15),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature
            ),
            Issuer = configuration["Jwt:Issuer"],
            Audience = configuration["Jwt:Audience"]
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
