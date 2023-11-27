using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Applications.Order.Common;

namespace SalesHub.Application.Order.Commands.Delete;

public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand, ErrorOr<DeleteOrderResult>>
{
    private readonly IOrderRepository _orderRepository;

    public DeleteOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<DeleteOrderResult>> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var result = await _orderRepository.DeleteAsync(request.Id, cancellationToken);

        return new DeleteOrderResult(result);
    }
}