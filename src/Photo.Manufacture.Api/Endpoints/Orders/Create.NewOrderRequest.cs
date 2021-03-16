using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Photo.Manufacture.Api.Endpoints.Orders
{
    public class NewOrderRequest
    {
        [Required]
        public string OrderId { get; set; }

        [Required]
        public IEnumerable<OrderItemModel> OrderItems { get; set; }
    }
}