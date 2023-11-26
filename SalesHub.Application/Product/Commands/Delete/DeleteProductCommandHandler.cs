using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces;
using SalesHub.Applications.Product.Common;

namespace SalesHub.Application.Product.Commands.Delete;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ErrorOr<DeleteProductResult>>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository ProductRepository)
    {
        _productRepository = ProductRepository;
    }

    public async Task<ErrorOr<DeleteProductResult>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var result = await _productRepository.DeleteAsync(request.Id, cancellationToken);

        return new DeleteProductResult(result);
    }
}