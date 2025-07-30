using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using berber.Data;
using berber.Models;

namespace berber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppointmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/appointment
        [HttpPost]
        public IActionResult Create([FromBody] Appointment appointment)
        {
 
            try
            {
                _context.Appointments.Add(appointment);
                _context.SaveChanges();
                return Ok(appointment);
            }
            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message;
                return BadRequest(new { error = ex.Message, innerError = innerMessage });
            }
        }

        // GET: api/appointment
    
        [HttpGet]
        public IActionResult GetAll()
        {
            /*    var appointments = _context.Appointments
                    .Include(a => a.User)
                    .Include(a => a.Service)
                    .ToList();
            */
            var appointments = _context.Appointments.ToList();
            return Ok(appointments);
        }
    }
}
