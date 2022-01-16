using KingsStoreApi.Model.DataTransferObjects.CategoryServicesDTO;
using KingsStoreApi.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KingsStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult GetCategory (string categoryName)
        {
            var result = _categoryService.GetCategory(categoryName);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        public IActionResult GetAllCategories()
        {
            var result = _categoryService.GetAllCategories();

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        public async Task<IActionResult> CreateCategory(CreateCategoryDTO model)
        {
            return Ok();
        }

        public async Task<IActionResult> ToggleSoftDeleteCategory(string id)
        {
            return Ok();
        }

        public async Task<IActionResult> UpdateCategoryTitle(UpdateCategoryDTO model) { return Ok(); }
          
        public async Task<IActionResult> UpdateCategorySummary(UpdateCategoryDTO model)
        {
            return Ok();
        }
    }
}
