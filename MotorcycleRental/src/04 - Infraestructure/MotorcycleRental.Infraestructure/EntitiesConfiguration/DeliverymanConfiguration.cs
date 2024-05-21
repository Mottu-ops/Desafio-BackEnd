using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Infraestructure.EntitiesConfiguration
{
    public class DeliverymanConfiguration : IEntityTypeConfiguration<Deliveryman>
    {
        public void Configure(EntityTypeBuilder<Deliveryman> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Name).HasColumnType("varchar").HasMaxLength(120).IsRequired();
            builder.Property(p => p.CNPJ).HasColumnType("varchar").HasMaxLength(18).IsRequired();
            builder.HasIndex(p => p.CNPJ).IsUnique();
            builder.Property(p => p.Email).HasColumnType("varchar").HasMaxLength(100).IsRequired();
            builder.HasIndex(p => p.Email).IsUnique();
            builder.Property(p => p.DriverLicenseNumber).HasColumnType("varchar").HasMaxLength(30);
            builder.HasIndex(p => p.DriverLicenseNumber).IsUnique();
            builder.Property(p => p.DateOfBirth).HasColumnType("date");
            builder.Property(p => p.DriverLicenseType).HasColumnType("varchar").HasMaxLength(30);
            builder.Property(p => p.CNHImageUrl).HasColumnType("varchar").HasMaxLength(100).IsRequired(false);
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.IsActived);
        }
    }
}
