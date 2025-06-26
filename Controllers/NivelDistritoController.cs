using BackendScout.Models;
using BackendScout.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BackendScout.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NivelDistritoController : ControllerBase
    {
        private readonly NivelDistritoService _nivelDistritoService;

        public NivelDistritoController(NivelDistritoService nivelDistritoService)
        {
            _nivelDistritoService = nivelDistritoService;
        }

        // GET: api/niveldistrito/todos
        [HttpGet("todos")]
        [AllowAnonymous]
        public async Task<ActionResult<List<NivelDistrito>>> ObtenerTodos()
        {
            var distritos = await _nivelDistritoService.ObtenerTodosAsync();
            return Ok(distritos);
        }
    }
}
