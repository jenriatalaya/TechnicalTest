using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Application.Features.Commands.CreateOrganization;
using TechnicalTest.Application.Features.Queries.GetPagedListProduct;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Api.Controllers.v1;

public class OrganizationController : BaseApiController
{
    [HttpPost]
    public async Task<BaseResult<long>> CreateOrganization(CreateOrganizationCommand model) =>
        await Mediator.Send(model);
}
