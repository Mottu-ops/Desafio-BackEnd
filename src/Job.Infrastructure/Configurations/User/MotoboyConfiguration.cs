using Job.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.Configurations.User;

public class MotoboyConfiguration : IEntityTypeConfiguration<MotoboyEntity>
{
    public void Configure(EntityTypeBuilder<MotoboyEntity> builder)
    {

    }
}