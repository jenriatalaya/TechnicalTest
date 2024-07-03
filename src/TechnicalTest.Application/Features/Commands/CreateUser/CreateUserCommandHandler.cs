using MediatR;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Features.Commands.CreateUser;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<CreateUserCommand, BaseResult<long>>
{
    public async Task<BaseResult<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var userId = await userService.RegisterUserAsync(request.Username, request.Password, request.Email, request.OrganizationId);        

        return new BaseResult<long>(userId);
    }
}
