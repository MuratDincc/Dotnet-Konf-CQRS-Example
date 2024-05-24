using Data.PostgreSQL.Business.Dtos;

namespace Data.PostgreSQL.Business.Abstracts;

public interface ITransactionBusiness
{
    Task<long> Create(CreateTransactionDto request);
    Task Update(long id, UpdateTransactionDto request);
    Task Delete(long id);
}