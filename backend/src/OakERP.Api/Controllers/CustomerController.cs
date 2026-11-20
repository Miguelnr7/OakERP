using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OakERP.Api.Contracts.Customers;
using OakERP.Domain.Customers;
using OakERP.Infrastructure.Persistence;

namespace OakERP.Api.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CustomersController(OakErpDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<CustomerResponse>>> GetAll(CancellationToken ct)
    {
        var customer = await db.Customers
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .ToListAsync(ct);
        return Ok(customer.Select(c => c.ToResponse()).ToList());
    }
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CustomerResponse>> GetById(Guid id, CancellationToken ct)
    {
        var customer = await db.Customers
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id, ct);
        if (customer is null) return NotFound();

        return Ok(customer.ToResponse());
    }

    [HttpPost]
    public async Task<ActionResult<CustomerResponse>> Create(
        CreateCustomerRequest request, CancellationToken ct)
    {
        var customer = new Customer
        {
            Name = request.Name.Trim(),
            TaxId = request.TaxId?.Trim(),
            Email = request.Email?.Trim()
        };
        db.Customers.Add(customer);
        await db.SaveChangesAsync(ct);
        return CreatedAtAction(nameof(GetById), new { id = customer.Id }, customer.ToResponse());
    }
 [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id, UpdateCustomerRequest request, CancellationToken ct)
    {
        var customer = await db.Customers.FirstOrDefaultAsync(c => c.Id == id, ct);

        if (customer is null) return NotFound();

        customer.Name = request.Name.Trim();
        customer.TaxId = request.TaxId?.Trim();
        customer.Email = request.Email?.Trim();

        await db.SaveChangesAsync(ct);

        return NoContent();
    }

     [HttpDelete("{id:guid}")]
    // DELETE /api/customers/{id}.
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var customer = await db.Customers.FirstOrDefaultAsync(c => c.Id == id, ct);
        if (customer is null) return NotFound();

        db.Customers.Remove(customer);
        // Marca el objeto para borrar.

        await db.SaveChangesAsync(ct);
        // Se ejecuta el DELETE.

        return NoContent();
    }
}