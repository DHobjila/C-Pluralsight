using System;
using ACM.BL;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ACM.BLTest
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void RetriveTest()
        {
            //--Arrange
            var productRepository = new ProductRepository();
            var expected = new Product(2)
            {
                ProductName = "Sunflowers",
                ProductDescription = "Assorted Size of 4 Bright Yellow Mini Sunflowers",
                CurrentPrice = 15.96M
            };

            //--Act
            var actual = productRepository.Retrieve(2);

            //--Assert
            Assert.AreEqual(expected.CurrentPrice, actual.CurrentPrice);
            Assert.AreEqual(expected.ProductDescription, actual.ProductDescription);
            Assert.AreEqual(expected.ProductName, actual.ProductName);
         
        }

        
    }
}
