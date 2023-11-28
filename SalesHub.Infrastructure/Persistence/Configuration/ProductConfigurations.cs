using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SalesHub.Domain.Entities;

namespace SalesHub.Infrastructure.Persistence.Configuration;

public class ProductConfigurations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(c => c.Name)
            .HasMaxLength(125);

        builder.Property(c => c.Description);

        builder.Property(c => c.SKU)
            .HasMaxLength(20)
            .IsRequired();

        builder.OwnsMany(m => m.Orders, o => {
            o.WithOwner().HasForeignKey("ProductId");
        });
    }
}