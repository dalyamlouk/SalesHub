using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Product;
using SalesHub.Application.Product.Commands.Create;
using SalesHub.Application.Product.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Product.Create;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ErrorOr<CreateProductResult>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository ProductRepository){
        _productRepository = ProductRepository;
    }

    public async Task<ErrorOr<CreateProductResult>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _productRepository.GetBySkuAsync(request.SKU);

        if (existingProduct is not null) 
        {
            return Errors.Product.AlreadyExists(request.SKU);
        }

        var Product = new Domain.Entities.Product { 
            Name = request.Name, 
            Description = request.Description, 
            SKU = request.SKU, 
        };

        var createdProduct = await _productRepository.CreateAsync(Product);
  
        return new CreateProductResult(
            createdProduct.Id,
            createdProduct.Name, 
            createdProduct.Description,
            createdProduct.SKU
        );
    }
}