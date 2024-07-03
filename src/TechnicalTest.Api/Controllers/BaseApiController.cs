using TechnicalTest.Api.Infrastracture.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TechnicalTest.Api.Controllers;

[ApiController]
[ApiResultFilter]
[Route("api/v{version:apiVersion}/{slugTenant}/[controller]/[action]")]
public abstract class BaseApiControllerWithTenant : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
}

[ApiController]
[ApiResultFilter]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public abstract class BaseApiController : ControllerBase
{
    protected IMediator Mediator => HttpContext.RequestServices.GetService<IMediator>();
}
