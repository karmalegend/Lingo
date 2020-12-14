using Lingo.Models;

namespace Lingo.Services.Interfaces
{
    public interface IUserService
    {
        public bool AddUser(UserModel user);
        public UserModel GetUserByUsername(string username);

        public bool AuthenticateUser(string basePw, string hash);
    }
}
