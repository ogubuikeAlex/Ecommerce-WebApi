using KingsStoreApi.Extensions;
using KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KingsStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBaseExtension
    {
        private readonly ITransactionService _transactionService;
        private readonly ICartService cartService;

        public TransactionController(ITransactionService transactionService, UserManager<User> userManager, ICartService cartService): base(userManager)
        {
            this._transactionService = transactionService;
            this.cartService = cartService;
        }
        [HttpPost("pay")]
        public async Task<IActionResult> PayForProduct(PayForProductDTO model)
        {
           var user = await GetLoggedInUserAsync();
            var result = _transactionService.PayForProduct(model.Amount, model.OrderId, user);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPost("confirmOrder")]
        public async Task<IActionResult> ConfirmOrder(ConfirmTransactionDTO confirmTransactionModel) 
        {
            var user = await GetLoggedInUserAsync();
            var result = await _transactionService.ConfirmOrder(confirmTransactionModel, user);

            //empty out cart
            cartService.ClearCart(user.Id);

            if (!result.Success)
                return result.Message.Contains("not found") ? NotFound(result.Message) : BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
