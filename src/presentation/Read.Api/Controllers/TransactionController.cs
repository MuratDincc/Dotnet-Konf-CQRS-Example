using Data.MongoDB.Business.Abstracts;
using Data.MongoDB.Business.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Read.Api.Controllers;

/// <summary>
/// Transaction
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
    /// Get Transcaction By Id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TransactionDto), 200)]
    public async Task<IActionResult> Get(int id)
    {
        return Ok(await _transactionBusiness.Get(id));
    }
    
    /// <summary>
    /// Get News
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TransactionDto>), 200)]
    public async Task<IActionResult> Get()
    {
        return Ok(await _transactionBusiness.GetAll());
    }
    
    #endregion
}
