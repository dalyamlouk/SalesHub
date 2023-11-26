using SalesHub.Application.Common.Interfaces;
using SalesHub.Application.Product.Queries.Get;
using SalesHub.Application.Services.Product.Common;

namespace SalesHub.Unit.Tests.Product.Queries;

public class GetProductQueryHandlerTests 
{
    private readonly Mock<IProductRepository> _mockProductRepository = new Mock<IProductRepository>();

    [Fact]
    public async Task Handle_ShouldReturnError_WhenProductNotFound()
    {
        var handler = new GetProductQueryHandler(_mockProductRepository.Object);
        var result = await handler.Handle(new GetProductQuery("1234567890"), It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<GetProductResult>>();
        result.FirstError.Code.ShouldBe("Product.NotFound");
    }

    [Fact]
    public async Task Handle_ShouldReturnProduct_WhenSkuMatchedInRepository()
    {
        _mockProductRepository.Setup(m => m.GetBySkuAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Domain.Entities.Product
            {
                Id = Guid.NewGuid(),
                Name = "Product",
                Description = "Product Description",
                SKU = "1234567890",
            });
        
        var handler = new GetProductQueryHandler(_mockProductRepository.Object);
        var result = await handler.Handle(new GetProductQuery("1234567890"), It.IsAny<CancellationToken>());

        result.ShouldBeOfType<ErrorOr<GetProductResult>>();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.Name.ShouldBe("Product");
        result.Value.Description.ShouldBe("Product Description");
        result.Value.SKU.ShouldBe("1234567890");
    }
}
