using ErrorOr;
using MediatR;
using SalesHub.Application.Common.Interfaces;
using SalesHub.Applications.Customer.Common;

namespace SalesHub.Application.Customer.Commands.Delete;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, ErrorOr<DeleteCustomerResult>>
{
    private readonly ICustomerRepository _customerRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ErrorOr<DeleteCustomerResult>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var result = await _customerRepository.DeleteAsync(request.Id, cancellationToken);

        return new DeleteCustomerResult(result);
    }
}