using System.ComponentModel.DataAnnotations;
namespace PenChecksAPI.Dtos;

public class AmountRequest
{
    [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be positive.")]
    public decimal Amount { get; set; }
}