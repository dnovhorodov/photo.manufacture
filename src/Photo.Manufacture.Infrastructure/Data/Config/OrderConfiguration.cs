using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Photo.Manufacture.Core.Entities;

namespace Photo.Manufacture.Infrastructure.Data.Config
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            builder
                .HasMany(o => o.OrderItems)
                .WithOne()
                .IsRequired();
        }
    }
}
