using Microsoft.AspNetCore.Mvc;
using PenChecksAPI.Dtos;
using PenChecksAPI.Models;
using PenChecksAPI.Services;

namespace PenChecksAPI.Controllers;

[ApiController]
[Route("api/transfers")]
public class TransfersController : ControllerBase
{
    private readonly AtmService _atm;
    public TransfersController(AtmService atm) => _atm = atm;

    // POST /api/transfers
    [HttpPost]
    public async Task<ActionResult<TransferResponse>> Transfer(TransferRequest req)
    {
        try
        {
            var (from, to) = await _atm.TransferAsync(
                req.FromAccountId, req.ToAccountId, req.Amount);

            return Ok(new TransferResponse(ToDto(from), ToDto(to)));
        }
        catch (KeyNotFoundException ex) { return NotFound(new { message = ex.Message }); }
        catch (InvalidOperationException ex) { return BadRequest(new { message = ex.Message }); }
    }

    private static AccountDto ToDto(Account a) => new(a.Id, a.Name, a.Balance);
}