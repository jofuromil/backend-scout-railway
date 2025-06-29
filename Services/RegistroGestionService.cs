using BackendScout.Data;
using BackendScout.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendScout.Services
{
    public class RegistroGestionService
    {
        private readonly AppDbContext _context;

        public RegistroGestionService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<RegistroGestion>> ObtenerRegistrosGrupoAsync(int grupoId)
        {
            return await _context.RegistrosGestion
                .Include(r => r.Usuario)
                    .ThenInclude(u => u.Unidad)
                .Include(r => r.Gestion)
                .Where(r => r.Usuario.GrupoScoutUsuarios.Any(g => g.GrupoScoutId == grupoId))
                .ToListAsync();
        }

        public async Task RegistrarUsuarioEnGestionAsync(Guid usuarioId, Guid gestionId)
        {
            var existe = await _context.RegistrosGestion.AnyAsync(r =>
                r.UsuarioId == usuarioId && r.GestionId == gestionId);

            if (!existe)
            {
                var nuevoRegistro = new RegistroGestion
                {
                    Id = Guid.NewGuid(),
                    UsuarioId = usuarioId,
                    GestionId = gestionId,
                    AprobadoGrupo = true,
                    FechaAprobadoGrupo = DateTime.UtcNow
                };

                _context.RegistrosGestion.Add(nuevoRegistro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task QuitarRegistroDeUsuarioAsync(Guid usuarioId, Guid gestionId)
        {
            var registro = await _context.RegistrosGestion.FirstOrDefaultAsync(r =>
                r.UsuarioId == usuarioId && r.GestionId == gestionId);

            if (registro != null)
            {
                _context.RegistrosGestion.Remove(registro);
                await _context.SaveChangesAsync();
            }
        }
        

        public async Task<List<User>> ObtenerUsuariosRegistradosAsync(int grupoId, Guid gestionId)
        {
            var idsRegistrados = await _context.RegistrosGestion
                .Where(r => r.GestionId == gestionId && r.AprobadoGrupo)
                .Select(r => r.UsuarioId)
                .ToListAsync();

            return await _context.Users
                .Include(u => u.Unidad)
                .Where(u => idsRegistrados.Contains(u.Id) && u.GrupoScoutUsuarios.Any(g => g.GrupoScoutId == grupoId))
                .ToListAsync();
        }
    }
}
