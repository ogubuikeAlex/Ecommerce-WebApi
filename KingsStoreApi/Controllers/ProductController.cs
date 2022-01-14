using KingsStoreApi.Extensions;
using KingsStoreApi.Helpers.Implementations.RequestFeatures;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KingsStoreApi.Controllers
{
    [Route("api/Product")]
    [ApiController]
    public class ProductController : ControllerBaseExtension
    {
        private readonly ProductService _productService;

       public ProductController(ProductService productService, UserManager<User> userManager) : base(userManager)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult GetProducts([FromQuery] ProductRequestParameters requestParameter)
        {
            var result = _productService.GetAllProducts(requestParameter);

            if (!result.Success)
                return BadRequest(result.Message);
            var products = result.Object as List<Product>;

            return Ok(products);
        }

        [HttpGet("email/{email}")]
        public async Task<IActionResult> GetProductsVendor([FromQuery] ProductRequestParameters requestParameter, string email)
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

        [HttpGet("id/{id}")]
        public IActionResult GetProductById(string id)
        {
            var result = _productService.GetProductById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as Product;

            return Ok(product);
        }

        [HttpGet("{email}/disabledProducts")]
        public async Task<IActionResult> GetDisabledProductsByVendor([FromQuery] ProductRequestParameters requestParameters, string email)
        {
            var result = await _productService.GetDisabledProductsByVendor(email, requestParameters);
            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as Product;

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> UploadProduct(UploadProductDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _productService.UploadProduct(model, user);

            if (!result.Success)
                return BadRequest("Product not uploaded");

            return Ok(result.Message);
        }

        [HttpPost("UploadImage")]
        public async Task<IActionResult> UplaodProductImage(UploadImageDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _productService.UplaodProductImage(model, user);

            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result.Message);
        }

        [HttpPost("EditPrice")]
        public async Task<IActionResult> EditProductPrice(EditProductDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _productService.EditProductPrice(model, user);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("EditSummary")]
        public async Task<IActionResult> EditProductSummary(EditProductDTO model)
        {
            var user = await GetLoggedInUserAsync();

            var result = await _productService.EditProductSummary(model, user);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("EditTitle")]
        public async Task<IActionResult> EditProductTitl(EditProductDTO model)
        {
            var user = await GetLoggedInUserAsync();

            var result = await _productService.EditProductTitle(model, user);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("TempDisable")]
        public async Task<IActionResult> TemporarilyDisableAProduct(string id)
        {
            var result = await _productService.TemporarilyDisableAProduct(id);

            if (!result.Success)
                return result.Message.Contains("not found") ? NotFound(result.Message) : BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
