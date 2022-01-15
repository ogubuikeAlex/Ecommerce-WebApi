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

        public async Task<IActionResult> AddCartItem(AddToCartDTO model) { return Ok()};
        Task<ReturnModel> RemoveCartItem(string cartItemId);
        Task<ReturnModel> ClearCart(Cart cart);
        ReturnModel GetTotalCartPrice(Cart cart);
    }
}
