using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lingo.Models
{
    public class userModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int gameSessionId {get;set;}

        // empty constructor to prevent EF from using the parametrized one and re hashing the hash.
        public userModel() { }

        public userModel(string username, string password) {
            this.Username = username;
            this.Password = BCrypt.Net.BCrypt.HashPassword(password,BCrypt.Net.BCrypt.GenerateSalt());
        }
    }
}
