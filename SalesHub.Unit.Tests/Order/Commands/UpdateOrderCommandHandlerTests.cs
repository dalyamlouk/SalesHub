using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Order.Commands.Update;
using SalesHub.Application.Order.Update;
using SalesHub.Applications.Order.Common;

namespace SalesHub.Unit.Tests.Order.Commands;

public class UpdateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepository = new Mock<IOrderRepository>();

    [Fact]
    public async Task Handle_ShouldReturnOrder_WhenOrderIsUpdated()
    {
        var orderId = Guid.NewGuid();
        var command = new UpdateOrderCommand(
            orderId,
            1,
            new DateTime(2021, 1, 1)
        );

        _mockOrderRepository.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Order
            {
                Id = orderId,
                ProductId = It.IsAny<Guid>(),
                CustomerId = It.IsAny<Guid>(),
                Status = command.Status,
                CreatedDate = It.IsAny<DateTime>(),
                UpdatedDate = command.UpdatedDate
            });

        var handler = new UpdateOrderCommandHandler(_mockOrderRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<UpdateOrderResult>>();
        result.Value.Id.ShouldBe(orderId);
        result.Value.Status.ShouldBe(command.Status);
        result.Value.UpdatedDate.ShouldBe(command.UpdatedDate);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFoundError_WhenOrderDoesNotExist()
    {
        var orderId = Guid.NewGuid();
        var command = new UpdateOrderCommand(
            orderId,
            1,
            new DateTime(2021, 1, 1)
        );

        _mockOrderRepository.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Order)null);

        var handler = new UpdateOrderCommandHandler(_mockOrderRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Errors.Count().ShouldBe(1);
        result.FirstError.Code.ShouldBe("Order.NotFound");
    }
}
