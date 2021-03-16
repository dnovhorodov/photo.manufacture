using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Photo.Manufacture.Core.Entities;

namespace Photo.Manufacture.Infrastructure.Data.Config
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.Id);

            builder
                .HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey("productTypeId")
                .IsRequired(true);
        }
    }
}
