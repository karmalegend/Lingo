using Lingo.Models;

namespace Lingo.Data.Interfaces
{
    public interface IUserRepo
    {
        public void AddUser(UserModel user);

        public bool SaveChanges();

        public UserModel GetUserByUsername(string username);
    }
}
