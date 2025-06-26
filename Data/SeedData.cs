using BackendScout.Data;
using BackendScout.Models;

namespace BackendScout.Data
{
    public static class SeedData
    {
        public static void Inicializar(AppDbContext context)
        {
            // Si ya existen distritos, no hacer nada
            if (context.NivelesDistrito.Any()) return;

            var distritos = new List<NivelDistrito>
            {
                new NivelDistrito { Nombre = "La Paz" },
                new NivelDistrito { Nombre = "Cochabamba" },
                new NivelDistrito { Nombre = "Santa Cruz" },
                new NivelDistrito { Nombre = "Oruro" },
                new NivelDistrito { Nombre = "Potosí" },
                new NivelDistrito { Nombre = "Chuquisaca" },
                new NivelDistrito { Nombre = "Tarija" },
                new NivelDistrito { Nombre = "Beni" },
                new NivelDistrito { Nombre = "Pando" }
            };

            context.NivelesDistrito.AddRange(distritos);
            context.SaveChanges();
        }
    }
}
