using MediatR;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Features.Commands.CreateOrganization;

public class CreateOrganizationCommandHandler(
    IOrganizationRepository organizationRepository,
    IOrganizationService organizationService,
    IUnitOfWork unitOfWork
) : IRequestHandler<CreateOrganizationCommand, BaseResult<long>>
{
    public async Task<BaseResult<long>> Handle(
        CreateOrganizationCommand request,
        CancellationToken cancellationToken
    )
    {
        var organization = new Organization(
            request.Name,
            request.SlugTenant,
            request.ConnectionString
        );

        await organizationService.GenerateDatabaseAsync(organization.ConnectionString);

        await organizationRepository.AddAsync(organization);
        await unitOfWork.SaveChangesAsync();

        return new BaseResult<long>(organization.Id);
    }
}
