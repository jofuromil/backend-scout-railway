namespace BackendScout.DTOs
{public class RegistroGestionResumenDto
{
    public Guid UsuarioId { get; set; }
    public string NombreCompleto { get; set; }
    public string CI { get; set; }
    public string Rama { get; set; }
    public string Tipo { get; set; } // Scout o Dirigente
    public string UnidadNombre { get; set; }
    public string GrupoNombre { get; set; }
    public string DistritoNombre { get; set; }

    // Estado en gestión
    public bool AprobadoGrupo { get; set; }
    public bool EnviadoADistrito { get; set; }
    public bool AprobadoDistrito { get; set; }
    public bool EnviadoANacional { get; set; }
    public bool AprobadoNacional { get; set; }

    public DateTime? FechaAprobadoGrupo { get; set; }
    public DateTime? FechaEnvioDistrito { get; set; }
}
}
