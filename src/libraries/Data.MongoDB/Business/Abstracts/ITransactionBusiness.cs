using Data.MongoDB.Business.Dtos;

namespace Data.MongoDB.Business.Abstracts;

public interface ITransactionBusiness
{
    Task<bool> Create(CreateTransactionDto request);
    Task<bool> Update(long id, PatchTransactionDto request);
    Task<bool> Delete(long id);
    Task<TransactionDto> Get(long id);
    Task<IList<TransactionDto>> GetAll();
}