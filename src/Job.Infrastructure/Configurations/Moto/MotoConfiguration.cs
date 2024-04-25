using Job.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.Configurations.Moto;

public class MotoConfiguration : IEntityTypeConfiguration<MotoboyEntity>
{
    public void Configure(EntityTypeBuilder<MotoboyEntity> builder)
    {

    }
}