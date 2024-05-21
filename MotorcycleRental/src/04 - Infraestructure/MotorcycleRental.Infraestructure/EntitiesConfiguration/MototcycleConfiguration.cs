using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Infraestructure.EntitiesConfiguration
{
    public class MototcycleConfiguration : IEntityTypeConfiguration<Motorcycle>
    {
        public void Configure(EntityTypeBuilder<Motorcycle> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Model).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.Property(p => p.Year).HasColumnType("numeric(18, 2)").IsRequired();
            builder.Property(p => p.Plate).HasColumnType("varchar").HasMaxLength(20).IsRequired();
            builder.HasIndex(p => p.Plate).IsUnique();
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.IsActived);
        }
    }
}
