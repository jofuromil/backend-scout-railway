using System.ComponentModel.DataAnnotations;

namespace BackendScout.Models
{
    public class Gestion
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty; // Ejemplo: "2025"
        public bool Activa { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.UtcNow;

        public DateTime? FechaCierre { get; set; }

        public bool EstaActiva { get; set; } = false;

        public List<RegistroGestion>? Registros { get; set; }
    }
}
