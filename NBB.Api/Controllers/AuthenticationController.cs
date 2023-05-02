using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using NBB.Api.Models;
using NBB.Api.ViewModels;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
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
                Password = loginModel.Password
            };
            User user = Authenticate(userModel);

            if (user != null)
            {
                string tokenString = BuildToken(user);
                return Ok(new Token { TokenId = tokenString });
            }

            return response;
        }

        private string BuildToken(User userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:ServerSecret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["JWT:Issuer"],
                _configuration["JWT:Issuer"],
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        private User Authenticate(UserLogin loginModel)
        {
            if (loginModel.UserName == "Pablo" && loginModel.Password == "QWERTY")
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
