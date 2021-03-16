using Photo.Manufacture.Core.Entities;
using Photo.Manufacture.Core.Interfaces;
using Photo.Manufacture.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Photo.Manufacture.Core.Services
{
    public class BinPackingService : IBinPackingService
    {
        /// <summary>
        /// Lookup for stackable product types with max stack limit
        /// </summary>
        private readonly Dictionary<ProductType, (bool, int)> stackableProductTypes;
        private readonly IRepository<Product> productRepository;

        public BinPackingService(IRepository<Product> productRepository)
        {
            this.stackableProductTypes = ProductType
                .List()
                .ToDictionary(pt => pt, pt => (pt == ProductType.Mug, 4));

            this.productRepository = productRepository;
        }

        public async Task<float> CalculateRequiredBinWidth(IEnumerable<OrderItem> orderItems)
        {
            var products = await this.productRepository.ListAsync();

            var binWidth = orderItems.Aggregate(0.00f, func: (acc, item) =>
            {
                var product = products.First(p => p.Id == item.ProductId);
                var (isStackable, maxLimit) = this.stackableProductTypes[product.ProductType];

                return acc + isStackable switch
                {
                    true => product.Width * (int)Math.Ceiling((float)item.Quantity / maxLimit), // Find lower bound
                    false => product.Width * item.Quantity
                };
            });

            return binWidth;
        }
    }
}
