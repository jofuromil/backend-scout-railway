using BackendScout.Services;
using Microsoft.AspNetCore.Mvc;
using BackendScout.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using BackendScout.Data; // Asegúrate de tener este using

namespace BackendScout.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GestionController : ControllerBase
    {
        private readonly GestionService _gestionService;
        private readonly AppDbContext _context;

        public GestionController(GestionService gestionService, AppDbContext context)
        {
            _gestionService = gestionService;
            _context = context;
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
        [HttpPost("crear-activa")]
        public async Task<IActionResult> CrearGestionActiva()
        {
            var existe = await _context.Gestiones.AnyAsync(g => g.EstaActiva);
            if (existe)
                return BadRequest("Ya existe una gestión activa.");

            var nuevaGestion = new Gestion
            {
                Id = Guid.NewGuid(),
                Nombre = "2025",
                FechaInicio = DateTime.UtcNow,
                EstaActiva = true
            };

            _context.Gestiones.Add(nuevaGestion);
            await _context.SaveChangesAsync();

            return Ok(new { mensaje = "Gestión 2025 creada y activada correctamente." });
        }
    }
}
