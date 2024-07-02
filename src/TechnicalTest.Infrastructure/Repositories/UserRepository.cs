using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext dbContext)
    : GenericRepository<User>(dbContext),
        IUserRepository { }
