using GlobalSolution.Data;
using GlobalSolution.Models;
using GlobalSolution.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Repositories.Implementations
{
    public class FeedbackConsumoRepository : IFeedbackConsumoRepository
    {
        private readonly AppDbContext _context;

        public FeedbackConsumoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FeedbackConsumo>> GetAllFeedbacksAsync()
        {
            return await _context.FeedbacksConsumo.ToListAsync();
        }

        public async Task<FeedbackConsumo> GetFeedbackByIdAsync(int id)
        {
            return await _context.FeedbacksConsumo.FindAsync(id);
        }

        public async Task AddFeedbackAsync(FeedbackConsumo feedback)
        {
            await _context.FeedbacksConsumo.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await GetFeedbackByIdAsync(id);
            if (feedback != null)
            {
                _context.FeedbacksConsumo.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }
    }
}
