using ErrorOr;
using MediatR;
using SalesHub.Application.Services.Customer.Common;

namespace SalesHub.Application.Customer.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<CreateCustomerResult>>
{

    public async Task<ErrorOr<CreateCustomerResult>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Domain.Entities.Customer { 
            FirstName = request.FirstName, 
            LastName = request.LastName, 
            Phone = request.Phone, 
            Email = request.Email
        };

        
        return new CreateCustomerResult(
            customer.Id,
            customer.FirstName, 
            customer.LastName,
            customer.Phone,
            customer.Email
        );
    }
}