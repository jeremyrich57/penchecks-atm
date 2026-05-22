using PenChecksAPI.Models;

namespace PenChecksAPI.Dtos;

public record TransactionDto(
    Guid Id,
    TransactionType Type,
    decimal Amount,
    Guid AccountId,
    string AccountName,
    Guid? ToAccountId,
    string? ToAccountName,
    DateTime Timestamp);