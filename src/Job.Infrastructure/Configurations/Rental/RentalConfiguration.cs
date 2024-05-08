using Job.Domain.Entities.Rental;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Job.Infrastructure.Configurations.Rental;

[ExcludeFromCodeCoverage]
public class RentalConfiguration : IEntityTypeConfiguration<RentalEntity>
{
    public void Configure(EntityTypeBuilder<RentalEntity> builder)
    {

    }
}