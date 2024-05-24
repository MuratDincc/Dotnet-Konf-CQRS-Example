namespace Data.MongoDB.Entities;

public class Transaction : BaseEntity
{
    public long TransactionId { get; set; }
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