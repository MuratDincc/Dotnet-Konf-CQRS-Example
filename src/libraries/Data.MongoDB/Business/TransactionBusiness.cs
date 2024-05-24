using Data.MongoDB.Business.Abstracts;
using Data.MongoDB.Business.Dtos;
using Data.MongoDB.Repository.Abstracts;

namespace Data.MongoDB.Business;

public class TransactionBusiness : ITransactionBusiness
{
    private readonly IRepository<Entities.Transaction> _transactionRepository;
    
    public TransactionBusiness(IRepository<Entities.Transaction> transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }
    
    public Task<bool> Create(CreateTransactionDto request)
    {
        try
        {
            _transactionRepository.Insert(new Entities.Transaction
            {
                TransactionId = request.TransactionId,
                FromWalletId = request.FromWalletId,
                ToWalletId = request.ToWalletId,
                CardNumber = request.CardNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Total = request.Total,
                CreatedOnUtc = request.CreatedOnUtc,
                UpdatedOnUtc = request.UpdatedOnUtc
            });
            
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> Update(long id, PatchTransactionDto request)
    {
        try
        {
            var data = _transactionRepository.GetAll().Where(x => x.TransactionId == id).FirstOrDefault();
            if (data is null)
                return Task.FromResult(false);

            data.FromWalletId = request.FromWalletId;
            data.ToWalletId = request.ToWalletId;
            data.CardNumber = request.CardNumber;
            data.FirstName = request.FirstName;
            data.LastName = request.LastName;
            data.Email = request.Email;
            data.Total = request.Total;
            data.UpdatedOnUtc = request.UpdatedOnUtc;
            
            _transactionRepository.Update(data);
            
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            return Task.FromResult(false);
        }
    }

    public Task<bool> Delete(long id)
    {
        try
        {
            var data = _transactionRepository.GetAll().Where(x => x.TransactionId == id).FirstOrDefault();
            if (data is null)
                return Task.FromResult(false);
            
            _transactionRepository.Delete(data.Id);
            
            return Task.FromResult(true);
        }
        catch (Exception e)
        {
            return Task.FromResult(false);
        }
    }
    
    public async Task<TransactionDto> Get(long id)
    {
        var data = _transactionRepository.GetAll().FirstOrDefault(x => x.TransactionId == id);
        if (data == null)
            return null;
        
        return new TransactionDto
        {
            TransactionId = data.TransactionId,
            FromWalletId = data.FromWalletId,
            ToWalletId = data.ToWalletId,
            CardNumber = data.CardNumber,
            FirstName = data.FirstName,
            LastName = data.LastName,
            Email = data.Email,
            Total = data.Total
        };
    }
    
    public async Task<IList<TransactionDto>> GetAll()
    {
        return _transactionRepository.GetAll().Select(x => new TransactionDto
        {
            TransactionId = x.TransactionId,
            FromWalletId = x.FromWalletId,
            ToWalletId = x.ToWalletId,
            CardNumber = x.CardNumber,
            FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email,
            Total = x.Total
        }).ToList();
    }
}