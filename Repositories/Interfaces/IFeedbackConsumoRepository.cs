using GlobalSolution.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Repositories.Interfaces
{
    public interface IFeedbackConsumoRepository
    {
        Task<IEnumerable<FeedbackConsumo>> GetAllFeedbacksAsync();
        Task<FeedbackConsumo> GetFeedbackByIdAsync(int id);
        Task AddFeedbackAsync(FeedbackConsumo feedback);
        Task DeleteFeedbackAsync(int id);
    }
}
