using KingsStoreApi.Extensions;
using KingsStoreApi.Helpers.Implementations.RequestFeatures;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public IActionResult GetProducts([FromQuery]ProductRequestParameters requestParameter)
        {
            var result = _productService.GetAllProducts(requestParameter);

            if (!result.Success)
                return BadRequest(result.Message);
            var products = result.Object as List<Product>;

            return Ok(products);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetProductsVendor([FromQuery]ProductRequestParameters requestParameter, string email)
        {
            var result = await _productService.GetProductsByVendor(email, requestParameter);

            if (!result.Success)
                return BadRequest(result.Message);

            var products = result.Object as List<Product>;

            return Ok(products);
        }
        
        [HttpGet("name/{name}")]
        public IActionResult GetProductByName(string name)
        {
            var result = _productService.GetProductByName(name);
            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as Product;

            return Ok(product);            
        }
       
        public IActionResult GetProductById(string id)
        {
            var result = _productService.GetProductById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as Product;

            return Ok(product);
        }
        
        [HttpGet("id/{id}")]
        public IActionResult GetDisabledProductsByVendo( [FromQuery] ProductRequestParameters requestParameters, string email)
        {
            var result = await _productService.GetDisabledProductsByVendor(email, requestParameters);
            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as Product;

            return Ok(product);
        }
       
        public IActionResult UploadProduct(UploadProductDTO model)
        {
            var product = _IMapper<Product>(model);
            //configure mapper!
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
