using Microsoft.AspNetCore.Mvc;
using Payments.Api.Dtos;
using Payments.Api.Services;

namespace Payments.Api.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")] // [cite: 46]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentsController(IPaymentService service)
        {
            _service = service;
        }

        
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePaymentDto dto, [FromHeader(Name = "X-Api-Key")] string apiKey)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); // [cite: 19]

            var result = await _service.CreatePaymentAsync(dto, apiKey);

            if (result.Status == Models.PaymentStatus.Rejected)
                return StatusCode(403, new { message = "Invalid API Key", payment = result });

            return Ok(result);
        }

        // GET api/payments [cite: 50]
        [HttpGet]
        public async Task<IActionResult> GetHistory([FromQuery] int page = 1)
        {
            var pageSize = 10;
            var data = await _service.GetHistoryAsync((page - 1) * pageSize, pageSize);
            return Ok(data);
        }

        // GET api/payments/stats [cite: 50]
        [HttpGet("stats")]
        public async Task<IActionResult> GetStats()
        {
            var stats = await _service.GetStatsAsync();
            return Ok(stats);
        }
    }
}