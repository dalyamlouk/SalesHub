using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces;
using SalesHub.Application.Services.Customer.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Customer.Queries.Get;

public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, ErrorOr<GetCustomerResult>>
{
    private readonly ICustomerRepository _customerRepository;

    public GetCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<GetCustomerResult>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        if(await _customerRepository.GetByEmailAsync(request.Email) is Domain.Entities.Customer customer) 
        {
            return new GetCustomerResult(customer.Id, customer.FirstName, customer.LastName, customer.Phone, customer.Email);
        }

        return Errors.Customer.NotFound(request.Email);
    }
}