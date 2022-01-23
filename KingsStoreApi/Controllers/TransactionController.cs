using KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KingsStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            this._transactionService = transactionService;
        }
        public IActionResult PayForProduct(decimal amount, Order order, User user)
        {
            var result = _transactionService.PayForProduct(amount, order, user);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);

        }
        public IActionResult ConfirmOrder(ConfirmTransactionDTO confirmTransactionModel, User user) 
        {
            return Ok(user);
        }
    }
}
