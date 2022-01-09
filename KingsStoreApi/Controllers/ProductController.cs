using KingsStoreApi.Extensions;
using KingsStoreApi.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KingsStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBaseExtension
    {
        public ProductController(UserManager<User> userManager) : base(userManager)
        {


        }

        public IActionResult GetProducts()
        {
            return Ok();
        }

        public IActionResult GetProductsVendor()
        {
            return Ok();
        }
    }
}
