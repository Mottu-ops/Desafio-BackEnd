using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Motorent.Infrastructure.Common.Outbox;

namespace Motorent.Infrastructure.Common.Persistence.Configurations;

internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(om => om.Id);

        builder.Property(om => om.Id)
            .ValueGeneratedNever();

        builder.Property(om => om.Type)
            .HasMaxLength(256);
        
        builder.Property(om => om.Data)
            .HasMaxLength(8192);
        
        builder.Property(om => om.ErrorType)
            .HasMaxLength(50);
        
        builder.Property(om => om.ErrorMessage)
            .HasMaxLength(100);
        
        builder.Property(om => om.ErrorDetails)
            .HasMaxLength(2048);

        builder.Property(om => om.Status)
            .HasConversion<EnumToStringConverter<OutboxMessageStatus>>()
            .HasMaxLength(20);
        
        builder.ToTable("outbox_messages");
    }
}