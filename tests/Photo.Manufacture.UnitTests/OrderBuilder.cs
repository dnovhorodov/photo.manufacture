using Photo.Manufacture.Core.Entities;

namespace Photo.Manufacture.UnitTests
{
    public class OrderBuilder
    {
        private Order order;

        public OrderBuilder PhotoBook(int quantity = 1)
        {
            this.Add(ProductBuilder.PhotoBook(), "Photo Book", quantity);

            return this;
        }

        public OrderBuilder Calendar(int quantity = 1)
        {
            this.Add(ProductBuilder.Calendar(), "Calendar", quantity);

            return this;
        }

        public OrderBuilder Canvas(int quantity = 1)
        {
            this.Add(ProductBuilder.Canvas(), "Canvas", quantity);

            return this;
        }

        public OrderBuilder Cards(int quantity = 1)
        {
            this.Add(ProductBuilder.Cards(), "Cards", quantity);

            return this;
        }

        public OrderBuilder Mug(int quantity = 1)
        {
            this.Add(ProductBuilder.Mug(), "Mug", quantity);

            return this;
        }

        public OrderBuilder OrderId(string orderId = "test-order-id")
        {
            this.order = new Order(orderId);

            return this;
        }

        private void Add(Product product, string name, int quantity)
        {
            this.order ??= this.OrderId().Build();
            this.order.AddOrderItem(product.Id, name, quantity);
        }

        public Order Build() => this.order ?? this.OrderId().Build();
    }
}
