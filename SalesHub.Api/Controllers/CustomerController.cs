using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SalesHub.Application.Customer.Create;
using SalesHub.Application.Services.Customer.Common;
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
    public async Task<IActionResult> CreateCustomer(AddCustomerRequest request) {

        var command = new CreateCustomerCommand(request.FirstName, request.LastName, request.Phone, request.Email);
        ErrorOr<CreateCustomerResult> result = await _sender.Send(command);

        return Ok(result);
    }

}