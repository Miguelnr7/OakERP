namespace OakERP.Api.Contracts.Customers;

public sealed record CustomerResponse(
    Guid Id,
    string Name,
    string? TaxId,
    string? Email,
    DateTime CreatedAtUtc);
