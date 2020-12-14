using Lingo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LingoTest.ModelTests
{
    [TestClass]
    public class UserModelTest
    {
        [TestMethod]
        public void testHashingOnConstructor()
        {
            UserModel user = new UserModel("username", "password");
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("password",user.Password));
        }
    }
}
