using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Rent.Domain.Entities;

namespace Rent.Infra.Mappings;
public class TransactionMap : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .UseIdentityColumn()
            .HasColumnName("id")
            .HasColumnType("BIGINT");

        builder.Property(x => x.Manager)
            .UseIdentityColumn()
            .HasColumnName("manager")
            .HasColumnType("BIGINT");

        builder.Property(x => x.DeliveryMan)
            .UseIdentityColumn()
            .HasColumnName("deliveryMan")
            .HasColumnType("BIGINT");
        
        builder.Property(x => x.Motorcycle)
            .UseIdentityColumn()
            .HasColumnName("motorcycle")
            .HasColumnType("BIGINT");

        builder.Property(x => x.Plan)
            .UseIdentityColumn()
            .HasColumnName("motorcycle")
            .HasColumnType("BIGINT");

        builder.Property(x => x.StartDate)
            .UseIdentityColumn()
            .HasColumnName("startDate")
            .HasColumnType("TIMESTAMP");

        builder.Property(x => x.EndDate)
            .UseIdentityColumn()
            .HasColumnName("endDate")
            .HasColumnType("TIMESTAMP");

        builder.Property(x => x.Price)
            .UseIdentityColumn()
            .HasColumnName("endDate")
            .HasColumnType("Decimal");
    }
}