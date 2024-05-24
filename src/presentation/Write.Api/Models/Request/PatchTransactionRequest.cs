namespace Write.Api.Models.Request;

public class PatchTransactionRequest
{
    public long FromWalletId { get; init; }
    public long ToWalletId { get; init; }
    public string CardNumber { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public decimal Total { get; init; }
}