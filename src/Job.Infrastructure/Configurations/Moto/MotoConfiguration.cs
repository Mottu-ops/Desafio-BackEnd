using Job.Domain.Entities.User;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.Configurations.Moto;

[ExcludeFromCodeCoverage]
public class MotoConfiguration : IEntityTypeConfiguration<MotoboyEntity>
{
    public void Configure(EntityTypeBuilder<MotoboyEntity> builder)
    {

    }
}