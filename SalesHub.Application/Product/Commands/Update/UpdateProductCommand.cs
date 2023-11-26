using ErrorOr;
using MediatR;
using SalesHub.Applications.Product.Common;

namespace SalesHub.Application.Product.Commands.Update;

public record UpdateProductCommand(Guid Id,
                            string Name,
                            string Description,
                            string SKU) : IRequest<ErrorOr<UpdateProductResult>>;