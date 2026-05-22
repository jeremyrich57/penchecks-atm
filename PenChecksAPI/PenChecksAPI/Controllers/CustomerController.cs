using Microsoft.AspNetCore.Mvc;
using PenChecksAPI.Dtos;
using PenChecksAPI.Models;
using PenChecksAPI.Services;

namespace PenChecksAPI.Controllers;

[ApiController]
[Route("api/customer")]
public class CustomerController : ControllerBase
{
    private readonly AtmService _atm;

    public CustomerController(AtmService atm) => _atm = atm;

    // NOTE: normally in a real app this would take an id after successful login, but for this assignment we only have one customer and no login

    // GET /api/customer
    [HttpGet]
    public async Task<ActionResult<CustomerDto>> GetCustomer()
    {
        var customer = await _atm.GetCustomerAsync();
        if (customer is null) return NotFound();

        return Ok(ToDto(customer));
    }

    private static CustomerDto ToDto(Customer c) =>
        new(c.Id, c.Name, c.Accounts.Select(ToDto).ToList());

    private static AccountDto ToDto(Account a) =>
        new(a.Id, a.Name, a.Balance);
}
