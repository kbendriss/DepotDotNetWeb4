using Microsoft.AspNetCore.Mvc;
using BasketBallLiveScore.Models;
using BasketBallLiveScore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System;

namespace BasketBallLiveScore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly BasketballContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(BasketballContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Check if the email already exists
                if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                {
                    return BadRequest(new { message = "This email is already in use." });
                }

                // Check if the username already exists
                if (await _context.Users.AnyAsync(u => u.Username == user.Username))
                {
                    return BadRequest(new { message = "This username is already taken." });
                }

                // Check if the combination of first name and last name is unique
                if (await _context.Users.AnyAsync(u => u.FirstName == user.FirstName && u.LastName == user.LastName))
                {
                    return BadRequest(new { message = "A user with the same first name and last name already exists." });
                }

                // Hash the password
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                // Add the user to the database
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                // Commit the transaction
                await transaction.CommitAsync();

                return Ok(new { message = "Registration successful!" });
            }
            catch
            {
                await transaction.RollbackAsync();
                throw; // Rethrow the exception for global handling
            }
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            // Find the user by username
            var existingUser = await _context.Users
                .SingleOrDefaultAsync(u => u.Username == user.Username);

            // Verify credentials
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password))
            {
                return Unauthorized(new { message = "Invalid username or password." });
            }

            // Generate a JWT token
            var token = GenerateJwtToken(existingUser);

            return Ok(new
            {
                message = "Login successful",
                token,
                user = new
                {
                    existingUser.Id,
                    existingUser.FirstName,
                    existingUser.LastName,
                    existingUser.Username
                }
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                // Recherche de l'utilisateur par son ID
                var user = await _context.Users
                    .Select(u => new
                    {
                        u.Id,
                        u.Username,
                        u.Email,
                        u.FirstName,  // Inclure FirstName
                        u.LastName    // Inclure LastName
                    })
                    .FirstOrDefaultAsync(u => u.Id == id);

                // Vérifiez si l'utilisateur existe
                if (user == null)
                {
                    return NotFound(new { message = "Utilisateur introuvable." });
                }

                return Ok(user); // Retourne les informations de l'utilisateur
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Une erreur s'est produite lors de la récupération de l'utilisateur.", details = ex.Message });
            }
        }

    }
}
