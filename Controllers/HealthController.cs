using ElectronicsStore.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsStore.API.Controllers
{
    [ApiController]
    [Route("health")]
    [Produces("application/json")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
        public IActionResult GetHealth()
        {
            var payload = new { status = "Healthy" };
            return Ok(ApiResponse<object>.SuccessResponse(payload, "Service is healthy"));
        }
    }
}
