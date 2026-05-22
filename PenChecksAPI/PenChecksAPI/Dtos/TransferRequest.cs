using System.ComponentModel.DataAnnotations;
namespace PenChecksAPI.Dtos;

public class TransferRequest
{
    [Required] public Guid FromAccountId { get; set; }
    [Required] public Guid ToAccountId { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive.")]
    public decimal Amount { get; set; }
}