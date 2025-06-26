using BackendScout.Models;
using Microsoft.EntityFrameworkCore;
using BackendScout.Data;

namespace BackendScout.Services
{
    public class NivelDistritoService
    {
        private readonly AppDbContext _context;

        public NivelDistritoService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<NivelDistrito>> ObtenerTodosAsync()
        {
            return await _context.NivelesDistrito
                .OrderBy(nd => nd.Nombre)
                .ToListAsync();
        }
    }
}
