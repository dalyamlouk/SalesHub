namespace SalesHub.Application.Services.Customer.Common
{
    public record GetCustomerResult(
        Guid Id,
        string FirstName,
        string LastName,
        string Phone,
        string Email);
}