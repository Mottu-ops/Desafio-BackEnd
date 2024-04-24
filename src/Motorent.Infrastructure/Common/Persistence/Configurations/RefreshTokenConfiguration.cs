using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Motorent.Domain.Users.ValueObjects;
using Motorent.Infrastructure.Common.Security;

namespace Motorent.Infrastructure.Common.Persistence.Configurations;

internal sealed class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => new { rt.UserId, rt.Token });

        builder.Property(rt => rt.UserId)
            .ValueGeneratedNever()
            .HasMaxLength(26)
            .HasConversion(v => v.Value.ToString(), v => new UserId(Ulid.Parse(v)));

        builder.Property(rt => rt.Token)
            .HasMaxLength(255);
        
        builder.Property(rt => rt.AccessTokenId)
            .HasMaxLength(2048);
        
        builder.ToTable("refresh_tokens");
    }
}