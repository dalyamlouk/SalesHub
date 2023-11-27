using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Order.Commands.Create;
using SalesHub.Application.Order.Common;
using SalesHub.Application.Order.Create;

namespace SalesHub.Unit.Tests.Order.Commands;

public class CreateOrderCommandHandlerTests
{
    private readonly Mock<IOrderRepository> _mockOrderRepository = new Mock<IOrderRepository>();

    [Fact]
    public async Task Handle_ShouldReturnCreateOrderResult_WhenOrderIsCreated()
    {
        var productId = Guid.NewGuid();
        var customerId = Guid.NewGuid();

        var command = new CreateOrderCommand
        (
            productId,
            customerId,
            1
        );

        _mockOrderRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Order>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Order
            {
                Id = Guid.NewGuid(),
                ProductId = productId,
                CustomerId = customerId,
                Status = 1,
                CreatedDate = It.IsAny<DateTime>(),
                UpdatedDate = It.IsAny<DateTime>()
            });

        var handler = new CreateOrderCommandHandler(_mockOrderRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<CreateOrderResult>>();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.ProductId.ShouldBe(command.ProductId);
        result.Value.CustomerId.ShouldBe(command.CustomerId);
        result.Value.Status.ShouldBe(command.Status);
    }
}