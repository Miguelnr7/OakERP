namespace OakERP.Domain.Customers;

public class Customer{
    public Guid Id { get; private set; } = Guid.CreateVersion7();
    public required string Name { get; set; }
    public string? TaxId { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;
}