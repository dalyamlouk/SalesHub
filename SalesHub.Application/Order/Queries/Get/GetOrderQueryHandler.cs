using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Order.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Order.Queries.Get;

public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, ErrorOr<GetOrderResult>>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<ErrorOr<GetOrderResult>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
    {
        if(await _orderRepository.GetById(request.Id) is Domain.Entities.Order order) 
        {
            return new GetOrderResult(order.Id, order.ProductId, order.CustomerId, order.Status, order.CreatedDate, order.UpdatedDate);
        }

        return Errors.Order.NotFound(request.Id);
    }
}