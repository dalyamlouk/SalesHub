using ErrorOr;
using Moq;
using SalesHub.Application.Common.Interfaces;
using SalesHub.Application.Customer.Commands.Create;
using SalesHub.Application.Customer.Common;
using SalesHub.Application.Customer.Create;
using Shouldly;

namespace SalesHub.Unit.Tests.Customer.Commands;

public class CreateCustomerCommandHandlerTests
{
    private readonly Mock<ICustomerRepository> _mockCustomerRepository = new Mock<ICustomerRepository>();

    [Fact]
    public async Task Handle_ShouldReturnCreateCustomerResult_WhenCustomerIsCreated()
    {
        var command = new CreateCustomerCommand
        (
            "John",
            "Doe",
            "1234567890",
            "john@doe.com"
        );

        _mockCustomerRepository.Setup(x => x.CreateAsync(It.IsAny<Domain.Entities.Customer>(), new CancellationToken()))
            .ReturnsAsync(new Domain.Entities.Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john@doe.com"
            });

        var handler = new CreateCustomerCommandHandler(_mockCustomerRepository.Object);
        var result = await handler.Handle(command, new CancellationToken());

        result.ShouldBeOfType<ErrorOr<CreateCustomerResult>>();
        result.Value.Id.ShouldNotBe(Guid.Empty);
        result.Value.FirstName.ShouldBe(command.FirstName);
        result.Value.LastName.ShouldBe(command.LastName);
        result.Value.Phone.ShouldBe(command.Phone);
        result.Value.Email.ShouldBe(command.Email);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenCustomerWithTheSameEmailAlreadyExists()
    {
        var command = new CreateCustomerCommand
        (
            "John",
            "Doe",
            "1234567890",
            "john@doe.com"
        );

        _mockCustomerRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>(), new CancellationToken()))
            .ReturnsAsync(new Domain.Entities.Customer
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Phone = "1234567890",
                Email = "john@doe.com"
            });

        var handler = new CreateCustomerCommandHandler(_mockCustomerRepository.Object);
        var result = await handler.Handle(command, new CancellationToken());

        result.Errors.Count().ShouldBe(1);
        result.FirstError.Code.ShouldBe("Customer.AlreadyExists");
    }
}