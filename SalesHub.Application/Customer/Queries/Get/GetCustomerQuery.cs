using ErrorOr;
using MediatR;
using SalesHub.Application.Services.Customer.Common;

namespace SalesHub.Application.Customer.Queries.Get;

public record GetCustomerQuery(string Email) : IRequest<ErrorOr<GetCustomerResult>>;