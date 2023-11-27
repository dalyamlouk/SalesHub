using SalesHub.Application.Common.Interfaces.Order;
using SalesHub.Application.Order.Common;
using SalesHub.Application.Order.Queries.Get;

namespace SalesHub.Unit.Tests.Order.Queries;

public class GetOrderQueryHandlerTests 
{
    private readonly Mock<IOrderRepository> _mockOrderRepository = new Mock<IOrderRepository>();

    [Fact]
    public async Task Handle_ShouldReturnError_WhenOrderNotFound()
    {
        var handler = new GetOrderQueryHandler(_mockOrderRepository.Object);
        var result = await handler.Handle(new GetOrderQuery(It.IsAny<Guid>()), It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<GetOrderResult>>();
        result.FirstError.Code.ShouldBe("Order.NotFound");
    }

    [Fact]
    public async Task Handle_ShouldReturnOrder_WhenIdMatchedInRepository()
    {
        var orderId = Guid.NewGuid();

        _mockOrderRepository.Setup(m => m.GetById(orderId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Order
            {
                Id = orderId,
                ProductId = It.IsAny<Guid>(),
                CustomerId = It.IsAny<Guid>(),
                Status = 1,
                CreatedDate = It.IsAny<DateTime>(),
                UpdatedDate = It.IsAny<DateTime>()
            });
        
        var handler = new GetOrderQueryHandler(_mockOrderRepository.Object);
        var result = await handler.Handle(new GetOrderQuery(orderId), It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<GetOrderResult>>();
        result.Value.Id.ShouldBe(orderId);
    }
}
