using Microsoft.EntityFrameworkCore;
using rent_core_api.Model;

namespace rent_core_api.Infra
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Moto> Motos { get; set; }
        public DbSet<Entregador> Entregadores { get; set; }
        public DbSet<Plano> Planos { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(
                    "Server=localhost;" +
                    "Port=5432; Database=postgres;" +
                    "User Id=postgres;" +
                    "Password=admin").LogTo(Console.WriteLine, LogLevel.Information);
                ;
            }
        }
    }
}
