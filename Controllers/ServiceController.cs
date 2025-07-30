using Microsoft.AspNetCore.Mvc;
using berber.Data;
using berber.Models;
using Microsoft.EntityFrameworkCore;

namespace berber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ServiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/service
        [HttpPost]
        public IActionResult Create([FromBody] Service service)
        {
            try
            {
                _context.Services.Add(service);
                _context.SaveChanges();
                return Ok(service);
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message;
                return BadRequest(new { error = ex.Message, innerError = innerMessage });
            }

        }

        // GET: api/service
        [HttpGet]
        public IActionResult GetAll()
        {
            var services = _context.Services.ToList();
            return Ok(services);
        }
    }
}
