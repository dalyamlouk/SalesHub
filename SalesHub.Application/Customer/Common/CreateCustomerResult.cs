namespace SalesHub.Application.Services.Customer.Common
{
    public record CreateCustomerResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Phone,
        string Email);
}