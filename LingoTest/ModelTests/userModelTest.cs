using Lingo.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LingoTest.ModelTests
{
    [TestClass]
    public class userModelTest
    {
        [TestMethod]
        public void testHashingOnConstructor()
        {
            userModel user = new userModel("username", "password");
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("password",user.Password));
        }
    }
}
