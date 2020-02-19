using System;
using Acme.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Acme.CommonTests
{
    [TestClass]
    public class StringHandlerTest
    {
        [TestMethod]
        public void InsertSpacesTestValid()
        {
            // Arrange
            var source = "DragosHobjila";
            var expected = "Dragos Hobjila";

            // Act
            //var actual = StringHandler.InsertSpaces(source); // static class
            var actual = source.InsertSpaces(); // extension

            // Assert

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void InsertSacesTestWithExistingSpace()
        {
            // Arrange
            var source = "Dragos Hobjila";
            var expected = "Dragos Hobjila";

            // Act
            var actual = StringHandler.InsertSpaces(source);

            // Assert

            Assert.AreEqual(expected, actual);
        }
    }
}
