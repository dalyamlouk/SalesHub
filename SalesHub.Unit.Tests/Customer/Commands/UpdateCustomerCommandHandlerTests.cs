using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Application.Customer.Commands.Update;
using SalesHub.Application.Customer.Update;
using SalesHub.Applications.Customer.Common;

namespace SalesHub.Unit.Tests.Customer.Commands;

public class UpdateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository = new Mock<ICustomerRepository>();

    [Fact]
    public async Task Handle_ShouldReturnCustomer_WhenCustomerIsUpdated()
    {
        var command = new UpdateCustomerCommand(
            Guid.NewGuid(),
            "John",
            "Doe",
            "123456789",
            "john@doe.com"
        );

        _mockCustomerRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Customer
            {
                Id = command.Id,
                FirstName = command.FirstName,
                LastName = command.LastName,
                Phone = command.Phone,
                Email = command.Email
            });

        var handler = new UpdateCustomerCommandHandler(_mockCustomerRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<UpdateCustomerResult>>();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.FirstName.ShouldBe(command.FirstName);
        result.Value.LastName.ShouldBe(command.LastName);
        result.Value.Phone.ShouldBe(command.Phone);
        result.Value.Email.ShouldBe(command.Email);

    }

    [Fact]
    public async Task Handle_ShouldReturnNotFoundError_WhenCustomerDoesNotExist()
    {
        var command = new UpdateCustomerCommand(
            Guid.NewGuid(),
            "John",
            "Doe",
            "123456789",
            "john@doe.com"
        );

        _mockCustomerRepository.Setup(x => x.UpdateAsync(It.IsAny<Domain.Entities.Customer>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Customer)null);

        var handler = new UpdateCustomerCommandHandler(_mockCustomerRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Errors.Count().ShouldBe(1);
        result.FirstError.Code.ShouldBe("Customer.NotFound");
    }
}
