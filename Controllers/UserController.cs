using Microsoft.AspNetCore.Mvc;
using berber.Data;
using berber.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Azure.Core;
using Microsoft.AspNetCore.Identity.Data;
using berber.Helpers;

namespace berber.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly JwtService _jwtService;

        public UserController(ApplicationDbContext context, JwtService jwtService)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
            _jwtService = jwtService;
        }
        public class RegisterRequest
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        // POST: api/user/register
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            try
            {
                if (_context.Users.Any(u => u.Email == request.Email))
                    return BadRequest("Bu e-posta adresi zaten kayıtlı.");

                var user = new User
                {
                    Name = request.Name,
                    Email = request.Email,
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                _context.Users.Add(user);
                _context.SaveChanges();

                return Ok("Kayıt başarılı.");
            }

            catch (DbUpdateException ex)
            {
                var innerMessage = ex.InnerException?.Message;
                return BadRequest(new { error = ex.Message, innerError = innerMessage });
            }

            
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == request.Email);

            if (user == null)
                return Unauthorized("Geçersiz kullanıcı.");

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Şifre hatalı.");

            var token = _jwtService.GenerateToken(user);

            // Şimdilik sadece giriş başarılı diyelim
            return Ok(
            new {
                Message = "Giriş başarılı.",
                Token = token
                });

        }

        // GET: api/user
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }
    }
}
