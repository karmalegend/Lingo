using Lingo.Data.Interfaces;
using Lingo.Models;
using System.Linq;

namespace Lingo.Data.Repository
{
    public class UserRepository : IUserRepo
    {
        private readonly LingoContext _context;

        public UserRepository(LingoContext lingoContext) { 
            _context = lingoContext;
        }

        public void AddUser(UserModel user)
        {
            _context.Users.Add(user);
        }

        public UserModel GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username.Equals(username));
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}
