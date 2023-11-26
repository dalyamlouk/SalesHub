using SalesHub.Application.Common.Interfaces;
using SalesHub.Application.Customer.Commands.Delete;

namespace SalesHub.Unit.Tests.Customer.Commands;

public class DeleteCustomerCommandHandlerTests {

    private Mock<ICustomerRepository> _mockCustomerRepository = new Mock<ICustomerRepository>();

    [Fact]
    public async Task Handle_WhenCustomerExists_ReturnsSuccess() {

        _mockCustomerRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new DeleteCustomerCommandHandler(_mockCustomerRepository.Object);
        var command = new DeleteCustomerCommand(Guid.NewGuid());

        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Value.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_WhenCustomerDoesNotExist_ReturnsUnsuccessful() {

        var handler = new DeleteCustomerCommandHandler(_mockCustomerRepository.Object);
        var command = new DeleteCustomerCommand(Guid.NewGuid());

        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Value.Success.ShouldBeFalse();
    }
}