using ETicketing.API.DTOs;
using ETicketing.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace ETicketing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController:ControllerBase
    {
        private readonly IBookingService _bookingService;

        public CheckoutController(IBookingService bookingService)
        {
            _bookingService=bookingService;
        }

        [HttpPost]
        public async Task<IActionResult> Checkout([FromBody] CheckoutRequestDto request)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var result = await _bookingService.CheckoutAsync(request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    message = ex.Message
                });
            }
        }
    }
}