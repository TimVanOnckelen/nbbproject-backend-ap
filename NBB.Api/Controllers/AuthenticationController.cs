using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using NBB.Api.Models;
using NBB.Api.ViewModels;
using NBB.Api.Services;
using System.Security.Cryptography;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _repository;

        public AuthenticationController(IConfiguration configuration, IUserRepository userRepository)
        {
            _configuration = configuration;
            _repository = userRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateToken([FromBody] UserLoginViewModel loginModel)
        {
            IActionResult response = Unauthorized();

            var userModel = new UserLogin 
            {
                UserName = loginModel.UserName,
                Password = HashPassword(loginModel.Password)
            };
            User user = Authenticate(userModel);

            if (user != null)
            {
                string tokenString = BuildToken(user);
                return Ok(new Token { TokenId = tokenString });
            }

            return response;
        }

        private string HashPassword(string password)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ServerSecret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Issuer"],
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private User Authenticate(UserLogin loginModel)
        {
            var user = _repository.Get(loginModel.UserName);

            if (user != null && user.Password == loginModel.Password)
            {
                return new User()
                {
                    UserName = loginModel.UserName,
                    Password = loginModel.Password
                };
            }

            return null;
        }

    }
}
