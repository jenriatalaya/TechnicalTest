using MediatR;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Features.Queries.GetPagedListProduct;

public class GetPagedListProductQueryHandler() : IRequestHandler<GetPagedListProductQuery, PagedResponse<Product>>
{
    public Task<PagedResponse<Product>> Handle(GetPagedListProductQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
