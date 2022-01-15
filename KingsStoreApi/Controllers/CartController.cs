using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        public Task<ReturnModel> AddCartItem(AddToCartDTO model);
        Task<ReturnModel> RemoveCartItem(string cartItemId);
        Task<ReturnModel> ClearCart(Cart cart);
        ReturnModel GetTotalCartPrice(Cart cart);
    }
}
