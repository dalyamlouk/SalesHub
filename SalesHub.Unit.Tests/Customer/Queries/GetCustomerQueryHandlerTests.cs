using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Application.Customer.Queries.Get;
using SalesHub.Application.Services.Customer.Common;

namespace SalesHub.Unit.Tests.Customer.Queries;

public class GetCustomerQueryHandlerTests 
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository = new Mock<ICustomerRepository>();

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCustomerNotFound()
    {
        var handler = new GetCustomerQueryHandler(_mockCustomerRepository.Object);
        var result = await handler.Handle(new GetCustomerQuery("john@doe.com"), It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<GetCustomerResult>>();
        result.FirstError.Code.ShouldBe("Customer.NotFound");
    }

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenEmailMatchedInRepository()
    {
        _mockCustomerRepository.Setup(m => m.GetByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john@doe.com"
            });
        
        var handler = new GetCustomerQueryHandler(_mockCustomerRepository.Object);
        var result = await handler.Handle(new GetCustomerQuery("john@doe.com"), It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<GetCustomerResult>>();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.FirstName.ShouldBe("John");
        result.Value.LastName.ShouldBe("Doe");
        result.Value.Phone.ShouldBe("1234567890");
        result.Value.Email.ShouldBe("john@doe.com");
    }
}
