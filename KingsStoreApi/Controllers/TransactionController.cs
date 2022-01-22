using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KingsStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        public TransactionController()
        {

        }
        public IActionResult PayForProduct(decimal amount, Order order, User user)
        {

        }
        public Task<IActionResult> ConfirmOrder(ConfirmTransactionDTO confirmTransactionModel, User user); 
        {

        }
    }
}
