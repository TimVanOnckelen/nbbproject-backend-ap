using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NBB.Api.Models;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<IdentityUser> _userManager;

        public AuthenticationController(IAuthenticationService authenticationService, IConfiguration configuration, UserManager<IdentityUser> userManager)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User user)
        {
            bool isAuthenticated = await _authenticationService.AuthenticateUserAsync(user.UserName, user.Password);

            if (isAuthenticated)
            {
                // User is authenticated, generate JWT token
                var jwtToken = GenerateJwtToken(user);

                // Return success response with JWT token
                return Ok(new { message = "Login successful", token = jwtToken });
            }
            else
            {
                // User authentication failed, return error response
                return Unauthorized(new { message = "Invalid username or password" });
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] JObject data)
        {
            var username = data["username"].ToString();
            var email = data["email"].ToString();
            var password = data["password"].ToString();

            var user = new IdentityUser
            {
                UserName = username,
                Email = email,
                // set any other properties needed for user registration
            };

            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                // User was created successfully, generate JWT token
                var jwtToken = GenerateJwtToken(user);

                // Return success response with JWT token
                return Ok(new { message = "User created", token = jwtToken });
            }
            else
            {
                // User creation failed, return error response
                return BadRequest(new { errors = result.Errors });
            }
        }

        private string GenerateJwtToken(IdentityUser user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var key = Encoding.ASCII.GetBytes(jwtSettings.GetValue<string>("Secret"));

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(jwtSettings.GetValue<int>("ExpirationInDays")),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
