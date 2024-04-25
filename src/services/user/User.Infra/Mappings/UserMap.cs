using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Domain.Entities;

namespace User.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<Partner>
    {
        public void Configure(EntityTypeBuilder<Partner> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .UseIdentityColumn()
                .HasColumnName("id")
                .HasColumnType("BIGINT");

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("name")
                .HasColumnType("VARCHAR(80)");

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnName("email")
                .HasColumnType("VARCHAR(200)")
                .HasMaxLength(200);

            builder.Property(x => x.Password)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("password")
                .HasColumnType("VARCHAR(200)");

            builder.Property(x => x.Cnpj)
                .IsRequired()
                .HasColumnName("cnpj")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);

            builder.Property(x => x.CnhImage)
                .IsRequired()
                .HasColumnName("chnImage")
                .HasColumnType("VARCHAR(1000)")
                .HasMaxLength(1000);

            builder.Property(x => x.CnhNumber)
                .IsRequired()
                .HasColumnName("cnhNumber")
                .HasColumnType("VARCHAR(15)")
                .HasMaxLength(15);

            builder.Property(x => x.CnhType)
                .IsRequired()
                .HasColumnName("chnType")
                .HasColumnType("VARCHAR(3)")
                .HasMaxLength(5);

            builder.Property(x => x.DateBirth)
                .IsRequired()
                .HasColumnName("dateBirth")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);

            builder.Property(x => x.Role)
                .IsRequired()
                .HasColumnName("role")
                .HasColumnType("VARCHAR(20)")
                .HasMaxLength(20);
        }
    }
}

