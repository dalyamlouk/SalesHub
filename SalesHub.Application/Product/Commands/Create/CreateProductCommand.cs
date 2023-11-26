using ErrorOr;
using MediatR;
using SalesHub.Application.Product.Common;

namespace SalesHub.Application.Product.Commands.Create;

public record CreateProductCommand(string Name,
                            string Description,
                            string SKU) : IRequest<ErrorOr<CreateProductResult>>;