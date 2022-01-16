using KingsStoreApi.Extensions;
using KingsStoreApi.Model.DataTransferObjects.CartServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [HttpGet]//working
        public async Task<IActionResult> GetAllCartItems()
        {
            var user = await GetLoggedInUserAsync();
            var result = _cartService.GetCartItems(user.Id);

            if (!result.Success)
                return NotFound(result.Message);

            var items = result.Object as IEnumerable<CartItem>;

            return Ok(items);
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

        [HttpGet("TotalPrice")]//working
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
