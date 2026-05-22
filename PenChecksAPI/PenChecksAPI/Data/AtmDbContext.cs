using Microsoft.EntityFrameworkCore;
using PenChecksAPI.Models;

namespace PenChecksAPI.Data;

public class AtmDbContext : DbContext
{
    public AtmDbContext(DbContextOptions<AtmDbContext> options)
        : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Account> Accounts => Set<Account>();
    public DbSet<Transaction> Transactions => Set<Transaction>();

    // Seed initial data for testing
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var customerId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
        var checkingId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var savingsId = Guid.Parse("22222222-2222-2222-2222-222222222222");

        modelBuilder.Entity<Customer>().HasData(
            new Customer { Id = customerId, Name = "Luke Skywalker" }
        );

        modelBuilder.Entity<Account>().HasData(
            new Account { Id = checkingId, Name = "Checking", Balance = 243.65m, CustomerId = customerId },
            new Account { Id = savingsId, Name = "Savings", Balance = 1000m, CustomerId = customerId }
        );
    }
}