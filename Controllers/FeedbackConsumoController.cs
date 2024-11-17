using GlobalSolution.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GlobalSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackConsumoController : ControllerBase
    {
        private readonly FeedbackConsumoService _service;

        public FeedbackConsumoController(FeedbackConsumoService service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeedback(int id)
        {
            var feedback = await _service.GetFeedbackByIdAsync(id);
            if (feedback == null) return NotFound();
            return Ok(feedback);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedbacks()
        {
            var feedbacks = await _service.GetAllFeedbacksAsync();
            return Ok(feedbacks);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFeedback(int idUsuario, string mensagemFeedback)
        {
            var result = await _service.InserirFeedbackAsync(idUsuario, mensagemFeedback);
            if (result.StartsWith("Erro"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var result = await _service.DeleteFeedbackAsync(id);
            if (result.StartsWith("Erro"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
