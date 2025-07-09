using BackendScout.Data;
using BackendScout.Models;

namespace BackendScout.Data
{
    public static class SeedData
    {
        public static void Inicializar(AppDbContext context)
        {
            // Si ya existen distritos, no hacer nada
          //  if (context.NivelesDistrito.Any()) return;

          //  var distritos = new List<NivelDistrito>
          //  {
          //      new NivelDistrito { Nombre = "PANDO" },
          //      new NivelDistrito { Nombre = "BENI" },
          //      new NivelDistrito { Nombre = "CHUCHISACA" },
           //     new NivelDistrito { Nombre = "COCHABAMBA" },
           //     new NivelDistrito { Nombre = "LA PAZ" },
           //     new NivelDistrito { Nombre = "ORURO" },
           //     new NivelDistrito { Nombre = "POTOSI" },
          //      new NivelDistrito { Nombre = "SANTA CRUZ" },
          //      new NivelDistrito { Nombre = "TARIJA" }
          //  };

          //  context.NivelesDistrito.AddRange(distritos);
          //  context.SaveChanges();
        }
    }
}
