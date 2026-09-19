using System.Threading.Tasks;
using JLITE.API.Models;
using JLITE.API.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace JLITE.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly MongoDbContext _context;
        private readonly IJwtService _jwtService;

        public AuthController(MongoDbContext context, IJwtService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _context.Users.Find(u => u.Email == model.Email).FirstOrDefaultAsync();
            if (user == null || !_jwtService.VerifyPassword(model.Password, user.PasswordHash))
            {
                return Unauthorized(new { message = "Invalid email or password" });
            }

            var token = _jwtService.GenerateAccessToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = System.DateTime.UtcNow.AddDays(7);
            await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);

            return Ok(new
            {
                token,
                refreshToken,
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Email,
                    user.Phone,
                    user.Role,
                    user.CompanyName,
                    user.GSTIN,
                    user.IsB2BVerified
                }
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var existing = await _context.Users.Find(u => u.Email == model.Email).FirstOrDefaultAsync();
            if (existing != null)
            {
                return BadRequest(new { message = "Email already registered" });
            }

            var user = new User
            {
                FullName = model.FullName,
                Email = model.Email,
                Phone = model.Phone,
                PasswordHash = _jwtService.HashPassword(model.Password),
                Role = string.IsNullOrWhiteSpace(model.Role) ? "Customer" : model.Role,
                CompanyName = model.CompanyName ?? string.Empty,
                GSTIN = model.GSTIN ?? string.Empty,
                IsB2BVerified = !string.IsNullOrWhiteSpace(model.GSTIN)
            };

            await _context.Users.InsertOneAsync(user);

            var token = _jwtService.GenerateAccessToken(user);
            return Ok(new { message = "Registration successful", token, user = new { user.Id, user.FullName, user.Email, user.Role } });
        }
    }

    public class LoginDto { public string Email { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; }
    public class RegisterDto { public string FullName { get; set; } = string.Empty; public string Email { get; set; } = string.Empty; public string Phone { get; set; } = string.Empty; public string Password { get; set; } = string.Empty; public string Role { get; set; } = "Customer"; public string? CompanyName { get; set; } public string? GSTIN { get; set; } }
}
