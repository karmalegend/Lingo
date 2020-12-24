using Lingo.Controllers;
using Lingo.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace LingoTest.ControllerTests
{
    [TestClass]
    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserService> _userService = new Mock<IUserService>();
        private readonly Mock<IConfiguration> _configuration = new Mock<IConfiguration>();

        public UserControllerTest()
        {
            _userController = new UserController(_userService.Object, _configuration.Object);
        }
    }
}
