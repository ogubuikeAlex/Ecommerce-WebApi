using KingsStoreApi.Model.DataTransferObjects.TransactionServiceDTO;
using KingsStoreApi.Model.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
        public Task<IActionResult> ConfirmOrder(ConfirmTransactionDTO confirmTransactionModel, User user) 
        {

        }
    }
}
