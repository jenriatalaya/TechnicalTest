using MediatR;
using TechnicalTest.Application.Wrappers;

namespace TechnicalTest.Application.Features.Commands.CreateUser;

public class CreateUserCommand : IRequest<BaseResult<long>>
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public int OrganizationId { get; set; }
}
