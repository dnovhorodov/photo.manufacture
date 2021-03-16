using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Photo.Manufacture.Core.Entities;

namespace Photo.Manufacture.Infrastructure.Data.Config
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Id)
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired(true);

            builder.Property(t => t.Name)
                .HasMaxLength(100)
                .IsRequired(true);
        }
    }
}
