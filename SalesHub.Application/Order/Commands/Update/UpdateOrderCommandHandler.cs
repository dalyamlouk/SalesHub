using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Order.Commands.Update;
using SalesHub.Applications.Order.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Order.Update;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ErrorOr<UpdateOrderResult>>
{
    private readonly IOrderRepository _orderRepository;

    public UpdateOrderCommandHandler(IOrderRepository orderRepository){
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<UpdateOrderResult>> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var updatedOrder = await _orderRepository.UpdateAsync(request.Id, request.Status, request.UpdatedDate, cancellationToken);

        if(updatedOrder is null)
        {
            return Errors.Order.NotFound(request.Id);
        }

        return new UpdateOrderResult(
            updatedOrder.Id,
            updatedOrder.ProductId, 
            updatedOrder.CustomerId,
            updatedOrder.Status,
            updatedOrder.CreatedDate,
            updatedOrder.UpdatedDate
        );
    }
}