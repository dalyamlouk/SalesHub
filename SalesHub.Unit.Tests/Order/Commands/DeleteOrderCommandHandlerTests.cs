using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Order.Commands.Delete;

namespace SalesHub.Unit.Tests.Customer.Commands;

public class DeleteOrderCommandHandlerTests {

    private readonly Mock<IOrderRepository> _mockOrderRepository = new Mock<IOrderRepository>();

    [Fact]
    public async Task Handle_WhenOrderExists_ReturnsSuccess() {

        _mockOrderRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new DeleteOrderCommandHandler(_mockOrderRepository.Object);
        var command = new DeleteOrderCommand(Guid.NewGuid());

        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Value.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_WhenOrderDoesNotExist_ReturnsUnsuccessful() {

        var handler = new DeleteOrderCommandHandler(_mockOrderRepository.Object);
        var command = new DeleteOrderCommand(Guid.NewGuid());

        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Value.Success.ShouldBeFalse();
    }
}