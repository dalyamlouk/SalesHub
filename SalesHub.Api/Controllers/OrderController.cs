using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.Order.Commands.Create;
using SalesHub.Application.Order.Commands.Delete;
using SalesHub.Application.Order.Commands.Update;
using SalesHub.Application.Order.Common;
using SalesHub.Application.Order.Queries.Get;
using SalesHub.Applications.Order.Common;
using SalesHub.Contracts.Order;

[ApiController]
[Route("order")]
public class OrderController : ControllerBase {

    private readonly ISender _sender;

    public OrderController(ISender sender) 
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateOrder(CreateOrderRequest request) {

        var command = new CreateOrderCommand(request.ProductId, request.CustomerId, request.Status);
        ErrorOr<CreateOrderResult> result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetOrder(GetOrderRequest request) {
        var query = new GetOrderQuery(request.Id);
        ErrorOr<GetOrderResult> result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateOrder(UpdateOrderRequest request) {
        var command = new UpdateOrderCommand(request.Id, request.Status, request.UpdatedDate);
        ErrorOr<UpdateOrderResult> result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteOrder(DeleteOrderRequest request) {
        var command = new DeleteOrderCommand(request.Id);
        ErrorOr<DeleteOrderResult> result = await _sender.Send(command);

        return Ok(result);
    }

}