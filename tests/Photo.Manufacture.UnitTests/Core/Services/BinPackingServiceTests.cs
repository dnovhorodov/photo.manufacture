using Moq;
using Photo.Manufacture.Core.Entities;
using Photo.Manufacture.Core.Services;
using Photo.Manufacture.SharedKernel.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace Photo.Manufacture.UnitTests.Core.Services
{
    public class BinPackingServiceTests
    {
        [Fact]
        public async Task ShouldCalculateItemsWithoutStacking()
        {
            // Arrange
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(r => r.ListAsync()).ReturnsAsync(ProductBuilder.List());

            var order = new OrderBuilder()
                .PhotoBook(1)
                .Calendar(2)
                .Canvas(1)
                .Build();

            // Act
            var sut = new BinPackingService(repo.Object);
            var actualWidth = await sut.CalculateRequiredBinWidth(order.OrderItems);

            // Assert
            Assert.Equal(55, actualWidth);
        }

        [Fact]
        public async Task ShouldCalculateItemsWithStacking()
        {
            // Arrange
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(r => r.ListAsync()).ReturnsAsync(ProductBuilder.List());

            var order = new OrderBuilder()
                .PhotoBook(1)
                .Calendar(2)
                .Mug(2)
                .Build();

            // Act
            var sut = new BinPackingService(repo.Object);
            var actualWidth = await sut.CalculateRequiredBinWidth(order.OrderItems);

            // Assert
            Assert.Equal(48.4f, actualWidth);
        }

        [Fact]
        public async Task ShouldCalculateItemsWithStackingInUpperLimit()
        {
            // Arrange
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(r => r.ListAsync()).ReturnsAsync(ProductBuilder.List());

            var order = new OrderBuilder()
                .PhotoBook(1)
                .Calendar(2)
                .Mug(4)
                .Build();

            // Act
            var sut = new BinPackingService(repo.Object);
            var actualWidth = await sut.CalculateRequiredBinWidth(order.OrderItems);

            // Assert
            Assert.Equal(48.4f, actualWidth);
        }

        [Fact]
        public async Task ShouldCalculateItemsExceedingUpperLimit()
        {
            // Arrange
            var repo = new Mock<IRepository<Product>>();
            repo.Setup(r => r.ListAsync()).ReturnsAsync(ProductBuilder.List());

            var order = new OrderBuilder()
                .Calendar(1)
                .Canvas(2)
                .Mug(7)
                .Build();

            // Act
            var sut = new BinPackingService(repo.Object);
            var actualWidth = await sut.CalculateRequiredBinWidth(order.OrderItems);

            // Assert
            Assert.Equal(60.8f, actualWidth);
        }
    }
}
