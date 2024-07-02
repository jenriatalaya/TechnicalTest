using Microsoft.EntityFrameworkCore;
using TechnicalTest.Application.DTOs;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {

        private readonly DbContext _currentDbContext;

        public GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _currentDbContext = applicationDbContext;
        }

        public GenericRepository(MultiTenantDbContext multiTenantDbContext)
        {
            _currentDbContext = multiTenantDbContext;
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _currentDbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _currentDbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _currentDbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _currentDbContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _currentDbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        protected async Task<PaginationResponseDto<TEntity>> Paged<TEntity>(
            IQueryable<TEntity> query,
            int pageNumber,
            int pageSize
        )
            where TEntity : class
        {
            var count = await query.CountAsync();

            var pagedResult = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return new(pagedResult, count, pageNumber, pageSize);
        }
    }
}
