using Microsoft.EntityFrameworkCore;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext)
    : GenericRepository<User>(dbContext),
        IUserRepository
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    public async Task<User> GetUserByUsernameAsync(string username)
    {
        return await _dbContext.Users.Where(n => n.Username == username).FirstOrDefaultAsync();
    }
}
