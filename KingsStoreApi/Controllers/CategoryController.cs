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
            var result = await _categoryService.CreateCategory(model);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        public async Task<IActionResult> ToggleSoftDeleteCategory(string id)
        {
            var result = await _categoryService.ToggleSoftDeleteCategory(id);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        public async Task<IActionResult> UpdateCategoryTitle(UpdateCategoryDTO model)
        {
            
        }
          
        public async Task<IActionResult> UpdateCategorySummary(UpdateCategoryDTO model)
        {
            return Ok();
        }
    }
}
