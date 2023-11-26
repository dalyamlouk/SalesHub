using ErrorOr;
using MediatR;
using SalesHub.Applications.Product.Common;

namespace SalesHub.Application.Product.Commands.Delete
{
    public record DeleteProductCommand(Guid Id) : IRequest<ErrorOr<DeleteProductResult>>;
}