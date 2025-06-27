namespace BackendScout.Dtos
{
    public class UsuarioResumenDto
    {
        public Guid Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Rama { get; set; } = string.Empty;
        public string UnidadNombre { get; set; } = string.Empty;
    }
}
