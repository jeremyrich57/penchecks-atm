namespace PenChecksAPI.Dtos;

public record CustomerDto(Guid Id, string Name, List<AccountDto> Accounts);