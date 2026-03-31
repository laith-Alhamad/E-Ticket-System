using ETicketing.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicketing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController:ControllerBase
    {
        private readonly AppDbContext _context;

        public TicketsController(AppDbContext context)
        {
            _context=context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            var tickets = await _context.Tickets
                .Where(x => x.IsActive)
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Price,
                    x.RemainingQuota
                })
                .ToListAsync();

            return Ok(tickets);
        }
    }
}