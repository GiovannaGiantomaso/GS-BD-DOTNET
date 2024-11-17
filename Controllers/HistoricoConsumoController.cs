using GlobalSolution.Models;
using GlobalSolution.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoricoConsumoController : ControllerBase
    {
        private readonly HistoricoConsumoService _service;

        public HistoricoConsumoController(HistoricoConsumoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CreateHistorico(int idUsuario)
        {
            if (idUsuario <= 0)
            {
                return BadRequest("ID de usuário inválido.");
            }

            var result = await _service.InserirHistoricoAsync(idUsuario);
            if (result.StartsWith("Erro"))
            {
                return BadRequest(result);
            }
            return Ok(new { Message = "Histórico criado com sucesso.", Result = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllHistoricos()
        {
            var historicos = await _service.BuscarTodosHistoricosAsync();
            return Ok(historicos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetHistoricoById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID de histórico inválido.");
            }

            var historico = await _service.BuscarHistoricoPorIdAsync(id);
            if (historico == null)
            {
                return NotFound("Histórico não encontrado.");
            }
            return Ok(historico);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistorico(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID de histórico inválido.");
            }

            var result = await _service.DeletarHistoricoAsync(id);
            if (result.StartsWith("Erro"))
            {
                return BadRequest(result);
            }
            return Ok(new { Message = result });
        }
    }
}
