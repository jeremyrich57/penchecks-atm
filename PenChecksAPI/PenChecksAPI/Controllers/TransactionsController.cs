using Microsoft.AspNetCore.Mvc;
using PenChecksAPI.Dtos;
using PenChecksAPI.Models;
using PenChecksAPI.Services;

namespace PenChecksAPI.Controllers;

[ApiController]
[Route("api/transactions")]
public class TransactionsController : ControllerBase
{
    private readonly AtmService _atm;
    public TransactionsController(AtmService atm) => _atm = atm;

    // GET /api/transactions
    // GET /api/transactions?accountId={guid}
    [HttpGet]
    public async Task<ActionResult<List<TransactionDto>>> GetAll([FromQuery] Guid? accountId)
    {
        var transactions = await _atm.GetTransactionsAsync(accountId);

        // Build a name lookup so we can populate accountName / toAccountName.
        var names = (await _atm.GetAccountsAsync())
            .ToDictionary(a => a.Id, a => a.Name);

        var result = transactions
            .Select(t => ToDto(t, names))
            .ToList();

        return Ok(result);
    }

    private static TransactionDto ToDto(Transaction t, IDictionary<Guid, string> names) =>
        new(
            t.Id,
            t.Type,
            t.Amount,
            t.AccountId,
            names.TryGetValue(t.AccountId, out var n) ? n : "",
            t.ToAccountId,
            t.ToAccountId.HasValue && names.TryGetValue(t.ToAccountId.Value, out var tn) ? tn : null,
            t.Timestamp);
}