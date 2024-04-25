using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plan.Domain.Entity;

namespace Plan.Infra.Mappings;
public class PlanMap : IEntityTypeConfiguration<RentPlan>
{
    public void Configure(EntityTypeBuilder<RentPlan> builder)
    {
        builder.ToTable("Plans");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("id")
                .HasColumnType("BIGINT");

        builder.Property(x => x.DailyRate)
                .HasMaxLength(80)
                .HasColumnName("dailyRate")
                .HasColumnType("DECIMAL");

        builder.Property(x => x.Name)
                .HasMaxLength(20)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(80)");

        builder.Property(x => x.Days)
                .HasMaxLength(100)
                .HasColumnName("days")
                .HasColumnType("INTEGER");

        builder.Property(x => x.User)
                .HasMaxLength(100)
                .HasColumnName("user")
                .HasColumnType("BIGINT");

        builder.Property(x => x.LateFee)
                .HasMaxLength(100)
                .HasColumnName("lateFee")
                .HasColumnType("DECIMAL");
    }
}