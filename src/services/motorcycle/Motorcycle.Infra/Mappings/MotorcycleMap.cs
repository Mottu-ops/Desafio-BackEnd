using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorcycle.Domain.Entities;

namespace Motorcycle.Infra.Mappings
{

        public class MotorcycleMap : IEntityTypeConfiguration<Vehicle>
        {
                public void Configure(EntityTypeBuilder<Vehicle> builder)
                {
                        builder.ToTable("Motorcycles");

                        builder.HasKey(x => x.Id);
                        builder.Property(x => x.Id)
                                .UseIdentityColumn()
                                .HasColumnName("id")
                                .HasColumnType("BIGINT");

                        builder.Property(x => x.PlateCode)
                                .HasMaxLength(80)
                                .HasColumnName("plateCode")
                                .HasColumnType("VARCHAR(80)");

                        builder.Property(x => x.Color)
                                .HasMaxLength(20)
                                .HasColumnName("color")
                                .HasColumnType("VARCHAR(20)");

                        builder.Property(x => x.Model)
                                .HasMaxLength(100)
                                .HasColumnName("model")
                                .HasColumnType("VARCHAR(100)");

                        builder.Property(x => x.Year)
                                .HasMaxLength(10)
                                .HasColumnName("year")
                                .HasColumnType("VARCHAR(10)");

                        builder.Property(x => x.Status)
                                .HasMaxLength(10)
                                .HasColumnName("year")
                                .HasColumnType("VARCHAR(10)");
                }
        }
}