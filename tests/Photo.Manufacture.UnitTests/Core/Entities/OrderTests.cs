using System;
using System.Linq;
using Xunit;

namespace Photo.Manufacture.UnitTests.Core.Entities
{
    public class OrderTests
    {
        [Fact]
        public void OnCreateOrderIdShouldBeProvided()
        {
            var orderId = "some-order-id";
            var order = new OrderBuilder()
                .OrderId(orderId)
                .Build();

            Assert.Equal(orderId, order.OrderId);
        }

        [Fact]
        public void OnCreateOrderItemsShouldNotBeNull()
        {
            var order = new OrderBuilder().Build();
            Assert.Empty(order.OrderItems);
        }

        [Fact]
        public void OnAddOrderItemQuantityAndProductShouldBeCorect()
        {
            var order = new OrderBuilder()
                .PhotoBook(1)
                .Canvas(2)
                .Build();

            Assert.Equal(2, order.OrderItems.Count);
            Assert.Equal(1, order.OrderItems.Single(item => item.ProductId == 1).Quantity);
            Assert.Equal(2, order.OrderItems.Single(item => item.ProductId == 3).Quantity);
        }

        [Fact]
        public void OnAddOrderItemWithTheSameProductIdQuantityShouldBeIncreased()
        {
            var order = new OrderBuilder()
                .PhotoBook(1)
                .PhotoBook(2)
                .PhotoBook(3)
                .Build();

            Assert.Single(order.OrderItems);
            Assert.Equal(6, order.OrderItems.Single(item => item.ProductId == 1).Quantity);
        }

        [Fact]
        public void OnAddOrderItemsCreateBinWidthShouldBeZero()
        {
            var order = new OrderBuilder()
                .Calendar(1)
                .Canvas(1)
                .Mug(2)
                .Build();

            Assert.Equal(0, order.RequiredBinWidth);
        }

        [Fact]
        public void OnSetRequiredBinWidthWithNegativeOrZeroShouldThrowWhenOrderItemsNotEmpty()
        {
            var order = new OrderBuilder()
                .Calendar(1)
                .Build();

            Assert.Throws<ArgumentException>(() => order.SetRequiredBinWidth(-1));
            Assert.Throws<ArgumentException>(() => order.SetRequiredBinWidth(0));
        }
    }
}
