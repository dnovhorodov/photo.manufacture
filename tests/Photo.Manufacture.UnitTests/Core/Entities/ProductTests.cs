using Photo.Manufacture.Core.Entities;
using System;
using Xunit;

namespace Photo.Manufacture.UnitTests.Core.Entities
{
    public class ProductTests
    {
        [Fact]
        public void OnCreateWithZeroOrNegativeWidthShouldThrow()
        {
            Assert.Throws<ArgumentException>(() => new Product(ProductType.PhotoBook, 0));
            Assert.Throws<ArgumentException>(() => new Product(ProductType.PhotoBook, -1));
        }
    }
}
