using SalesHub.Application.Common.Interfaces.Product;
using SalesHub.Application.Product.Commands.Update;
using SalesHub.Application.Product.Update;
using SalesHub.Applications.Product.Common;

namespace SalesHub.Unit.Tests.Product.Commands;

public class UpdateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();

    [Fact]
    public async Task Handle_ShouldReturnProduct_WhenProductIsUpdated()
    {
        var command = new UpdateProductCommand(
            Guid.NewGuid(),
            "Product",
            "Product Description",
            "123456789"
        );

        _mockProductRepository.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(),  It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Product
            {
                Id = command.Id,
                Name = command.Name,
                Description = command.Description,
                SKU = command.SKU,
            });

        var handler = new UpdateProductCommandHandler(_mockProductRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<UpdateProductResult>>();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.Name.ShouldBe(command.Name);
        result.Value.Description.ShouldBe(command.Description);
        result.Value.SKU.ShouldBe(command.SKU);
    }

    [Fact]
    public async Task Handle_ShouldReturnNotFoundError_WhenProductDoesNotExist()
    {
        var command = new UpdateProductCommand(
            Guid.NewGuid(),
            "Product",
            "Product Description",
            "123456789"
        );

        _mockProductRepository.Setup(x => x.UpdateAsync(It.IsAny<Guid>(), It.IsAny<string>(), It.IsAny<string>(),  It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Entities.Product)null);

        var handler = new UpdateProductCommandHandler(_mockProductRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Errors.Count().ShouldBe(1);
        result.FirstError.Code.ShouldBe("Product.NotFound");
    }
}
