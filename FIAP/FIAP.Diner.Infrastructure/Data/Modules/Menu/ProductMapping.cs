using FIAP.Diner.Domain.Menu;
using FIAP.Diner.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FIAP.Diner.Infrastructure.Data.Modules.Menu;

public class ProductMapping : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .IsId()
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(p => p.Name)
               .HasMaxLength(40)
               .IsRequired();

        builder.Property(p => p.Description)
            .HasMaxLength(300)
            .IsRequired();

        builder.Property(p => p.Category)
               .IsEnum()
               .IsRequired();

        builder.Property(c => c.Price)
            .IsMoney()
            .HasColumnName("Price")
            .IsRequired();

        builder.OwnsOne(p => p.PreparationTime, time =>
        {
            time.Property(c => c.Minutes)
                .HasColumnName("PreparationTime")
                .IsRequired();
        });

        builder.Property(p => p.Enabled)
            .IsRequired();

        builder.HasMany(p => p.Images)
            .WithOne()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}