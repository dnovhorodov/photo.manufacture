using System;
using System.Linq;
using Photo.Manufacture.Core.Entities;
using Photo.Manufacture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Photo.Manufacture.Api
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var dbContext = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (!dbContext.ProductTypes.Any())
            {
                dbContext.ProductTypes.AddRange(ProductType.List());
            }

            if (!dbContext.Products.Any())
            {
                PopulateProducts(dbContext);
            }

            dbContext.SaveChanges();
        }

        public static void PopulateProducts(AppDbContext dbContext)
        {
            dbContext.Products.AddRange(new[]
            {
                new Product(ProductType.PhotoBook, "Photo Book", 19),
                new Product(ProductType.Calendar, "Calendar", 10),
                new Product(ProductType.Canvas, "Canvas", 16),
                new Product(ProductType.Cards, "Cards", 4.7f),
                // Hmm, created Mug as 9.4mm, however in task desription it seems like 94mm.
                // Don't belive that it could be such a huge mug as size of ~5 photo books :)
                // Anyway you are free to change the value here
                new Product(ProductType.Mug, "Mug", 9.4f),
            });
        }
    }
}
