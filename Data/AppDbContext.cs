using GlobalSolution.Models;
using Microsoft.EntityFrameworkCore;

namespace GlobalSolution.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UsuarioEnergia> Usuarios { get; set; }
        public DbSet<ConsumoEnergia> Consumos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UsuarioEnergia>().ToTable("USUARIO_ENERGIA");
            modelBuilder.Entity<ConsumoEnergia>().ToTable("CONSUMO_ENERGIA");

            modelBuilder.Entity<ConsumoEnergia>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Consumos)
                .HasForeignKey(c => c.IdUsuario);
        }
    }
}
