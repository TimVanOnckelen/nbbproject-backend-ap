public interface IAuthenticationService
{
    Task<bool> AuthenticateUserAsync(string username, string password);
    string HashPassword(string password);
}