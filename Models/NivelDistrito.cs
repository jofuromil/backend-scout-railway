using System;
using System.Collections.Generic;

namespace BackendScout.Models
{
    public class NivelDistrito
    {
        public int Id { get; set; }  // ID numérico fijo (1 al 9)
        public string Nombre { get; set; } = string.Empty;

        // Relación: Un distrito tiene muchos grupos
        public List<GrupoScout>? GruposScout { get; set; }

        // Relación: Un distrito puede tener dirigentes administradores
        public List<NivelDistritoUsuario>? NivelDistritoUsuarios { get; set; }

        // ✅ Nueva relación: Un distrito tiene muchas unidades
        public List<Unidad>? Unidades { get; set; }
    }
}

