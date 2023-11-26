using SalesHub.Application.Common.Interfaces;
using SalesHub.Application.Product.Commands.Create;
using SalesHub.Application.Product.Common;
using SalesHub.Application.Product.Create;

namespace SalesHub.Unit.Tests.Product.Commands;

public class CreateProductCommandHandlerTests
{
    private readonly Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();

    [Fact]
    public async Task Handle_ShouldReturnCreateProductResult_WhenProductIsCreated()
    {
        var command = new CreateProductCommand
        (
            "Product",
            "Product Description",
            "1234566789"
        );

        _mockProductRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Product>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = "Product",
                Description = "Product Description",
                SKU = "1234566789"
            });

        var handler = new CreateProductCommandHandler(_mockProductRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<CreateProductResult>>();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.Name.ShouldBe(command.Name);
        result.Value.Description.ShouldBe(command.Description);
        result.Value.SKU.ShouldBe(command.SKU);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenProductWithTheSameSkuAlreadyExists()
    {
        var command = new CreateProductCommand
        (
            "Product",
            "Product Description",
            "1234566789"
        );

        _mockProductRepository.Setup(x => x.GetBySkuAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = "Product",
                Description = "Product Description",
                SKU = "1234566789"
            });

        var handler = new CreateProductCommandHandler(_mockProductRepository.Object);
        var result = await handler.Handle(command, It.IsAny<CancellationToken>());

        result.Errors.Count().ShouldBe(1);
        result.FirstError.Code.ShouldBe("Product.AlreadyExists");
    }
}