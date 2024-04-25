using Job.Domain.Entities.Rent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.Configurations.Rent;

public class RentConfiguration : IEntityTypeConfiguration<RentEntity>
{
    public void Configure(EntityTypeBuilder<RentEntity> builder)
    {

    }
}