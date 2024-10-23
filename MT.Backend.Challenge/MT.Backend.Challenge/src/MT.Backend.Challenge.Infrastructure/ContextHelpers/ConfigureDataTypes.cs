using Microsoft.EntityFrameworkCore;
using MT.Backend.Challenge.Domain.Entities;

namespace MT.Backend.Challenge.Infrastructure.ContextHelpers
{
    public static class ConfigureDataTypes
    {
        public static void ConfigureGenericDataTypes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RentalCategory>(
                entity =>
                {
                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.HasIndex(e => e.Name)
                        .IsUnique();

                    entity.Property(e => e.RentalCategoryDays)
                        .IsRequired();

                    entity.HasIndex(e => e.RentalCategoryDays)
                        .IsUnique();

                    entity.Property(e => e.Price)
                        .IsRequired();

                    entity.Property(e => e.PercentualFine)
                        .IsRequired();
                });

            modelBuilder.Entity<Motorcycle>(
                entity =>
                {
                    entity.Property(e => e.LicensePlate)
                        .IsRequired()
                        .HasMaxLength(7);

                    entity.HasIndex(e => e.LicensePlate)
                        .IsUnique();

                    entity.Property(e => e.Brand)
                        .HasMaxLength(50);

                    entity.Property(e => e.Model)
                        .IsRequired()
                        .HasMaxLength(50);

                    entity.Property(e => e.Year)
                        .IsRequired();

                    entity.Property(e => e.Color)
                        .HasMaxLength(50);
                });

            modelBuilder.Entity<DeliveryDriver>(
                entity =>
                {
                    entity.Property(e => e.Name)
                        .IsRequired()
                        .HasMaxLength(120);

                    entity.Property(e => e.Document)
                        .IsRequired()
                        .HasMaxLength(14);

                    entity.HasIndex(e => e.Document)
                        .IsUnique();

                    entity.Property(e => e.BirthDate)
                        .IsRequired();

                    entity.Property(e => e.DriversLicenseNumber)
                        .IsRequired()
                        .HasMaxLength(30);

                    entity.HasIndex(e => e.DriversLicenseNumber)
                        .IsUnique();

                    entity.Property(e => e.DriversLicenseImageUrl)
                        .HasMaxLength(300);

                    entity.Ignore(e => e.DriversLicenseImage);
                });

            modelBuilder.Entity<Rental>(
                entity =>
                {
                    entity.Property(e => e.DeliveryDriverId)
                        .IsRequired();
                    entity.Property(e => e.MotorcycleId)
                        .IsRequired();
                    entity.Property(e => e.StartDate)
                        .IsRequired();
                    entity.Property(e => e.EndDate)
                        .IsRequired();
                    entity.Property(e => e.EstimatedEndDate)
                        .IsRequired();
                });
        }
    }
}
