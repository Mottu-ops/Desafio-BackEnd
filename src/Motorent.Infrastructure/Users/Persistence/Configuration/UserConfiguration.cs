using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorent.Domain.Common.ValueObjects;
using Motorent.Domain.Users;
using Motorent.Domain.Users.Enums;
using Motorent.Domain.Users.ValueObjects;

namespace Motorent.Infrastructure.Users.Persistence.Configuration;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasIndex(u => u.Email);

        builder.Property(u => u.Id)
            .HasConversion(v => v.Value, v => new UserId(v));

        builder.Property(u => u.Role)
            .HasConversion(v => v.Name, v => Role.FromName(v, false))
            .HasMaxLength(UserConstants.RoleMaxLength);
        
        builder.Property(u => u.Name)
            .HasConversion(v => v.Value, v => new Name(v))
            .HasMaxLength(UserConstants.NameMaxLength);

        builder.Property(u => u.Birthdate)
            .HasConversion(v => v.Value, v => Birthdate.Create(v).Value);
        
        builder.Property(u => u.Email)
            .HasMaxLength(UserConstants.EmailMaxLength);

        builder.Property(u => u.PasswordHash)
            .HasMaxLength(UserConstants.PasswordHashMaxLength);

        builder.ToTable(UserConstants.TableName);
    }
}