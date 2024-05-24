using System.Text.Json.Serialization;

namespace Consumers.Data.Sync.Models;

public class TransactionMessageModel
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("from_wallet_id")]
    public long FromWalletId { get; set; }
    
    [JsonPropertyName("to_wallet_id")]
    public long ToWalletId { get; set; }
    
    [JsonPropertyName("card_number")]
    public string CardNumber { get; set; }
    
    [JsonPropertyName("first_name")]
    public string FirstName { get; set; }
    
    [JsonPropertyName("last_name")]
    public string LastName { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("total")]
    public decimal Total { get; set; }
    
    [JsonPropertyName("created_on_utc")]
    public DateTimeOffset CreatedOnUtc { get; set; }
    
    [JsonPropertyName("updated_on_utc")]
    public DateTimeOffset? UpdatedOnUtc { get; set; }
}
