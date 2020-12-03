using Lingo.Data.Interfaces;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepo _userrepo;

        public UserService(IUserRepo userRepo) {
            _userrepo = userRepo;
        }

        public bool addUser(userModel user) {
            _userrepo.addUser(user);
            return _userrepo.saveChanges();
        }

        public userModel getUserByUsername(string username)
        {
            return _userrepo.getUserByUsername(username);
        }

        public bool authenticateUser(string basePw, string hash) {
            return BCrypt.Net.BCrypt.Verify(basePw, hash);
        }
    }
}
