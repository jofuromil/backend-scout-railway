namespace BackendScout.Models
{
    public class GrupoScout
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Nueva relación con NivelDistrito
        public int NivelDistritoId { get; set; }
        public NivelDistrito NivelDistrito { get; set; }

        // Relación con unidades
        public List<Unidad> Unidades { get; set; }

        // Relación con usuarios
        public List<GrupoScoutUsuario> GrupoScoutUsuarios { get; set; }
    }
}
