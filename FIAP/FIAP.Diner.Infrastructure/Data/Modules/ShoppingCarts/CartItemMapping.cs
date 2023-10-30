using FIAP.Diner.Domain.ShoppingCarts;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FIAP.Diner.Infrastructure.Data.Modules.ShoppingCarts;

public class CartItemMapping : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.ToTable("CartItems");

        builder.HasKey(c => c.Id);

        builder.Property(i => i.Id)
            .IsId()
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(i => i.Product)
            .IsId()
            .HasColumnName("ProductId")
            .IsRequired();

        builder.Property(c => c.Price)
            .IsMoney()
            .HasColumnName("Price")
            .IsRequired();

        builder.Property(o => o.Quantity)
            .HasConversion(
                quantity => quantity.Value,
                value => new Quantity(value))
            .HasColumnName("Quantity")
            .IsRequired();
    }
}