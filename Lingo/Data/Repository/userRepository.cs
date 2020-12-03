using Lingo.Data.Interfaces;
using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data.Repository
{
    public class userRepository : IUserRepo
    {
        private readonly LingoContext _context;

        public userRepository(LingoContext lingoContext) { 
            _context = lingoContext;
        }

        public void addUser(userModel user)
        {
            _context.users.Add(user);
        }

        public userModel getUserByUsername(string username)
        {
            return _context.users.Where(u => u.Username.Equals(username)).FirstOrDefault();
        }

        public bool saveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}
