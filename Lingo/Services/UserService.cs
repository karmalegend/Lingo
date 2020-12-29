using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services.Interfaces;

namespace Lingo.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepo _userrepo;

        public UserService(IUserRepo userRepo) {
            _userrepo = userRepo;
        }

        public bool AddUser(UserModel user) {
            if (GetUserByUsername(user.Username) != null)
            {
                return false;
            }
            _userrepo.AddUser(user);
            return _userrepo.SaveChanges();
        }

        public UserModel GetUserByUsername(string username)
        {
            return _userrepo.GetUserByUsername(username);
        }

        public bool AuthenticateUser(string basePw, string hash) {
            return BCrypt.Net.BCrypt.Verify(basePw, hash);
        }
    }
}
