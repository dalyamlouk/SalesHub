using ErrorOr;
using SalesHub.Application.Customer.Create;
using SalesHub.Application.Services.Customer.Common;
using Shouldly;

namespace SalesHub.Unit.Tests.Customer.Commands;

public class CreateCustomerCommandHandlerTests
{
    private readonly CreateCustomerCommandHandler handler = new CreateCustomerCommandHandler();

    [Fact]
    public async Task Handle_ShouldReturnCreateCustomerResult_WhenCustomerIsCreated()
    {
        var command = new CreateCustomerCommand
        (
            "John",
            "Doe",
            "1234567890",
            "john@doe.com"
        );

        var result = await handler.Handle(command, new CancellationToken());

        result.ShouldBeOfType<ErrorOr<CreateCustomerResult>>();
    }
}