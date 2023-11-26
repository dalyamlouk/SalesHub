using ErrorOr;
using MediatR;
using SalesHub.Application.Customer.Common;

namespace SalesHub.Application.Customer.Commands.Create;

public record CreateCustomerCommand(string FirstName,
                            string LastName,
                            string Phone,
                            string Email) : IRequest<ErrorOr<CreateCustomerResult>>;