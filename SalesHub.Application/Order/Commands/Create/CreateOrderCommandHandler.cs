using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Order.Commands.Create;
using SalesHub.Application.Order.Common;

namespace SalesHub.Application.Order.Create;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ErrorOr<CreateOrderResult>>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository){
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<CreateOrderResult>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = new Domain.Entities.Order { 
            ProductId = request.ProductId, 
            CustomerId = request.CustomerId, 
            Status = request.Status
        };

        var createdOrder = await _orderRepository.CreateAsync(order);
  
        return new CreateOrderResult(
            createdOrder.Id,
            createdOrder.ProductId, 
            createdOrder.CustomerId,
            createdOrder.Status,
            createdOrder.CreatedDate,
            createdOrder.UpdatedDate
        );
    }
}