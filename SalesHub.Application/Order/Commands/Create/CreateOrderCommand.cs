using ErrorOr;
using MediatR;
using SalesHub.Application.Order.Common;

namespace SalesHub.Application.Order.Commands.Create;

public record CreateOrderCommand(Guid ProductId,
                            Guid CustomerId,
                            int Status) : IRequest<ErrorOr<CreateOrderResult>>;