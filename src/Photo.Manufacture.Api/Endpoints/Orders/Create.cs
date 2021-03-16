using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Photo.Manufacture.SharedKernel.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Photo.Manufacture.Core.Entities;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Photo.Manufacture.Core.Interfaces;

namespace Photo.Manufacture.Api.Endpoints.Orders
{
    public class Create : BaseAsyncEndpoint
        .WithRequest<NewOrderRequest>
        .WithResponse<OrderResponse>
    {
        private readonly IRepository<Product> productRepository;
        private readonly IRepository<Order> orderRepository;
        private readonly IBinPackingService binPackingService;

        public Create(
            IRepository<Product> productRepository, 
            IRepository<Order> orderRepository,
            IBinPackingService binPackingService)
        {
            this.productRepository = productRepository;
            this.orderRepository = orderRepository;
            this.binPackingService = binPackingService;
        }

        [HttpPost("/Orders")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [SwaggerOperation(
            Summary = "Creates a new Order",
            Description = "Creates a new Order",
            OperationId = "Order.Create",
            Tags = new[] { "OrderEndpoints" })
        ]
        public override async Task<ActionResult<OrderResponse>> HandleAsync(NewOrderRequest request, CancellationToken cancellationToken)
        {
            // In bigger projects I keep validations separate from endpoints w FluentValidation
            //
            var products = await this.productRepository.ListAsync();
            var productItems = request.OrderItems
                ?.Join(
                    products,
                    orderItem => orderItem.ProductId,
                    product => product.Id,
                    (orderItem, product) => new
                    {
                        productId = orderItem.ProductId,
                        productName = product.ProductType.ToString(),
                        quantity = orderItem.Quantity,
                    }
                ).ToList();

            var order = new Order(request.OrderId);
            productItems?.ForEach(item => order.AddOrderItem(item.productId, item.productName, item.quantity));
            var binWidth = await this.binPackingService.CalculateRequiredBinWidth(order.OrderItems);
            order.SetRequiredBinWidth(binWidth);

            var createdItem = await this.orderRepository.AddAsync(order);

            // Assume running in IIS
            // Better approach to use CreatedAtAction
            //
            string uri = $"https://localhost:44366/Orders/{createdItem.Id}";
            return createdItem?.Id != 0 ? this.Created(uri, createdItem) : BadRequest();
        }
    }
}
