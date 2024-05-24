namespace Data.PostgreSQL.Entities;

public class Transaction
{
    public long Id { get; set; }
    public long FromWalletId { get; set; }
    public long ToWalletId { get; set; }
    public string CardNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public decimal Total { get; set; }
    public DateTimeOffset CreatedOnUtc { get; set; }
    public DateTimeOffset? UpdatedOnUtc { get; set; }
}