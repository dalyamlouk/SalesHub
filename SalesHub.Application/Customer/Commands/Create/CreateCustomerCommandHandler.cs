using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Application.Customer.Commands.Create;
using SalesHub.Application.Customer.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Customer.Create;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, ErrorOr<CreateCustomerResult>>
{
    private readonly ICustomerRepository _customerRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository){
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<CreateCustomerResult>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingCustomer = await _customerRepository.GetByEmailAsync(request.Email);

        if (existingCustomer is not null) 
        {
            return Errors.Customer.AlreadyExists(request.Email);
        }

        var customer = new Domain.Entities.Customer { 
            FirstName = request.FirstName, 
            LastName = request.LastName, 
            Phone = request.Phone, 
            Email = request.Email
        };

        var createdCustomer = await _customerRepository.CreateAsync(customer);
  
        return new CreateCustomerResult(
            createdCustomer.Id,
            createdCustomer.FirstName, 
            createdCustomer.LastName,
            createdCustomer.Phone,
            createdCustomer.Email
        );
    }
}