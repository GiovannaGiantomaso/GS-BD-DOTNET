using GlobalSolution.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Repositories.Interfaces
{
    public interface IConsumoRepository
    {
        Task<IEnumerable<ConsumoEnergia>> GetAllConsumosAsync();
        Task<ConsumoEnergia> GetConsumoByIdAsync(int id);
        Task AddConsumoAsync(ConsumoEnergia consumo);
        Task UpdateConsumoAsync(ConsumoEnergia consumo);
        Task DeleteConsumoAsync(int id);
    }
}
