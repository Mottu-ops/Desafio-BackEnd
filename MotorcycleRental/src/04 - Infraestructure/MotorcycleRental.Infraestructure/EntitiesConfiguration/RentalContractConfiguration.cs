using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Domain.Entities;
using SharpCompress.Common;

namespace MotorcycleRental.Infraestructure.EntitiesConfiguration
{
    public class RentalContractConfiguration : IEntityTypeConfiguration<RentalContract>
    {
        public void Configure(EntityTypeBuilder<RentalContract> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasIndex(e => new { e.DeliverymanId, e.MotorcycleId, e.RentanPlanId, e.StartDate }).IsUnique();

            builder.HasOne(d => d.Deliveryman)
                   .WithMany(c => c.RentalContracts)
                   .HasForeignKey(d => d.DeliverymanId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Plan)
                   .WithMany(c => c.RentalContracts)
                   .HasForeignKey(p => p.RentanPlanId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.Motorcycle)
                   .WithMany(c => c.RentalContracts)
                   .HasForeignKey(p => p.MotorcycleId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.StartDate).HasColumnType("date").IsRequired();
            builder.Property(p => p.StartDate).HasColumnType("date").IsRequired();
            builder.Property(p => p.ExpectedEndDate).HasColumnType("date").IsRequired();

            builder.Property(p => p.EndDate).HasColumnType("date");
            builder.Property(p => p.RentalValue).HasColumnType("numeric(18, 2)");
            builder.Property(p => p.AdditionalFineValue).HasColumnType("numeric(18, 2)");
            builder.Property(p => p.AdditionalDailyValue).HasColumnType("numeric(18, 2)");
            builder.Property(p => p.TotalRentalValue).HasColumnType("numeric(18, 2)");
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.IsActived);
        }
    }
}
