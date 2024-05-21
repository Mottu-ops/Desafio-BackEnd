using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Infraestructure.EntitiesConfiguration
{
    public class RentalPlanConfiguration : IEntityTypeConfiguration<RentalPlan>
    {
        public void Configure(EntityTypeBuilder<RentalPlan> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(p => p.Descrition).HasColumnType("varchar").HasMaxLength(120).IsRequired();
            builder.Property(p => p.Days).HasColumnType("numeric(5,0)").IsRequired();
            builder.HasIndex(p => p.Days).IsUnique();
            builder.Property(p => p.DayValue).HasColumnType("numeric(18, 2)").IsRequired();
            builder.Property(p => p.PercentageFine).HasColumnType("numeric(18, 2)").IsRequired();
            builder.Property(p => p.AdditionalValueDaily).HasColumnType("numeric(18, 2)").IsRequired();
            builder.Property(p => p.CreatedAt);
            builder.Property(p => p.UpdatedAt);
            builder.Property(p => p.IsActived);
        }
    }
}
