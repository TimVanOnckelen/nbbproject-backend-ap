using NBB.Api.Models;

namespace NBB.Api.Services
{
    public class InMemoryUserDB : IUserRepository
    {
        private readonly List<User> _users;

        public InMemoryUserDB()
        {
            _users = new List<User>
            {
                new User()
                {
                    Id = "1",
                    FirstName = "Test",
                    LastName = "Test",
                    UserName = "test",
                    // T3st!
                    Password = "eJUxYVLe2pCOCoZTSm09AIiw5Mt/7fJ39oysdwwQ8ds="
                }
            };
        }


        public User Get(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName == userName);
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public void Update(User user)
        {
            var current = Get(user.UserName);
            var updated = user;
            if (current != null && updated != null)
            {
                _users.Remove(current);
                _users.Add(updated);
            }
        }
    }
}
