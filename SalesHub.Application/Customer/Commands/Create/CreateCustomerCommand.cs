using ErrorOr;
using MediatR;
using SalesHub.Application.Services.Customer.Common;

namespace SalesHub.Application.Customer.Create;

public record CreateCustomerCommand(string FirstName,
                            string LastName,
                            string Phone,
                            string Email) : IRequest<ErrorOr<CreateCustomerResult>>;