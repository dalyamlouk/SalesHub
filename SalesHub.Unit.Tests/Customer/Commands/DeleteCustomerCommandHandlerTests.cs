using Moq;
using SalesHub.Application.Common.Interfaces;
using SalesHub.Application.Customer.Commands.Delete;
using Shouldly;

namespace SalesHub.Unit.Tests.Customer.Commands;

public class DeleteCustomerCommandHandlerTests {

    private Mock<ICustomerRepository> _mockCustomerRepository = new Mock<ICustomerRepository>();

    [Fact]
    public async Task Handle_WhenCustomerExists_ReturnsSuccess() {

        _mockCustomerRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new DeleteCustomerCommandHandler(_mockCustomerRepository.Object);
        var command = new DeleteCustomerCommand(Guid.NewGuid());

        var result = await handler.Handle(command, CancellationToken.None);

        result.Value.Success.ShouldBeTrue();
    }
}