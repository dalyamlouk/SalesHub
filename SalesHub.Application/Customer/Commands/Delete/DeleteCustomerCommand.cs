using ErrorOr;
using MediatR;
using SalesHub.Applications.Customer.Common;

namespace SalesHub.Application.Customer.Commands.Delete
{
    public record DeleteCustomerCommand(Guid Id) : IRequest<ErrorOr<DeleteCustomerResult>>;
}