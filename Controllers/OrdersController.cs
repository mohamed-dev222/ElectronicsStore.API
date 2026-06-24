using ElectronicsStore.API.DTOs.Order;
using ElectronicsStore.API.Helpers;
using ElectronicsStore.API.Models;
using ElectronicsStore.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<OrderReadDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] OrderCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Helpers.ApiResponse<object>.Fail("Validation failed", ModelState));
            }

            var order = await _orderService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = order.Id }, Helpers.ApiResponse<OrderReadDto>.SuccessResponse(order, "Order created successfully"));
        }

        [HttpGet]
        [ProducesResponseType(typeof(Helpers.ApiResponse<PaginationResult<OrderReadDto>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10, [FromQuery] OrderStatus? status = null)
        {
            var result = await _orderService.GetAllAsync(page, pageSize, status);
            return Ok(Helpers.ApiResponse<PaginationResult<OrderReadDto>>.SuccessResponse(result, "Orders fetched successfully"));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<OrderReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(Helpers.ApiResponse<OrderReadDto>.SuccessResponse(order, "Order fetched successfully"));
        }

        [HttpPut("{id:int}/status")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<OrderReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] OrderUpdateStatusDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(Helpers.ApiResponse<object>.Fail("Validation failed", ModelState));
            }

            var order = await _orderService.UpdateStatusAsync(id, dto.Status);
            return Ok(Helpers.ApiResponse<OrderReadDto>.SuccessResponse(order, "Order status updated successfully"));
        }

        [HttpPut("{id:int}/cancel")]
        [ProducesResponseType(typeof(Helpers.ApiResponse<OrderReadDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Helpers.ApiResponse<object>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel(int id)
        {
            var order = await _orderService.CancelAsync(id);
            return Ok(Helpers.ApiResponse<OrderReadDto>.SuccessResponse(order, "Order cancelled successfully"));
        }
    }
}