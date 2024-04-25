using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;
using User.Infra.Mappings;

namespace User.Infra.context
{
    public class UserContext : DbContext
    {
        public UserContext()
        {

        }

        public UserContext(DbContextOptions options) : base(options) { }

        public virtual DbSet<Partner> Partners { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserMap());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Host=localhost;Username=postgres;Password=postgres;Database=SampleDbDriver; PORT=5433";
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}