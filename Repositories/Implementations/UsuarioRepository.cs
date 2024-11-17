using GlobalSolution.Data;
using GlobalSolution.Models;
using GlobalSolution.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Repositories.Implementations
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioEnergia>> GetAllUsuariosAsync()
        {
            return await _context.Usuarios
                .Include(u => u.Consumos)
                .Include(u => u.HistoricosConsumo)
                .Include(u => u.FeedbacksConsumo)
                .ToListAsync();
        }


        public async Task<UsuarioEnergia> GetUsuarioByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.Consumos)
                .Include(u => u.HistoricosConsumo)
                .Include(u => u.FeedbacksConsumo)
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        public async Task AddUsuarioAsync(UsuarioEnergia usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsuarioAsync(UsuarioEnergia usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioAsync(int id)
        {
            var usuario = await GetUsuarioByIdAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
            }
        }
    }
}
