using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Renters;
using Motorent.Domain.Renters.Enums;
using Motorent.Domain.Renters.ValueObjects;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Infrastructure.Renters.Persistence.Configuration;

internal sealed class RenterConfiguration : IEntityTypeConfiguration<Renter>
{
    public void Configure(EntityTypeBuilder<Renter> builder)
    {
        builder.HasKey(r => r.Id);

        builder.HasIndex(r => r.UserId)
            .IsUnique();

        builder.HasIndex(r => r.Cnpj)
            .IsUnique();

        builder.Property(r => r.Id)
            .ValueGeneratedNever()
            .HasMaxLength(26)
            .HasConversion(v => v.Value.ToString(), v => new RenterId(Ulid.Parse(v)));

        builder.Property(r => r.UserId)
            .ValueGeneratedNever()
            .HasMaxLength(26)
            .HasConversion(v => v.Value.ToString(), v => new UserId(Ulid.Parse(v)));

        builder.Property(r => r.Cnpj)
            .HasMaxLength(RenterConstants.CnpjMaxLength)
            .HasConversion(v => v.Value, v => Cnpj.Create(v).Value);
        
        builder.Property(u => u.Name)
            .HasConversion(v => v.Value, v => new Name(v))
            .HasMaxLength(RenterConstants.NameMaxLength);

        builder.Property(u => u.Birthdate)
            .HasConversion(v => v.Value, v => Birthdate.Create(v).Value);

        builder.OwnsOne(u => u.Cnh, b =>
        {
            b.HasIndex(c => c.Number)
                .IsUnique();

            b.Property(c => c.Number)
                .HasColumnName("cnh_number")
                .HasConversion(v => v.Value, v => CnhNumber.Create(v).Value);

            b.Property(c => c.ExpirationDate)
                .HasColumnName("cnh_exp_date");
            
            b.Property(c => c.Category)
                .HasColumnName("cnh_category")
                .HasConversion(v => v.Name, v => CnhCategory.FromName(v, true))
                .HasMaxLength(RenterConstants.CategoryMaxLength);
        });
        
        builder.ToTable(RenterConstants.TableName);
    }
}