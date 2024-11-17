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
            // Configuração para a tabela USUARIO_ENERGIA
            modelBuilder.Entity<UsuarioEnergia>()
                .ToTable("USUARIO_ENERGIA")
                .HasKey(u => u.IdUsuario);

            // Configuração para a tabela CONSUMO_ENERGIA
            modelBuilder.Entity<ConsumoEnergia>()
                .ToTable("CONSUMO_ENERGIA")
                .HasKey(c => c.IdConsumo);

            modelBuilder.Entity<ConsumoEnergia>()
                .HasOne<UsuarioEnergia>()
                .WithMany(u => u.Consumos)
                .HasForeignKey(c => c.IdUsuario);

            // Configuração para a tabela HISTORICO_CONSUMO
            modelBuilder.Entity<HistoricoConsumo>()
                .ToTable("HISTORICO_CONSUMO")
                .HasKey(h => h.IdHistorico);

            modelBuilder.Entity<HistoricoConsumo>()
                .HasOne<UsuarioEnergia>()
                .WithMany(u => u.HistoricosConsumo)
                .HasForeignKey(h => h.IdUsuario);

            // Configuração para a tabela FEEDBACK_CONSUMO
            modelBuilder.Entity<FeedbackConsumo>()
                .ToTable("FEEDBACK_CONSUMO")
                .HasKey(f => f.IdFeedback);

            modelBuilder.Entity<FeedbackConsumo>()
                .HasOne<UsuarioEnergia>()
                .WithMany(u => u.FeedbacksConsumo)
                .HasForeignKey(f => f.IdUsuario);
        }
    }
}
