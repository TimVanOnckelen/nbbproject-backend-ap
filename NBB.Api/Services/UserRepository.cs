using Microsoft.EntityFrameworkCore;
using NBB.Api.Entities;
using NBB.Api.Models;

namespace NBB.Api.Services
{
    public class UserRepository : IUserRepository
    {
        private NBBDBContext _context;

        public UserRepository(NBBDBContext context)
        {
            _context = context;
        }


        public User Get(string userName)
        {
            var user = _context.User.FirstOrDefault(u => u.UserName == userName);

            return user;
        }

        public void Add(User user)
        {
            var validateUser = _context.User.FirstOrDefault(u => u.Id == user.Id);

            if (validateUser == null)
                _context.User.Add(user);

            _context.SaveChanges();
        }

        public void Delete(User user)
        {
            _context.Remove(user);
            _context.SaveChanges();
        }

        public void Update(User user)
        {
            var toUpdate = _context.User.FirstOrDefault(x => x.Id == user.Id);
            _context.User.Update(toUpdate);
            _context.SaveChanges();
        }
    }
}
