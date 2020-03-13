using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GradeBook.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange

            var book = new InMemoryBook("");
            book.AddGrade(89.1);
            book.AddGrade(90.5);
            book.AddGrade(77.3);

            //Act
            var result = book.GetStatistics();
            //Assert

            Assert.AreEqual(85.6, result.Average, 1);
            Assert.AreEqual(90.5, result.High, 1);
            Assert.AreEqual(77.3, result.Low, 1);
        }
    }
}
