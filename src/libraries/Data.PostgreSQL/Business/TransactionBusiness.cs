using Data.PostgreSQL.Business.Abstracts;
using Data.PostgreSQL.Business.Dtos;

namespace Data.PostgreSQL.Business;

public class TransactionBusiness : ITransactionBusiness
{
    private readonly TransactionDbContext _dbContext;
    
    public TransactionBusiness(TransactionDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<long> Create(CreateTransactionDto request)
    {
        var transaction = new Entities.Transaction
        {
            FromWalletId = request.FromWalletId,
            ToWalletId = request.ToWalletId,
            CardNumber = request.CardNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Total = request.Total,
            CreatedOnUtc = DateTimeOffset.UtcNow
        };
        
        _dbContext.Transaction.Add(transaction);
        
        await _dbContext.SaveChangesAsync();

        return transaction.Id;
    }
    
    public async Task Update(long id, UpdateTransactionDto request)
    {
        var transaction = await _dbContext.Transaction.FindAsync(id);
        if (transaction == null)
            throw new Exception("Transaction not found");

        transaction.FromWalletId = request.FromWalletId;
        transaction.ToWalletId = request.ToWalletId;
        transaction.CardNumber = request.CardNumber;
        transaction.FirstName = request.FirstName;
        transaction.LastName = request.LastName;
        transaction.Email = request.Email;
        transaction.Total = request.Total;
        
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task Delete(long id)
    {
        var transaction = await _dbContext.Transaction.FindAsync(id);
        if (transaction == null)
            throw new Exception("Transaction not found");
        
        _dbContext.Transaction.Remove(transaction);
        
        await _dbContext.SaveChangesAsync();
    }
}