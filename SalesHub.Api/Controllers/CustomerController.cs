using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.Customer.Commands.Create;
using SalesHub.Application.Customer.Commands.Update;
using SalesHub.Application.Customer.Common;
using SalesHub.Application.Customer.Queries.Get;
using SalesHub.Application.Services.Customer.Common;
using SalesHub.Applications.Customer.Common;
using SalesHub.Contracts.Customer;

[ApiController]
[Route("customer")]
public class CustomerController : ControllerBase {

    private readonly ISender _sender;

    public CustomerController(ISender sender) 
    {
        _sender = sender;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateCustomer(CreateCustomerRequest request) {

        var command = new CreateCustomerCommand(request.FirstName, request.LastName, request.Phone, request.Email);
        ErrorOr<CreateCustomerResult> result = await _sender.Send(command);

        return Ok(result);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetCustomer(GetCustomerRequest request) {
        var query = new GetCustomerQuery(request.Email);
        ErrorOr<GetCustomerResult> result = await _sender.Send(query);

        return Ok(result);
    }

    [HttpGet("update")]
    public async Task<IActionResult> UpdateCustomer(UpdateCustomerRequest request) {
        var command = new UpdateCustomerCommand(request.Id, request.FirstName, request.LastName, request.Phone, request.Email);
        ErrorOr<UpdateCustomerResult> result = await _sender.Send(command);

        return Ok(result);
    }

}