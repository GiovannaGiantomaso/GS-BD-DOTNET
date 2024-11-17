using GlobalSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UsuarioEnergia> Usuarios { get; set; }
        public DbSet<ConsumoEnergia> Consumos { get; set; }
        public DbSet<HistoricoConsumo> HistoricosConsumo { get; set; }
        public DbSet<FeedbackConsumo> FeedbacksConsumo { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioEnergia>().ToTable("USUARIO_ENERGIA");

            modelBuilder.Entity<ConsumoEnergia>()
                .ToTable("CONSUMO_ENERGIA")
                .HasOne<UsuarioEnergia>()
                .WithMany(u => u.Consumos)
                .HasForeignKey(c => c.IdUsuario);

            modelBuilder.Entity<HistoricoConsumo>()
                .ToTable("HISTORICO_CONSUMO")
                .HasOne<UsuarioEnergia>()
                .WithMany(u => u.HistoricosConsumo)
                .HasForeignKey(h => h.IdUsuario);

            modelBuilder.Entity<FeedbackConsumo>()
                .ToTable("FEEDBACK_CONSUMO")
                .HasOne<UsuarioEnergia>()
                .WithMany(u => u.FeedbacksConsumo)
                .HasForeignKey(f => f.IdUsuario);
        }
    }
}
