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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _repository;
        private readonly UsuarioEnergiaService _usuarioService;

        public UsuarioController(IUsuarioRepository repository, UsuarioEnergiaService usuarioService)
        {
            _repository = repository;
            _usuarioService = usuarioService;
        }

        // GET: api/Usuario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioEnergia>>> GetUsuarios()
        {
            var usuarios = await _repository.GetAllUsuariosAsync();
            return Ok(usuarios);
        }

        // GET: api/Usuario/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuarioEnergia>> GetUsuario(int id)
        {
            var usuario = await _repository.GetUsuarioByIdAsync(id);
            if (usuario == null) return NotFound();
            return Ok(usuario);
        }

        // POST: api/Usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] UsuarioEnergia usuario)
        {
            var result = await _usuarioService.InserirUsuarioAsync(usuario.Nome, usuario.Email, usuario.Senha);
            if (result.StartsWith("Erro"))
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        // PUT: api/Usuario/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsuario(int id, UsuarioEnergia usuario)
        {
            if (id != usuario.IdUsuario) return BadRequest();
            await _repository.UpdateUsuarioAsync(usuario);
            return NoContent();
        }

        // DELETE: api/Usuario/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            await _repository.DeleteUsuarioAsync(id);
            return NoContent();
        }
    }
}
