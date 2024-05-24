using FakeDataPoster.Services.Models;
using Refit;

namespace FakeDataPoster.Services;

public interface ITransactionService
{
    [Post("/api/v1/transactions")]
    Task<long> Create([Body] CreateTransactionRequest request);
}