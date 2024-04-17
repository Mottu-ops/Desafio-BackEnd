using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorent.Domain.Users;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Infrastructure.Users.Persistence.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasKey(u => u.Email);

        builder.Property(u => u.Id)
            .HasConversion(v => v.Value, v => new UserId(v));

        builder.Property(u => u.Email)
            .HasMaxLength(UserConstants.EmailMaxLength);

        builder.Property(u => u.Name)
            .HasMaxLength(UserConstants.NameMaxLength);

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(UserConstants.PasswordHashMaxLength);

        builder.ToTable(UserConstants.TableName);
    }
}