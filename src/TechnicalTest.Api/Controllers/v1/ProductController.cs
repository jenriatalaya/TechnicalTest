using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Application.Features.Commands.CreateProduct;
using TechnicalTest.Application.Features.Queries.GetPagedListProduct;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Api.Controllers.v1;

public class ProductController : BaseApiControllerWithTenant
{
    [Authorize]
    [HttpGet]
    public async Task<PagedResponse<Product>> GetPagedListProduct([FromQuery] GetPagedListProductQuery model)
        => await Mediator.Send(model);

    [Authorize]
    [HttpPost]
    public async Task<BaseResult<long>> CreateProduct(CreateProductCommand model) =>
        await Mediator.Send(model);
}
