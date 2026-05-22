using Microsoft.AspNetCore.Mvc;
using PenChecksAPI.Dtos;
using PenChecksAPI.Models;
using PenChecksAPI.Services;

namespace PenChecksAPI.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountsController : ControllerBase
{
    private readonly AtmService _atm;
    public AccountsController(AtmService atm) => _atm = atm;

    // GET /api/accounts
    [HttpGet]
    public async Task<ActionResult<List<AccountDto>>> GetAll()
    {
        var accounts = await _atm.GetAccountsAsync();
        return Ok(accounts.Select(ToDto).ToList());
    }

    // GET /api/accounts/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<AccountDto>> GetById(Guid id)
    {
        var account = await _atm.GetAccountByIdAsync(id);
        if (account is null) return NotFound();
        return Ok(ToDto(account));
    }

    // POST /api/accounts/{id}/deposit
    [HttpPost("{id:guid}/deposit")]
    public async Task<ActionResult<AccountDto>> Deposit(Guid id, AmountRequest req)
    {
        try
        {
            var account = await _atm.DepositAsync(id, req.Amount);
            return Ok(ToDto(account));
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
    }

    // POST /api/accounts/{id}/withdraw
    [HttpPost("{id:guid}/withdraw")]
    public async Task<ActionResult<AccountDto>> Withdraw(Guid id, AmountRequest req)
    {
        try
        {
            var account = await _atm.WithdrawAsync(id, req.Amount);
            return Ok(ToDto(account));
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
    }

    private static AccountDto ToDto(Account a) => new(a.Id, a.Name, a.Balance);
}