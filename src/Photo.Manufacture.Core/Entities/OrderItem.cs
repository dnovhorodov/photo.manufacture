using Photo.Manufacture.SharedKernel;

namespace Photo.Manufacture.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public OrderItem(int productId, string productName, int quantity)
        {
            this.ProductId = productId;
            this.ProductName = productName;
            this.Quantity = quantity;
        }

        protected OrderItem()
        {

        }
    }
}
