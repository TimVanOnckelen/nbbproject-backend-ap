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


        public User Get(string userName) => 
            _context.User.FirstOrDefault(u => u.UserName == userName);

        public void Add(User user)
        {
       
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
