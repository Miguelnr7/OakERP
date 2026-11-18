using Microsoft.EntityFrameworkCore;
using OakERP.Domain.Customers;

namespace OakERP.Infrastructure.Persistence;

public class OakErpDbContext(DbContextOptions<OakErpDbContext> options) : DbContext(options){
    public DbSet<Customer> Customers => Set<Customer>();

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customers");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).HasMaxLength(200).IsRequired();
            entity.Property(c => c.TaxId).HasMaxLength(20);
            entity.Property(c => c.Email).HasMaxLength(254);
        });
    }
}

