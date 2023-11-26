using ErrorOr;
using SalesHub.Application.Customer.Queries.Get;
using SalesHub.Application.Services.Customer.Common;
using Shouldly;

namespace SalesHub.Unit.Tests.Customer.Queries;

public class GetCustomerQueryHandlerTests 
{
    [Fact]
    public async Task Handle_ShouldReturnError_WhenCustomerNotFound()
    {
        var handler = new GetCustomerQueryHandler();
        var result = await handler.Handle(new GetCustomerQuery("john@doe.com"), new CancellationToken());

        result.ShouldBeOfType<ErrorOr<GetCustomerResult>>();
        result.FirstError.Code.ShouldBe("Customer.NotFound");
    }
}
