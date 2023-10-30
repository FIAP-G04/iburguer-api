using FIAP.Diner.Domain.ShoppingCarts;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Diner.Infrastructure.Data.Modules.ShoppingCarts;

public class ShoppingCartMapping : IEntityTypeConfiguration<ShoppingCart>
{
    public void Configure(EntityTypeBuilder<ShoppingCart> builder)
    {
        builder.ToTable("ShoppingCarts");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsId()
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(p => p.Customer)
               .HasColumnName("CustomerId")!
               .IsId()
               .IsRequired(false);

        builder.Property(p => p.Closed)
            .IsRequired();

       builder.HasMany(p => p.Items)
            .WithOne()
            .HasForeignKey(i => i.ShoppingCart)
            .OnDelete(DeleteBehavior.Cascade);
    }
}