using BackendScout.Data;
using BackendScout.Models;
using BackendScout.Helpers;
using Microsoft.EntityFrameworkCore;

namespace BackendScout.Services
{
    public class UnidadService
    {
        private readonly AppDbContext _context;

        public UnidadService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unidad> CrearUnidad(CrearUnidadRequest request)
        {
            // Validar que el ID sea un número válido
            if (!int.TryParse(request.NivelDistritoID, out int distritoId))
                throw new Exception("El distrito seleccionado no es válido.");

            // Buscar NivelDistrito por ID
            var nivelDistrito = await _context.NivelesDistrito
                .FirstOrDefaultAsync(d => d.Id == distritoId);

            if (nivelDistrito == null)
                throw new Exception("No se encontró el nivel de distrito correspondiente.");

            // ✅ Validación del grupo eliminada porque ya está controlado desde el frontend

            // Validar que el dirigente exista
            var dirigente = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.DirigenteId);
            if (dirigente == null)
                throw new Exception("El dirigente no fue encontrado.");

            // Validar que sea dirigente
            if (dirigente.Tipo.ToLower() != "dirigente")
                throw new Exception("Solo un dirigente puede crear una unidad.");

            // Validar que no esté ya en una unidad
            if (dirigente.UnidadId != null)
                throw new Exception("Este dirigente ya está en una unidad. Debe salir antes de crear una nueva.");

            // Buscar grupo scout por nombre
            var grupoScout = await _context.GruposScout
                .FirstOrDefaultAsync(g => g.Nombre == request.GrupoScoutNombre);

            // Si no existe, lo creamos
            if (grupoScout == null)
            {
                grupoScout = new GrupoScout
                {
                    Nombre = request.GrupoScoutNombre,
                    NivelDistritoId = distritoId,
                    Unidades = new List<Unidad>()
                };

                _context.GruposScout.Add(grupoScout);
                await _context.SaveChangesAsync();
            }

            // Crear nueva unidad
            var nueva = new Unidad
            {
                Id = Guid.NewGuid(),
                Nombre = request.Nombre,
                Rama = request.Rama,
                GrupoScoutNombre = request.GrupoScoutNombre,
                GrupoScoutId = grupoScout.Id,
                NivelDistritoId = distritoId,
                DirigenteId = request.DirigenteId,
                CodigoUnidad = Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            _context.Unidades.Add(nueva);
            await _context.SaveChangesAsync();

            // Asociar el dirigente a la unidad
            dirigente.UnidadId = nueva.Id;
            await _context.SaveChangesAsync();

            // Verificar si ya existe una relación con el grupo
            bool yaRelacionado = await _context.GrupoScoutUsuarios
                .AnyAsync(r => r.GrupoScoutId == grupoScout.Id && r.UsuarioId == dirigente.Id);

            if (!yaRelacionado)
            {
                var relacion = new GrupoScoutUsuario
                {
                    UsuarioId = dirigente.Id,
                    GrupoScoutId = grupoScout.Id,
                    EsAdminGrupo = true
                };

                _context.GrupoScoutUsuarios.Add(relacion);
                await _context.SaveChangesAsync();
            }

            return nueva;
        }

        public async Task<List<Unidad>> ObtenerUnidades()
        {
            return await _context.Unidades.ToListAsync();
        }
    }
}
