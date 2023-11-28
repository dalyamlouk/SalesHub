using System.Security.Cryptography;
using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces.Customer;
using SalesHub.Application.Customer.Commands.Update;
using SalesHub.Applications.Customer.Common;
using SalesHub.Domain.Common.Errors;

namespace SalesHub.Application.Customer.Update;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, ErrorOr<UpdateCustomerResult>>
{
    private readonly ICustomerRepository _customerRepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerRepository){
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<UpdateCustomerResult>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var updatedCustomer = await _customerRepository.UpdateAsync(
            request.Id, request.FirstName, request.LastName, request.Phone, request.Email, cancellationToken);

        if(updatedCustomer is null)
        {
            return Errors.Customer.NotFound(request.Id);
        }

        return new UpdateCustomerResult(
            updatedCustomer.Id,
            updatedCustomer.FirstName, 
            updatedCustomer.LastName,
            updatedCustomer.Phone,
            updatedCustomer.Email
        );
    }
}