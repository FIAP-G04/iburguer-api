using System.Linq.Expressions;
using FIAP.Diner.Domain.Customers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using FIAP.Diner.Domain.Common;
using FIAP.Diner.Infrastructure.Data.Configurations;

namespace FIAP.Diner.Infrastructure.Data.Modules.Customers;

public class CustomerMapping : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(c => new { c.Id });

        builder.Property(c => c.Id)
               .IsId()
               .HasColumnName("Id")
               .IsRequired();

        builder.Property(c => c.CPF)
               .IsCpf()
               .HasColumnName("CPF")
               .IsRequired();

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