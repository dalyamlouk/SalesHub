using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Product;
using SalesHub.Application.Product.Commands.Update;
using SalesHub.Applications.Product.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Product.Update;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ErrorOr<UpdateProductResult>>
{
    private readonly IProductRepository _productRepository;

    public UpdateProductCommandHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    public async Task<ErrorOr<UpdateProductResult>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var updatedProduct = await _productRepository.UpdateAsync(
            request.Id, request.Name, request.Description, request.SKU, cancellationToken);

        if(updatedProduct is null)
        {
            return Errors.Product.NotFound(request.Id);
        }

        return new UpdateProductResult(
            updatedProduct.Id,
            updatedProduct.Name, 
            updatedProduct.Description,
            updatedProduct.SKU        
        );
    }
}