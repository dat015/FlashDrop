using FlashDrop.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlashDrop.Catalog.Infrastructure.Persistence.Configurations
{
    public class ProductSpecConfiguration : IEntityTypeConfiguration<ProductSpecification>
    {
        public void Configure(EntityTypeBuilder<ProductSpecification> builder)
        {
            builder.ToTable("ProductSpecifications");

            builder.HasKey(ps => ps.Id);

            builder.Property(ps => ps.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ps => ps.Value)
                .IsRequired()
                .HasMaxLength(500);

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(ps => ps.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
