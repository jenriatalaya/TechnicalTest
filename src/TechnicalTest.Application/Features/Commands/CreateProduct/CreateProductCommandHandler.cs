using MediatR;
using TechnicalTest.Application.Features.Commands.CreateOrganization;
using TechnicalTest.Application.Interfaces;
using TechnicalTest.Application.Interfaces.Repositories;
using TechnicalTest.Application.Wrappers;
using TechnicalTest.Domain.Entities;

namespace TechnicalTest.Application.Features.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository, IMultiTenantUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, BaseResult<long>>
{
    public async Task<BaseResult<long>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = new Product(request.Name, request.Price, request.Quantity);

        await productRepository.AddAsync(product);
        await unitOfWork.SaveChangesAsync();

        return new BaseResult<long>(product.Id);
    }
}
