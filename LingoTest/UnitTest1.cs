using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LingoTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            string t = BCrypt.Net.BCrypt.HashPassword("test1");
            Assert.IsTrue(BCrypt.Net.BCrypt.Verify("test1", t));
        }
    }
}
