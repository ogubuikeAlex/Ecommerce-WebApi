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




        /*ReturnModel GetProductByName(string name);
        ReturnModel GetProductById(string id);
        ReturnModel GetAllProducts();
        Task<ReturnModel> GetDisabledProductsByVendor(string email);
        Task<ReturnModel> GetProductsByVendor(string email);
        Task<ReturnModel> BuyNow();*/
    }
}
