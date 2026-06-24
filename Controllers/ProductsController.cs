using ElectronicsStore.API.DTOs.Product;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Helpers.ApiResponse<PaginationResult<ProductResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] int? categoryId = null, [FromQuery] string? query = null)
        {
            var result = await _productService.GetAllAsync(page, pageSize, categoryId, query);
            return Ok(Helpers.ApiResponse<PaginationResult<ProductResponseDto>>.SuccessResponse(result, "Products fetched successfully"));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<ProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            return Ok(Helpers.ApiResponse<ProductResponseDto>.SuccessResponse(product, "Product fetched successfully"));
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<ProductResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Helpers.ApiResponse<object>.Fail("Validation failed", ModelState));
            }

            var product = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, Helpers.ApiResponse<ProductResponseDto>.SuccessResponse(product, "Product created successfully"));
        }

        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<ProductResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Helpers.ApiResponse<object>.Fail("Validation failed", ModelState));
            }

            var product = await _productService.UpdateAsync(id, dto);
            return Ok(Helpers.ApiResponse<ProductResponseDto>.SuccessResponse(product, "Product updated successfully"));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }
    }
}