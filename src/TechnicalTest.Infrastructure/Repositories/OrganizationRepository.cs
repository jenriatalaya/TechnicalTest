using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Repositories;

public class OrganizationRepository(ApplicationDbContext dbContext)
    : GenericRepository<Organization>(dbContext),
        IOrganizationRepository { }
