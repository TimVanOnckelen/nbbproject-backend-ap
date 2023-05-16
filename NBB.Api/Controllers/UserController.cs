using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NBB.Api.Models;
using NBB.Api.Services;
using NBB.Api.ViewModels;
using System.Security.Cryptography;

namespace NBB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _repository;

        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(User), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] UserCreateViewModel user)
        {
            var existingUser = _repository.Get(user.UserName);
            if (existingUser != null) return BadRequest();

            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Password = HashPassword(user.Password)
            };

            _repository.Add(newUser);
            
            return CreatedAtAction(nameof(Get), new {newUser.UserName}, newUser);
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

        [HttpGet("{userName}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult Get(string userName)
        {
            var user = _repository.Get(userName);
            if (user == null) return NotFound();

            user.Password = "";

            return Ok(user);
        }

        [HttpPut]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult Put([FromBody] UserUpdateViewModel user)
        {   var userName = user.UserName;
            var userToUpdate = _repository.Get(userName);
            if (userToUpdate == null) return NotFound();

            var updatedUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = userToUpdate.UserName,
                Password = HashPassword(user.Password)
            };

            _repository.Update(updatedUser);

            return Ok(updatedUser);
        }

        [HttpDelete("{userName}")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public IActionResult Delete(string userName)
        {
            var userToDelete = _repository.Get(userName);
            if (userToDelete == null) return NotFound();

            _repository.Delete(userToDelete);

            return Ok(userToDelete);
        }
    }
}
