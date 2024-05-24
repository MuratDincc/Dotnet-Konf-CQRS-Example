using Data.PostgreSQL.Business.Abstracts;
using Data.PostgreSQL.Business.Dtos;
using Microsoft.AspNetCore.Mvc;
using Write.Api.Models.Request;
using Write.Api.Models.Response;

namespace Write.Api.Controllers;

/// <summary>
/// Transactions
/// </summary>
[Route("api/v1/transactions")]
public class TransactionController : ControllerBase
{
    #region Fields

    private readonly ITransactionBusiness _transactionBusiness;

    #endregion
    
    #region Ctor
    
    public TransactionController(ITransactionBusiness transactionBusiness)
    {
        _transactionBusiness = transactionBusiness;
    }

    #endregion
    
    #region Methods
    
    /// <summary>
    /// Create Transaction
    /// </summary>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(CreateTransactionResponse), 200)]
    public async Task<IActionResult> Post([FromBody] CreateTransactionRequest request)
    {
        return Ok(await _transactionBusiness.Create(new CreateTransactionDto
        {
            FromWalletId = request.FromWalletId,
            ToWalletId = request.ToWalletId,
            CardNumber = request.CardNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Total = request.Total
        }));
    }

    /// <summary>
    /// Update Transaction
    /// </summary>
    /// <param name="id">Transaction Id</param>
    /// <param name="request">Request Model</param>
    /// <returns></returns>
    [HttpPatch("{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Patch([FromRoute] int id, [FromBody] PatchTransactionRequest request)
    {
        await _transactionBusiness.Update(id, new UpdateTransactionDto
        {
            FromWalletId = request.FromWalletId,
            ToWalletId = request.ToWalletId,
            CardNumber = request.CardNumber,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Total = request.Total
        });
        
        return Ok();
    }

    /// <summary>
    /// Delete Transaction
    /// </summary>
    /// <param name="id">Transaction Id</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        await _transactionBusiness.Delete(id);

        return Ok();
    }
    
    #endregion
}
