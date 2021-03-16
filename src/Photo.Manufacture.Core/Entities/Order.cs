using Photo.Manufacture.SharedKernel;
using Photo.Manufacture.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Photo.Manufacture.Core.Entities
{
    public class Order : BaseEntity, IAggregateRoot
    {
        public string OrderId { get; }

        public List<OrderItem> OrderItems { get; }

        public float RequiredBinWidth { get; private set; }

        public Order(string orderId)
        {
            this.OrderId = orderId;
            this.OrderItems = new List<OrderItem>();
        }

        protected Order() { }

        public void AddOrderItem(int productId, string productName, int quantity = 1)
        {
            var existingOrderForProduct = this.OrderItems.SingleOrDefault(o => o.ProductId == productId);

            if (existingOrderForProduct != null)
            {
                existingOrderForProduct.Quantity += quantity;
            }
            else
            {
                var orderItem = new OrderItem(productId, productName, quantity);
                this.OrderItems.Add(orderItem);
            }
        }

        public void SetRequiredBinWidth(float width)
        {
            if (this.OrderItems.Count > 0 && width <= 0)
                throw new ArgumentException("Required bin width should be positive real number", nameof(width));

            this.RequiredBinWidth = width;
        }
    }
}
