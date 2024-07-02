using MediatR;
using TechnicalTest.Application.Parameters;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Features.Queries.GetPagedListProduct;

public class GetPagedListProductQuery : PaginationRequestParameter, IRequest<PagedResponse<Product>>
{
    public string Name { get; set; }
}
