using FIAP.Diner.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FIAP.Diner.Domain.Common;

namespace FIAP.Diner.Infrastructure.Data.Modules.Customers;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.Property(c => c.Id)
            .HasConversion(new ValueConverter<CustomerId, Guid>(
                v => v.Value,
                v => new CustomerId(v)
            ))
            .IsRequired();

        builder.HasKey(c => c.Id);

        builder.OwnsOne(c => c.CPF, cpf =>
        {
            cpf.Property(c => c.Number)
                .HasColumnName("CPF")
                .IsRequired();
        });

        builder.OwnsOne(c => c.Email)
            .Property(x => x.Value)
            .HasColumnName("Email")
            .HasMaxLength(60)
            .IsRequired();

        builder.OwnsOne(c => c.Name, name =>
        {
            name.Property(n => n.FirstName)
                .HasMaxLength(30)
                .HasColumnName("FirstName")
                .IsRequired();

            name.Property(n => n.LastName)
                .HasMaxLength(80)
                .HasColumnName("LastName")
                .IsRequired();
        });

        builder.Property(c => c.RegistrationDate)
            .IsRequired();

        builder.Property(c => c.LastUpdated)
            .IsRequired();
    }
}