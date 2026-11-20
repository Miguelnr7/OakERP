using OakERP.Domain.Customers;

namespace OakERP.Api.Contracts.Customers;

public static class CustomerMappings
{
    public static CustomerResponse ToResponse(this Customer customer) =>
        new(customer.Id, customer.Name, customer.TaxId, customer.Email, customer.CreatedAtUtc);
    
}