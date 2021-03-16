using Photo.Manufacture.SharedKernel;
using Photo.Manufacture.SharedKernel.Interfaces;
using System;

namespace Photo.Manufacture.Core.Entities
{
    public class Product : BaseEntity, IAggregateRoot
    {
        public ProductType ProductType { get; }
        
        public float Width { get; set; }

        public string Description { get; set; }

        public Product(ProductType type, float width)
        {
            if (width <= 0)
            {
                throw new ArgumentException("Invalid product size", nameof(width));
            }

            this.ProductType = type;
            this.Width = width;
        }

        public Product(ProductType type, string description, float width)
            : this(type, width)
        {
            this.Description = description;
        }

        protected Product() { }

        public override string ToString() => $"{Id}: Width: {this.Width} - {this.ProductType?.Name ?? "NoValue"}";
    }
}
