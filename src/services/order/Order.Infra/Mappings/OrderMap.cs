using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Order.Domain.Entities;

namespace Order.Infra.Mappings
{

        public class OrderMap : IEntityTypeConfiguration<OrderEntity>
        {
                public void Configure(EntityTypeBuilder<OrderEntity> builder)
                {
                        builder.ToTable("Orders");

                        builder.HasKey(x => x.Id);
                        builder.Property(x => x.Id)
                                .UseIdentityColumn()
                                .HasColumnName("id")
                                .HasColumnType("BIGINT");

                        builder.Property(x => x.Price)
                                .HasColumnName("price")
                                .HasColumnType("DECIMAL");

                        builder.Property(x => x.Situation)
                                .HasMaxLength(20)
                                .HasColumnName("situation")
                                .HasColumnType("VARCHAR(20)");
                }
        }
}