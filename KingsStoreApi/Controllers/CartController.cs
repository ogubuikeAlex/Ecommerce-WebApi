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

        [HttpGet]
        public IActionResult GetAllCartItems()
        {

        }
        [HttpPost("Add")]//working
        public async Task<IActionResult> AddCartItem(AddToCartDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _cartService.AddCartItem(user, model.ProductId, model.Quantity);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("Remove")]//working
        public async Task<IActionResult> RemoveCartItem(string cartItemId)
        {
            var result = await _cartService.RemoveCartItem(cartItemId);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("Clear")]//working
        public async Task<IActionResult> ClearCart()
        {
            var user = await GetLoggedInUserAsync();
            var result = await _cartService.ClearCart(user.Id);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpGet]//working
        public async Task<IActionResult> GetTotalCartPrice()
        {
            var user = await GetLoggedInUserAsync();

            var result = _cartService.GetTotalCartPrice(user.Id);

            if (!result.Success)
                return NotFound(result.Message);

            var totalAmount = result.Object as string;

            return Ok($"Total Amount : $ {totalAmount}");
        }
    }
}
