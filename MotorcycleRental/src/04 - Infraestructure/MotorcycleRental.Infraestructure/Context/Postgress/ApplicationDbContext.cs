using Microsoft.EntityFrameworkCore;
using MotorcycleRental.Domain.Entities;

namespace MotorcycleRental.Infraestructure.Context.Postgress
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Motorcycle> Motorcycles { get; set; }
        public DbSet<RentalPlan> RentalPlans { get; set; }
        public DbSet<Deliveryman> Deliverymen { get; set; }
        public DbSet<RentalContract> RentalContracts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
