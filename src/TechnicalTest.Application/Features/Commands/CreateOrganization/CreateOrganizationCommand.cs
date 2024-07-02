using MediatR;
using TechnicalTest.Application.Wrappers;

namespace TechnicalTest.Application.Features.Commands.CreateOrganization;

public class CreateOrganizationCommand : IRequest<BaseResult<long>>
{
    public required string Name { get; set; }
    public required string SlugTenant { get; set; }
    public required string ConnectionString { get; set; }
}
