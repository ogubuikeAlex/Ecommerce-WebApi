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
        
        public IActionResult GetProductByName()
        {
            return Ok();
        }
        public IActionResult GetProductById()
        {
            return Ok();
        }
        public IActionResult GetAllProducts()
        {
            return Ok();
        }
        public IActionResult GetDisabledProductsByVendo()
        {
            return Ok();
        }
        public IActionResult BuyNow()
        {
            return Ok();
        }
        public IActionResult UploadProduct()
        {
            return Ok();
        }
        public IActionResult UplaodProductImage()
        {
            return Ok();
        } 
        public IActionResult EditProductPrice()
        {
            return Ok();
        }
        public IActionResult EditProductSummary()
        {
            return Ok();
        }
        public IActionResult EditProductTitl()
        {
            return Ok();
        }
        public IActionResult TemporarilyDisableAProduct()
        {
            return NotFound();
        }







       
    }
}
