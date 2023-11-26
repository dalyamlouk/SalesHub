using ErrorOr;
using MediatR;
using SalesHub.Application.Services.Customer.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Customer.Queries.Get;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, ErrorOr<GetCustomerResult>>
{
    public async Task<ErrorOr<GetCustomerResult>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        return await Task.FromResult(Errors.Customer.NotFound(request.Email));
    }
}