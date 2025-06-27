using BackendScout.Services;
using Microsoft.AspNetCore.Mvc;
using BackendScout.Models;
using Microsoft.AspNetCore.Authorization;

namespace BackendScout.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionController : ControllerBase
    {
        private readonly GestionService _gestionService;

        public GestionController(GestionService gestionService)
        {
            _gestionService = gestionService;
        }

        // Solo AdminNacional puede crear nueva gestión
        [HttpPost("crear")]
        [Authorize(Roles = "AdminNacional")]
        public async Task<IActionResult> CrearGestion([FromBody] string nombre)
        {
            var nuevaGestion = await _gestionService.CrearNuevaGestionAsync(nombre);
            return Ok(nuevaGestion);
        }

        // Todos los niveles pueden consultar la gestión activa
        [HttpGet("activa")]
        [Authorize]
        public async Task<IActionResult> ObtenerGestionActiva()
        {
            var gestion = await _gestionService.ObtenerGestionActivaAsync();
            if (gestion == null)
                return NotFound("No hay una gestión activa.");
            return Ok(gestion);
        }

        // Solo AdminNacional puede ver el historial completo
        [HttpGet("historial")]
        [Authorize(Roles = "AdminNacional")]
        public async Task<IActionResult> ObtenerHistorial()
        {
            var historial = await _gestionService.ObtenerHistorialGestionesAsync();
            return Ok(historial);
        }
    }
}
