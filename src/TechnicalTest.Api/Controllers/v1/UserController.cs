using Microsoft.AspNetCore.Mvc;
using TechnicalTest.Application.DTOs;
using TechnicalTest.Application.Features.Commands.CreateUser;
using TechnicalTest.Application.Features.Queries.GetPagedListProduct;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Api.Controllers.v1;

public class UserController(IUserService userService) : BaseApiController
{
    [HttpPost]
    public async Task<BaseResult<long>> CreateUser(CreateUserCommand model) =>
        await Mediator.Send(model);

    [HttpPost]
    public async Task<BaseResult<string>> Login(LoginRequest request)
    {
        var token = await userService.AuthenticateAsync(request);
        return new BaseResult<string>(token);
    }
}