using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BackendScout.Services;
using System;
using System.Threading.Tasks;
using BackendScout.Data;
using BackendScout.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendScout.Controllers
{
    [ApiController]
    [Route("api/gruposcout")]
    public class GrupoScoutController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AppDbContext _context;

        public GrupoScoutController(UserService userService, AppDbContext context)
        {
            _userService = userService;
            _context = context;
        }

        [HttpGet("dirigentes")]
        [Authorize(Roles = "Dirigente")]
        public async Task<IActionResult> ObtenerDirigentesDelGrupo()
        {
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var dirigentes = await _userService.ObtenerDirigentesDelGrupo(Guid.Parse(userId));
            return Ok(dirigentes);
        }
        private Guid ObtenerUsuarioIdDesdeToken()
{
    var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
    return Guid.Parse(userIdClaim.Value);
}



        [HttpPut("asignar-admingrupo/{userId}")]
[Authorize(Roles = "Dirigente")]
public async Task<IActionResult> AsignarAdminGrupo(Guid userId)
{
    var usuarioActualId = ObtenerUsuarioIdDesdeToken(); // método auxiliar
    var usuarioActual = await _context.Users
        .Include(u => u.GrupoScoutUsuarios)
        .FirstOrDefaultAsync(u => u.Id == usuarioActualId);

    if (usuarioActual == null)
        return Unauthorized();

    // Obtener el grupo al que pertenece el usuario actual
    var grupoUsuario = usuarioActual.GrupoScoutUsuarios.FirstOrDefault();
    if (grupoUsuario == null)
        return BadRequest("No estás vinculado a ningún grupo scout.");

    var grupoScoutId = grupoUsuario.GrupoScoutId;

    // Buscar si ya existe el registro
    var existente = await _context.GrupoScoutUsuarios
        .FirstOrDefaultAsync(g => g.GrupoScoutId == grupoScoutId && g.UsuarioId == userId);

    if (existente != null)
    {
        existente.EsAdminGrupo = true;
    }
    else
    {
        _context.GrupoScoutUsuarios.Add(new GrupoScoutUsuario
        {
            UsuarioId = userId,
            GrupoScoutId = grupoScoutId,
            EsAdminGrupo = true
        });
    }

    await _context.SaveChangesAsync();
    return Ok();
}



        [HttpPut("quitar-admingrupo/{userId}")]
[Authorize(Roles = "Dirigente")]
public async Task<IActionResult> QuitarAdminGrupo(Guid userId)
{
    var usuarioActualId = ObtenerUsuarioIdDesdeToken();
    var usuarioActual = await _context.Users
        .Include(u => u.GrupoScoutUsuarios)
        .FirstOrDefaultAsync(u => u.Id == usuarioActualId);

    if (usuarioActual == null)
        return Unauthorized();

    var grupoUsuario = usuarioActual.GrupoScoutUsuarios.FirstOrDefault();
    if (grupoUsuario == null)
        return BadRequest("No estás vinculado a ningún grupo scout.");

    var grupoScoutId = grupoUsuario.GrupoScoutId;

    var existente = await _context.GrupoScoutUsuarios
        .FirstOrDefaultAsync(g => g.GrupoScoutId == grupoScoutId && g.UsuarioId == userId);

    if (existente == null)
        return NotFound("El usuario no es administrador del grupo.");

    existente.EsAdminGrupo = false;
    await _context.SaveChangesAsync();

    return Ok();
}

    }
}
