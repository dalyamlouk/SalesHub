using ErrorOr;
using MediatR;
using SalesHub.Application.Services.Product.Common;

namespace SalesHub.Application.Product.Queries.Get;

public record GetProductQuery(string SKU) : IRequest<ErrorOr<GetProductResult>>;