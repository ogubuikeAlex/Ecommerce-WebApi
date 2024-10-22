﻿using KingsStoreApi.Extensions;
using KingsStoreApi.Helpers.Implementations.RequestFeatures;
using KingsStoreApi.Model.DataTransferObjects.ProductServiceDTO;
using KingsStoreApi.Model.DataTransferObjects.SharedDTO;
using KingsStoreApi.Model.Entities;
using KingsStoreApi.Services.Interfaces;
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
        private readonly IProductService _productService;

        public ProductController(IProductService productService, UserManager<User> userManager) : base(userManager)
        {
            _productService = productService;
        }

        [HttpGet]//working
        public IActionResult GetProducts([FromQuery] ProductRequestParameters requestParameter)
        {
            var result = _productService.GetAllProducts(requestParameter);

            if (!result.Success)
                return NotFound(result.Message);

            var products = result.Object as List<Product>;

            return Ok(products);
        }

        [HttpGet("email/{email}")]//working
        public async Task<IActionResult> GetProductsVendor([FromQuery] ProductRequestParameters requestParameter)
        {
            var user = await GetLoggedInUserAsync();
            var result = _productService.GetProductsByVendor(user, requestParameter);

            if (!result.Success)
                return BadRequest(result.Message);

            var products = result.Object as List<Product>;

            return Ok(products);
        }

        [HttpGet("name/{name}")]//working
        public IActionResult GetProductByName(string name, [FromQuery] ProductRequestParameters requestParameters)
        {
            var result = _productService.GetProductByName(name, requestParameters);
            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as IEnumerable<Product>;

            return Ok(product);
        }

        [HttpGet("id/{id}")]//working
        public IActionResult GetProductById(string id)
        {
            var result = _productService.GetProductById(id);
            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as Product;

            return Ok(product);
        }

        [HttpGet("disabledProducts")]//working
        public async Task<IActionResult> GetDisabledProductsByVendor([FromQuery] ProductRequestParameters requestParameters)
        {
            var user = await GetLoggedInUserAsync();
            var result = _productService.GetDisabledProductsByVendor(user, requestParameters);

            if (!result.Success)
                return BadRequest(result.Message);

            var product = result.Object as IEnumerable<Product>;

            return Ok(product);
        }

        [HttpPost]//working
        public async Task<IActionResult> UploadProduct(UploadProductDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _productService.UploadProduct(model, user);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("UploadImage")]//working
        public async Task<IActionResult> UplaodProductImage([FromForm] UploadImageDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _productService.UplaodProductImage(model, user);

            if (!result.Success)
                return NotFound(result.Message);
            return Ok(result.Message);
        }

        [HttpPost("EditPrice")]//working
        public async Task<IActionResult> EditProductPrice(EditProductPriceDTO model)
        {
            var user = await GetLoggedInUserAsync();
            var result = await _productService.EditProductPrice(model, user);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("EditSummary")]//working
        public async Task<IActionResult> EditProductSummary(EditProductDTO model)
        {
            var user = await GetLoggedInUserAsync();

            var result = await _productService.EditProductSummary(model, user);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("EditTitle")]//working
        public async Task<IActionResult> EditProductTitl(EditProductDTO model)
        {
            var user = await GetLoggedInUserAsync();

            var result = await _productService.EditProductTitle(model, user);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("TempDisable")]//working
        public async Task<IActionResult> TemporarilyDisableAProduct(string id)
        {
            var result = await _productService.TemporarilyDisableAProduct(id);

            if (!result.Success)
                return result.Message.Contains("not found") ? NotFound(result.Message) : BadRequest(result.Message);

            return Ok(result.Message);
        }
    }
}
