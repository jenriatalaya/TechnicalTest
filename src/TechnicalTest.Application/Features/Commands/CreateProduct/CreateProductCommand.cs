using MediatR;
using TechnicalTest.Application.Wrappers;

namespace TechnicalTest.Application.Features.Commands.CreateProduct;

public class CreateProductCommand : IRequest<BaseResult<long>>
{
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}
