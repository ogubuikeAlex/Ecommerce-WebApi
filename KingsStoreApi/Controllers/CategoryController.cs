using KingsStoreApi.Model.DataTransferObjects.CategoryServicesDTO;
using KingsStoreApi.Model.Entities;
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

        [HttpGet("{categoryName}")]//working
        public IActionResult GetCategory (string categoryName)
        {
            var result = _categoryService.GetCategory(categoryName);

            if (!result.Success)
                return NotFound(result.Message);

            var category = result.Object as Category;
            return Ok(category);
        }

        [HttpGet]//working
        public IActionResult GetAllCategories()
        {
            var result = _categoryService.GetAllCategories();

            if (!result.Success)
                return NotFound(result.Message);

            var categories = result.Object as IEnumerable<Category>;
            return Ok(categories);
        }

        [HttpPost]//working
        public async Task<IActionResult> CreateCategory(CreateCategoryDTO model)
        {
            var result = await _categoryService.CreateCategory(model);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("SoftDelete")]//working
        public async Task<IActionResult> ToggleSoftDeleteCategory(string name)
        {
            var result = await _categoryService.ToggleSoftDeleteCategory(name);

            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPost("titleUpdate")]//working
        public async Task<IActionResult> UpdateCategoryTitle(UpdateCategoryDTO model)
        {
            var result = await _categoryService.UpdateCategoryTitle(model);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
          
        [HttpPost("summaryUpdate")]//working
        public async Task<IActionResult> UpdateCategorySummary(UpdateCategoryDTO model)
        {
            var result = await _categoryService.UpdateCategorySummary(model);

            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }
    }
}
