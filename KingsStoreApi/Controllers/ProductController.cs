using KingsStoreApi.Data.Implementations;
using KingsStoreApi.Extensions;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace KingsStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBaseExtension
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService, UserManager<User> userManager) : base(userManager)
        {
            _productService = productService;
        }

        public IActionResult GetProducts()
        {
            var result = _productService.GetAllProducts();

            if (!result.Success)
                return BadRequest(result.Message);
            var products = result.Object as List<Product>;

            return Ok(products);
        }

        public IActionResult GetProductsVendor(string email)
        {
            var result = _productService.GetProductsByVendor()
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
            return NotFound();
        }
        public IActionResult EditProductTitl()
        {
            return NotFound();
        }
        public IActionResult TemporarilyDisableAProduct()
        {
            return NotFound();
        }







       
    }
}
