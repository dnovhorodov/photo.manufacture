using Photo.Manufacture.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Photo.Manufacture.Api.ApiModels
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }

        public static List<OrderItemDTO> FromOrderItems(IEnumerable<OrderItem> items)
            => items.Select(FromOrderItem).ToList();

        public static OrderItemDTO FromOrderItem(OrderItem item)
        {
            return new OrderItemDTO()
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
            };
        }
    }
}
