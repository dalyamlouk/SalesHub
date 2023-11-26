using ErrorOr;
using MediatR;
using SalesHub.Applications.Customer.Common;

namespace SalesHub.Application.Customer.Commands.Update;

public record UpdateCustomerCommand(Guid Id,
                            string FirstName,
                            string LastName,
                            string Phone,
                            string Email) : IRequest<ErrorOr<UpdateCustomerResult>>;