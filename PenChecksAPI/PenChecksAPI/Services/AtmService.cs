using Microsoft.EntityFrameworkCore;
using PenChecksAPI.Data;
using PenChecksAPI.Models;

namespace PenChecksAPI.Services;

public class AtmService
{
    private readonly AtmDbContext _db;

    public AtmService(AtmDbContext db) => _db = db;

    public Task<Customer?> GetCustomerAsync() =>
        _db.Customers
            .Include(c => c.Accounts)
            .FirstOrDefaultAsync();

    public Task<List<Account>> GetAccountsAsync() =>
        _db.Accounts.ToListAsync();

    public Task<Account?> GetAccountByIdAsync(Guid id) =>
        _db.Accounts.FirstOrDefaultAsync(a => a.Id == id);

    public Task<List<Transaction>> GetTransactionsAsync(Guid? accountId = null)
    {
        var query = _db.Transactions.AsQueryable();

        if (accountId.HasValue)
            query = query.Where(t =>
                t.AccountId == accountId.Value ||
                t.ToAccountId == accountId.Value);

        return query
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();
    }

    public async Task<Account> DepositAsync(Guid accountId, decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Amount must be positive.");

        var account = await _db.Accounts.FirstOrDefaultAsync(a => a.Id == accountId)
            ?? throw new KeyNotFoundException("Account not found.");

        account.Balance += amount;

        _db.Transactions.Add(new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Deposit,
            Amount = amount,
            AccountId = account.Id,
            Timestamp = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return account;
    }

    public async Task<Account> WithdrawAsync(Guid accountId, decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Amount must be positive.");

        var account = await _db.Accounts.FirstOrDefaultAsync(a => a.Id == accountId)
            ?? throw new KeyNotFoundException("Account not found.");

        if (account.Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");

        account.Balance -= amount;

        _db.Transactions.Add(new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Withdraw,
            Amount = amount,
            AccountId = account.Id,
            Timestamp = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return account;
    }

    public async Task<(Account From, Account To)> TransferAsync(
        Guid fromId, Guid toId, decimal amount)
    {
        if (amount <= 0)
            throw new InvalidOperationException("Amount must be positive.");

        if (fromId == toId)
            throw new InvalidOperationException("Cannot transfer to the same account.");

        var from = await _db.Accounts.FirstOrDefaultAsync(a => a.Id == fromId)
            ?? throw new KeyNotFoundException("Source account not found.");

        var to = await _db.Accounts.FirstOrDefaultAsync(a => a.Id == toId)
            ?? throw new KeyNotFoundException("Destination account not found.");

        if (from.Balance < amount)
            throw new InvalidOperationException("Insufficient funds.");

        from.Balance -= amount;
        to.Balance += amount;

        _db.Transactions.Add(new Transaction
        {
            Id = Guid.NewGuid(),
            Type = TransactionType.Transfer,
            Amount = amount,
            AccountId = from.Id,
            ToAccountId = to.Id,
            Timestamp = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();
        return (from, to);
    }
}