using GlobalSolution.Data;
using GlobalSolution.Models;
using GlobalSolution.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Repositories.Implementations
{
    public class HistoricoConsumoRepository : IHistoricoConsumoRepository
    {
        private readonly AppDbContext _context;

        public HistoricoConsumoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HistoricoConsumo>> GetAllHistoricosAsync()
        {
            return await _context.HistoricosConsumo.ToListAsync();
        }

        public async Task<HistoricoConsumo> GetHistoricoByIdAsync(int id)
        {
            return await _context.HistoricosConsumo.FindAsync(id);
        }

        public async Task AddHistoricoAsync(HistoricoConsumo historico)
        {
            await _context.HistoricosConsumo.AddAsync(historico);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteHistoricoAsync(int id)
        {
            var historico = await GetHistoricoByIdAsync(id);
            if (historico != null)
            {
                _context.HistoricosConsumo.Remove(historico);
                await _context.SaveChangesAsync();
            }
        }
    }
}
