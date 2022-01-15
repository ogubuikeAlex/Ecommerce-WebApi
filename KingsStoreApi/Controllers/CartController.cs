using KingsStoreApi.Extensions;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KingsStoreApi.Controllers
{
    [Route("api/Cart")]
    [ApiController]
    public class CartController : ControllerBaseExtension
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService, UserManager<User> userManager) : base(userManager)
        {
            _cartService = cartService;
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(AddToCartDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _cartService.AddCartItem(user, model.ProductId, model.Quantity);

            if (!result.Success)
                return BadRequest();

            return Ok(result.Message);            
        }

        /*public async Task<IActionResult> RemoveCartItem(string cartItemId)
        {
            var result = _cartService.RemoveCartItem(model);
        }

        public async Task<IActionResult> ClearCart(Cart cart) { return Ok(); }
        public IActionResult GetTotalCartPrice(Cart cart) { return Ok(); }*/
    }
}
