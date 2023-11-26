using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces;
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
        var Product = new Domain.Entities.Product { 
            Id = request.Id,
            Name = request.Name, 
            Description = request.Description, 
            SKU = request.SKU, 
        };

        var updatedProduct = await _productRepository.UpdateAsync(Product, cancellationToken);

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