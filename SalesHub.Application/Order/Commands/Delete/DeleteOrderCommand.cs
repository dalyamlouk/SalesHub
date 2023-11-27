using ErrorOr;
using MediatR;
using SalesHub.Applications.Order.Common;

namespace SalesHub.Application.Order.Commands.Delete
{
    public record DeleteOrderCommand(Guid Id) : IRequest<ErrorOr<DeleteOrderResult>>;
}