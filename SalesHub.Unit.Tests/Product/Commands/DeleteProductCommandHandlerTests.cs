using SalesHub.Application.Common.Interfaces.Product;
using SalesHub.Application.Product.Commands.Delete;

namespace SalesHub.Unit.Tests.Customer.Commands;

public class DeleteProductCommandHandlerTests {

    private readonly Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();

    [Fact]
    public async Task Handle_WhenProductExists_ReturnsSuccess() {

        _mockProductRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new DeleteProductCommandHandler(_mockProductRepository.Object);
        var command = new DeleteProductCommand(Guid.NewGuid());

        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Value.Success.ShouldBeTrue();
    }

    [Fact]
    public async Task Handle_WhenProductDoesNotExist_ReturnsUnsuccessful() {

        var handler = new DeleteProductCommandHandler(_mockProductRepository.Object);
        var command = new DeleteProductCommand(Guid.NewGuid());

        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Value.Success.ShouldBeFalse();
    }
}