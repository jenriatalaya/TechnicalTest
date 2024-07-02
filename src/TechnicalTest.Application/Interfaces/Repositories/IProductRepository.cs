using TechnicalTest.Application.DTOs;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<PaginationResponseDto<Product>> GetPagedListAsync(int pageNumber, int pageSize, string name);
}
