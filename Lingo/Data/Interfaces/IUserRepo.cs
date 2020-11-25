using Lingo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Data.Interfaces
{
    public interface IUserRepo
    {
        public void addUser(userModel user);

        public bool saveChanges();

        public userModel getUserByUsername(string username);
    }
}
