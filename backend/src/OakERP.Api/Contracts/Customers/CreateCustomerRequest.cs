using System.ComponentModel.DataAnnotations;

namespace OakERP.Api.Contracts.Customers;

public sealed record CreateCustomerRequest(
    [Required, StringLength(200)] string Name,
    
    [StringLength(20)] string? TaxId,
   
    [EmailAddress, StringLength(254)] string? Email);
   