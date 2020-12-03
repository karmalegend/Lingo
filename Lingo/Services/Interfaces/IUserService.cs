using Lingo.DTO;
using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Services.Interfaces
{
    public interface IUserService
    {
        public bool addUser(userModel user);
        public userModel getUserByUsername(string username);

        public bool authenticateUser(string basePw, string hash);
    }
}
