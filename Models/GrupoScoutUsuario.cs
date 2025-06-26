using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendScout.Models
{
    public class GrupoScoutUsuario
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UsuarioId { get; set; }

        [Required]
        public int GrupoScoutId { get; set; }

        [Required]
        public bool EsAdminGrupo { get; set; }

        [ForeignKey("UsuarioId")]
        public User Usuario { get; set; }

        [ForeignKey("GrupoScoutId")]
        public GrupoScout GrupoScout { get; set; }
    }
}
