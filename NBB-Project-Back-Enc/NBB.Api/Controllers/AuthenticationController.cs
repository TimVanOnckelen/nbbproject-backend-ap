﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using NBB.Api.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public TokenController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Token), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult CreateToken([FromBody] UserLogin loginModel)
        {
            IActionResult response = Unauthorized();

            User user = Authenticate(loginModel);

            if (user != null)
            {
                string tokenString = BuildToken(user);
                return Ok(new Token { tokenId = tokenString });
            }

            return response;
        }

        private string BuildToken(User userModel)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:ServerSecret"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                configuration["JWT:Issuer"],
                configuration["JWT:Issuer"],
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
