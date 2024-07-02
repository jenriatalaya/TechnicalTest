using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Application.DTOs;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Domain.Entities;
using TechnicalTest.Infrastructure.Contexts;

namespace TechnicalTest.Infrastructure.Repositories;

public class ProductRepository(MultiTenantDbContext multiTenantDbContext)
    : GenericRepository<Product>(multiTenantDbContext),
        IProductRepository
{
    public async Task<PaginationResponseDto<Product>> GetPagedListAsync(
        int pageNumber,
        int pageSize,
        string name
    )
    {
        var query = multiTenantDbContext.Products.AsQueryable();

        if (!string.IsNullOrEmpty(name))
        {
            query = query.Where(n => n.Name.Contains(name));
        }

        return await Paged(
            query.Select(p => new Product(p.Name, p.Price, p.Quantity)),
            pageNumber,
            pageSize
        );
    }
}
