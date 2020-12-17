using Lingo.Data.Interfaces;
using Lingo.Models;
using Lingo.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LingoTest.ServicesTests
{
    [TestClass]
    public class UserServiceTest
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepo> _userRepo = new Mock<IUserRepo>();

        public UserServiceTest()
        {
            _userService = new UserService(_userRepo.Object);
        }

        [TestMethod]
        public void AddUserTest()
        {
            UserModel user = new UserModel();
            _userRepo.Setup(ur => ur.SaveChanges()).Returns(true);
            
            Assert.IsTrue(_userService.AddUser(user));
        }

        [TestMethod]
        public void GetUserByUsernameFoundTest()
        {
            UserModel user = new UserModel(){Username = "username"};
            _userRepo.Setup(ur => ur.GetUserByUsername(user.Username)).Returns(new UserModel());
            
           Assert.IsInstanceOfType(_userService.GetUserByUsername(user.Username),typeof(UserModel));
        }

        [TestMethod]
        public void GetUserByUsernameNotFoundTest()
        {
            UserModel user = new UserModel() { Username = "username" };
            _userRepo.Setup(ur => ur.GetUserByUsername(user.Username)).Returns((UserModel) null);

            Assert.AreEqual(_userService.GetUserByUsername(user.Username),null);
        }

        [TestMethod]
        public void AuthenticateUserTestFalse()
        {
            UserModel user = new UserModel( "username",  "password");
            Assert.IsFalse(_userService.AuthenticateUser("admin", user.Password));
        }

        [TestMethod]
        public void AuthenticateUserTestTrue()
        {
            UserModel user = new UserModel("username", "password");
            Assert.IsTrue(_userService.AuthenticateUser("password", user.Password));
        }
    }
}
