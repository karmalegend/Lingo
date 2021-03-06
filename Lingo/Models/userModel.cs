﻿namespace Lingo.Models
{
    public class UserModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int GameSessionId {get;set;}

        // empty constructor to prevent EF from using the parametrized one and re hashing the hash.
        public UserModel() { }

        public UserModel(string username, string password) {
            this.Username = username;
            this.Password = BCrypt.Net.BCrypt.HashPassword(password,BCrypt.Net.BCrypt.GenerateSalt());
        }
    }
}
