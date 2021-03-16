using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Photo.Manufacture.Core.Entities;
using Photo.Manufacture.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Photo.Manufacture.Api.ApiModels;
using Microsoft.AspNetCore.Http;

namespace Photo.Manufacture.Api.Endpoints.Orders
{
    public class List : BaseAsyncEndpoint
        .WithoutRequest
        .WithResponse<List<OrderResponse>>
    {
        private readonly IRepository<Order> orderRepository;

        public List(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet("/Orders")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation(
            Summary = "Gets a list of all Orders",
            Description = "Gets a list of all Orders",
            OperationId = "Order.List",
            Tags = new[] { "OrderEndpoints" })
        ]
        public override async Task<ActionResult<List<OrderResponse>>> HandleAsync(CancellationToken cancellationToken)
        {
            var items = (await this.orderRepository.ListAsync())
                .Select(order => new OrderResponse
                {
                    OrderId = order.Id,
                    OrderItems = OrderItemDTO.FromOrderItems(order.OrderItems),
                    RequiredBinWidth = order.RequiredBinWidth,
                });

            return Ok(items);
        }
    }
}
