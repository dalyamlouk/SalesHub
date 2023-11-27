using ErrorOr;
using MediatR;
using SalesHub.Applications.Order.Common;

namespace SalesHub.Application.Order.Commands.Update;

public record UpdateOrderCommand(Guid Id,
                            int Status,
                            DateTime UpdatedDate) : IRequest<ErrorOr<UpdateOrderResult>>;