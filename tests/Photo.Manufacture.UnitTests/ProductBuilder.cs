using Photo.Manufacture.Core.Entities;
using System.Collections.Generic;

namespace Photo.Manufacture.UnitTests
{
    public static class ProductBuilder
    {
        public static Product PhotoBook() => new(ProductType.PhotoBook, 19)
        {
            Id = 1,
            Description = "Photo Book",
        };

        public static Product Calendar() => new(ProductType.Calendar, 10)
        {
            Id = 2,
            Description = "Calendar",
        };

        public static Product Canvas() => new(ProductType.Canvas, 16)
        {
            Id = 3,
            Description = "Canvas",
        };

        public static Product Cards() => new(ProductType.Cards, 4.7f)
        {
            Id = 4,
            Description = "Cards",
        };

        public static Product Mug() => new(ProductType.Mug, 9.4f)
        {
            Id = 5,
            Description = "Mug",
        };

        public static List<Product> List() => new()
        {
                PhotoBook(),
                Calendar(),
                Canvas(),
                Cards(),
                Mug(),
            };
    }
}
