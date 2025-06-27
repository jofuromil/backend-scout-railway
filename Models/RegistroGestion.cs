using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendScout.Models
{
    public class RegistroGestion
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public User Usuario { get; set; }

        [Required]
        public Guid GestionId { get; set; }

        [ForeignKey("GestionId")]
        public Gestion Gestion { get; set; }

        // Estado de registro
        public bool AprobadoGrupo { get; set; } = false;
        public DateTime? FechaAprobadoGrupo { get; set; }

        public bool EnviadoADistrito { get; set; } = false;
        public bool AprobadoDistrito { get; set; } = false;
        public DateTime? FechaAprobadoDistrito { get; set; }

        public bool EnviadoANacional { get; set; } = false;
        public bool AprobadoNacional { get; set; } = false;
        public DateTime? FechaAprobadoNacional { get; set; }
    }
}
