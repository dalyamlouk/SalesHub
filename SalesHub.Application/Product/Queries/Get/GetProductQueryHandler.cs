using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Product;
using SalesHub.Application.Services.Product.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Product.Queries.Get;

public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ErrorOr<GetProductResult>>
{
    private readonly IProductRepository _productRepository;

    public GetProductQueryHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    public async Task<ErrorOr<GetProductResult>> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        if(await _productRepository.GetBySkuAsync(request.SKU, cancellationToken) is Domain.Entities.Product Product) 
        {
            return new GetProductResult(Product.Id, Product.Name, Product.Description, Product.SKU);
        }

        return Errors.Product.NotFound(request.SKU);
    }
}