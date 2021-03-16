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
    public class GetById : BaseAsyncEndpoint
        .WithRequest<int>
        .WithResponse<OrderResponse>
    {
        private readonly IRepository<Order> orderRepository;

        public GetById(IRepository<Order> orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpGet("/Orders/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(
            Summary = "Gets a single Order",
            Description = "Gets a single Order by Id",
            OperationId = "Orders.GetById",
            Tags = new[] { "OrderEndpoints" })
        ]
        public override async Task<ActionResult<OrderResponse>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            var order = await this.orderRepository.GetByIdAsync(id);

            if (order is null)
                return NotFound();

            var response = new OrderResponse
            {
                OrderId = order.Id,
                OrderItems = OrderItemDTO.FromOrderItems(order.OrderItems),
                RequiredBinWidth = order.RequiredBinWidth,
            };

            return Ok(response);
        }
    }
}
