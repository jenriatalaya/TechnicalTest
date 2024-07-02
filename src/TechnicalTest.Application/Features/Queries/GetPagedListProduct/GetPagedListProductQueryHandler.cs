using MediatR;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Features.Queries.GetPagedListProduct;

public class GetPagedListProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetPagedListProductQuery, PagedResponse<Product>>
{
    public async Task<PagedResponse<Product>> Handle(
        GetPagedListProductQuery request,
        CancellationToken cancellationToken
    )
    {
        var result = await productRepository.GetPagedListAsync(
            request.PageNumber,
            request.PageSize,
            request.Name
        );

        return new PagedResponse<Product>(result);
    }
}
