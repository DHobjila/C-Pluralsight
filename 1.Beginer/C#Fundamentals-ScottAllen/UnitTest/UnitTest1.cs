using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Method1()
        {
            var x = 5;
            var y = 2;
            var actual = x * y;
            var expected = 7;

            Assert.AreEqual(expected, actual);
        }
    }
}
