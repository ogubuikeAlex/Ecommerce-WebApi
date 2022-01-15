using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KingsStoreApi.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IActionResult> AddCartItem(AddToCartDTO model) { return Ok(); }
        public async Task<IActionResult> RemoveCartItem(string cartItemId) { return Ok(); }
        Task<ReturnModel> ClearCart(Cart cart);
        ReturnModel GetTotalCartPrice(Cart cart);
    }
}
