using ElectronicsStore.API.DTOs.Category;
using ElectronicsStore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Helpers.ApiResponse<IEnumerable<CategoryResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync();
            return Ok(Helpers.ApiResponse<IEnumerable<CategoryResponseDto>>.SuccessResponse(categories, "Categories fetched successfully"));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<CategoryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            return Ok(Helpers.ApiResponse<CategoryResponseDto>.SuccessResponse(category, "Category fetched successfully"));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<CategoryResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Helpers.ApiResponse<object>.Fail("Validation failed", ModelState));
            }

            var category = await _categoryService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, Helpers.ApiResponse<CategoryResponseDto>.SuccessResponse(category, "Category created successfully"));
        }

        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<CategoryResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Helpers.ApiResponse<object>.Fail("Validation failed", ModelState));
            }

            var category = await _categoryService.UpdateAsync(id, dto);
            return Ok(Helpers.ApiResponse<CategoryResponseDto>.SuccessResponse(category, "Category updated successfully"));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}