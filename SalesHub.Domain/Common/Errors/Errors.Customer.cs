using ErrorOr;

namespace SalesHub.Domain.Common.Errors;

public static class Errors 
{
    public static class Customer
    {
        public static Error AlreadyExists(string email) =>
            Error.Conflict(code: "Customer.AlreadyExists",
                           description: $"Customer with email {email} already exists.");
    }
}