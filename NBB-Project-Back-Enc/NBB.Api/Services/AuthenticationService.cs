using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using NBB.Api.Entities;
using NBB.Api.Models;
using System.Security.Cryptography;

namespace NBB.Api.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<UserManager<User>> _logger;

        public AuthenticationService(IDbContextFactory<NBBDBContext> dbContextFactory,
            IOptions<IdentityOptions> identityOptions,
            ILogger<UserManager<User>> logger)
        {
            var dbContext = dbContextFactory.CreateDbContext();
            var userStore = new UserStore<User>(dbContext);
            var passwordHasher = new PasswordHasher<User>();
            var userValidators = new List<IUserValidator<User>>();
            var passwordValidators = new List<IPasswordValidator<User>>();
            var lookupNormalizer = new UpperInvariantLookupNormalizer();
            var errorDescriber = new IdentityErrorDescriber();
            var serviceProvider = new ServiceCollection().BuildServiceProvider();
            _logger = logger;

            _userManager = new UserManager<User>(
                userStore,
                identityOptions,
                passwordHasher,
                userValidators,
                passwordValidators,
                lookupNormalizer,
                errorDescriber,
                serviceProvider,
                _logger);
        }

        public async Task<bool> AuthenticateUserAsync(string username, string password)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user == null)
            {
                return false;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);

            return passwordValid;
        }

        public string HashPassword(string password)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            using (var sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}