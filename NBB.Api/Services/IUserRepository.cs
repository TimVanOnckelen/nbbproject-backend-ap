using NBB.Api.Models;

namespace NBB.Api.Services
{
    public interface IUserRepository
    {
        User Get(string userName);
        void Add(User user);
        void Delete(User user);
        void Update (User user);
    }
}
