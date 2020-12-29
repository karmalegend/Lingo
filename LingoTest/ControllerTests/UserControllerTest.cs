using System;
using Lingo.Controllers;
using Lingo.DTO;
using Lingo.Models;
using Lingo.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
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

        [TestMethod]
        public void RegisterTest()
        {
            UserDto dto = new UserDto() {Password = "password", Username = "username"};
            //https://stackoverflow.com/questions/7827053/moq-mock-method-with-out-specifying-input-parameter
            //as the BCrypt will always generate a unique hash we can't give it an object instance here.
            _userService.Setup(us => us.AddUser(It.IsAny<UserModel>())).Returns(true);
            OkObjectResult result =_userController.Register(dto) as OkObjectResult;

            Assert.IsInstanceOfType(result,typeof(OkObjectResult));
            Assert.AreEqual($"User with username : {dto.Username} created.",result.Value);
        }

        [TestMethod]
        public void RegisterTestUsernameTaken()
        {
            _userService.Setup(us => us.AddUser(It.IsAny<UserModel>())).Returns(false);
            ConflictObjectResult result = _userController.Register(new UserDto() { Password = "password", Username = "username" }) as ConflictObjectResult;

            Assert.IsInstanceOfType(result, typeof(ConflictObjectResult));
            Assert.AreEqual("Username already taken", result.Value);
        }

        [TestMethod]
        public void LoginTest()
        {
            UserDto reqBody = new UserDto(){Username = "username",Password = "password"};

            _configuration.SetupGet(con => con[It.Is<String>(s=>s == "Jwt_Key")]).Returns("minimumSixteenCharacters");
            _configuration.SetupGet(con => con[It.Is<String>(s => s == "Jwt_Issuer")]).Returns("Issuer");

            _userService.Setup(us => us.GetUserByUsername(reqBody.Username))
                .Returns(new UserModel(reqBody.Username, reqBody.Password));

            var result = _userController.Login(reqBody);
            Assert.IsInstanceOfType(result,typeof(OkObjectResult));
        }

        [TestMethod]
        public void LoginTestInvalidPassword()
        {
            UserDto reqBody = new UserDto() { Username = "username", Password = "password" };

            _userService.Setup(us => us.GetUserByUsername(reqBody.Username))
                .Returns(new UserModel(reqBody.Username, "differentPassword"));

            var result = _userController.Login(reqBody);
            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public void LoginTestUserNotFound()
        {
            UserDto reqBody = new UserDto();

            _userService.Setup(us => us.GetUserByUsername(reqBody.Username))
                .Returns((UserModel) null);

            var result = _userController.Login(reqBody);

            Assert.IsInstanceOfType(result,typeof(UnauthorizedResult));
        }
    }
}
