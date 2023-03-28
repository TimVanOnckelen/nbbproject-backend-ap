using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using NBB.Api.Data;
using NBB.Api.Models;

public class AuthenticationService
{
    private readonly UserManager<User> _userManager;

    public AuthenticationService(IDbContextFactory<NbbDbContext<User>> dbContextFactory)
    {
        var dbContext = dbContextFactory.CreateDbContext();
        var userStore = new UserStore<User>(dbContext);
        _userManager = new UserManager<User>(userStore);
    }

    public async Task<bool> AuthenticateUserAsync(string username, string password)
    {
        var user = await _userManager.FindAsync(username, password);

        return user != null;
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
