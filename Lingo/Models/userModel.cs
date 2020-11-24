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

        public userModel(string username, string password) {
            this.Username = username;
            this.Password = BCrypt.Net.BCrypt.HashPassword(password);
        }
    }
}
