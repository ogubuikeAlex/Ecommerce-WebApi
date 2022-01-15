using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;
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
        public async Task<IActionResult> ClearCart(Cart cart) { return Ok(); }
        ReturnModel GetTotalCartPrice(Cart cart);
    }
}
