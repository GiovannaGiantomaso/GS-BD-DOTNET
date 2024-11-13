using GlobalSolution.Models;
using GlobalSolution.Repositories.Interfaces;
using GlobalSolution.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ConsumoController : ControllerBase
    {
        private readonly IConsumoRepository _repository;
        private readonly ConsumoEnergiaService _consumoService;

        public ConsumoController(IConsumoRepository repository, ConsumoEnergiaService consumoService)
        {
            _repository = repository;
            _consumoService = consumoService;
        }

        // GET: api/Consumo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsumoEnergia>>> GetConsumos()
        {
            var consumos = await _repository.GetAllConsumosAsync();
            return Ok(consumos);
        }

        // GET: api/Consumo/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsumoEnergia>> GetConsumo(int id)
        {
            var consumo = await _repository.GetConsumoByIdAsync(id);
            if (consumo == null) return NotFound();
            return Ok(consumo);
        }

        // POST: api/Consumo
        [HttpPost]
        public async Task<IActionResult> CreateConsumo(int idUsuario, decimal consumoKwh)
        {
            // Chama a procedure para inserir um novo consumo usando o serviço
            var result = await _consumoService.InserirConsumoAsync(idUsuario, consumoKwh);
            if (result.StartsWith("Erro"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT: api/Consumo/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateConsumo(int id, ConsumoEnergia consumo)
        {
            if (id != consumo.IdConsumo) return BadRequest();
            await _repository.UpdateConsumoAsync(consumo);
            return NoContent();
        }

        // DELETE: api/Consumo/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConsumo(int id)
        {
            await _repository.DeleteConsumoAsync(id);
            return NoContent();
        }
    }
}
