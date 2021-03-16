using Photo.Manufacture.Api.ApiModels;
using System.Collections.Generic;

namespace Photo.Manufacture.Api.Endpoints.Orders
{
    public class OrderResponse
    {
        public int OrderId { get; set; }

        public IEnumerable<OrderItemDTO> OrderItems { get; set; }
        public float RequiredBinWidth { get; set; }
    }
}