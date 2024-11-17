using GlobalSolution.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Repositories.Interfaces
{
    public interface IHistoricoConsumoRepository
    {
        Task<IEnumerable<HistoricoConsumo>> GetAllHistoricosAsync();
        Task<HistoricoConsumo> GetHistoricoByIdAsync(int id);
        Task AddHistoricoAsync(HistoricoConsumo historico);
        Task DeleteHistoricoAsync(int id);
    }
}
