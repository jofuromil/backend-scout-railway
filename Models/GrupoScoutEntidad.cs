using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace BackendScout.Models
{
    public class GrupoScoutEntidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        public string Distrito { get; set; }

        public List<Unidad> Unidades { get; set; }
        public List<GrupoScoutUsuario> Usuarios { get; set; }
    }
}
