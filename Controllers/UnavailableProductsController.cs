using ElectronicsStore.API.DTOs.UnavailableProduct;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class UnavailableProductsController : ControllerBase
    {
        private readonly IUnavailableProductService _service;

        public UnavailableProductsController(IUnavailableProductService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Helpers.ApiResponse<PaginationResult<UnavailableRequestResponseDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] bool? isFulfilled = null)
        {
            var result = await _service.GetAllAsync(page, pageSize, isFulfilled);
            return Ok(Helpers.ApiResponse<PaginationResult<UnavailableRequestResponseDto>>.SuccessResponse(result, "Unavailable requests fetched successfully"));
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<UnavailableRequestResponseDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromForm] CreateUnavailableRequestFormDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Helpers.ApiResponse<object>.Fail("Validation failed", ModelState));
            }

            var request = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = request.Id }, Helpers.ApiResponse<UnavailableRequestResponseDto>.SuccessResponse(request, "Unavailable request created successfully"));
        }

        [HttpPut("{id:int}/fulfill")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<UnavailableRequestResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> MarkAsFulfilled(int id)
        {
            var request = await _service.MarkAsFulfilledAsync(id);
            return Ok(Helpers.ApiResponse<UnavailableRequestResponseDto>.SuccessResponse(request, "Unavailable request fulfilled successfully"));
        }
    }
}
