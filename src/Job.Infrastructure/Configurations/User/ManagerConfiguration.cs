using Job.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.Configurations.User;

public class ManagerConfiguration : IEntityTypeConfiguration<ManagerEntity>
{
    public void Configure(EntityTypeBuilder<ManagerEntity> builder)
    {
        builder.ToTable("Managers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Password)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Email)
            .IsUnique();

        builder.HasData(new ManagerEntity("job@job.com", "mudar@123"));
    }
}