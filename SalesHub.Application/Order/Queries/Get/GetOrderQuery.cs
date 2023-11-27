using ErrorOr;
using MediatR;
using SalesHub.Application.Order.Common;

namespace SalesHub.Application.Order.Queries.Get;

public record GetOrderQuery(Guid Id) : IRequest<ErrorOr<GetOrderResult>>;