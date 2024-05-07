using Job.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.Configurations.User;

[ExcludeFromCodeCoverage]
public class MotoboyConfiguration : IEntityTypeConfiguration<MotoboyEntity>
{
    public void Configure(EntityTypeBuilder<MotoboyEntity> builder)
    {
        builder.ToTable("MotoBoys");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .ValueGeneratedNever();

    }
}